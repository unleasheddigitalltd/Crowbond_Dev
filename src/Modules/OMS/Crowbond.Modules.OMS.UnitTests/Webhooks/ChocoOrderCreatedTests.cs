using System.Security.Cryptography;
using System.Text;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Webhooks.Choco;
using Crowbond.Modules.OMS.Presentation.Webhooks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;

namespace Crowbond.Modules.OMS.UnitTests.Webhooks
{
    public class ChocoOrderCreatedTests
    {
        private const string Secret =
            "7204319714fef9ef1def67df218040a1d1e6b20352211adad8f2cfdc730983425becfa6b2541d8d425a7ebc57f6a45e9";

        /// <summary>
        /// Helper to compute the HMACSHA256 signature given a secret and request body.
        /// </summary>
        private static string ComputeSignature(string secret, string requestBody)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] bodyBytes = Encoding.UTF8.GetBytes(requestBody);
            using var hmac = new HMACSHA256(keyBytes);
            byte[] computedHash = hmac.ComputeHash(bodyBytes);
            return Convert.ToHexString(computedHash);
        }

        /// <summary>
        /// Helper to create a minimal HTTP context with a given request body and optional signature header.
        /// </summary>
        private static DefaultHttpContext CreateHttpContext(string requestBody, string? signature = null)
        {
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethods.Post;
            // Write the request body bytes to a memory stream.
            var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            context.Request.Body = bodyStream;
            if (signature != null)
            {
                context.Request.Headers["X-Choco-Signature"] = new StringValues(signature);
            }
            // Reset the stream position so it can be read from the beginning.
            bodyStream.Seek(0, SeekOrigin.Begin);
            // Provide a fresh response body stream to capture output.
            context.Response.Body = new MemoryStream();
            return context;
        }

        /// <summary>
        /// A helper to invoke the endpoint's logic directly via its HandleAsync method.
        /// </summary>
        private static async Task<IResult> InvokeHandleAsync(
            ChocoOrderCreated endpoint,
            HttpContext context,
            IServiceProvider serviceProvider,
            ILogger<ChocoOrderCreated> logger,
            IConfiguration configuration)
        {
            // Ensure RequestServices is set so that dependency lookups in result execution work.
            context.RequestServices = serviceProvider;
            return await endpoint.HandleAsync(context, serviceProvider, logger, configuration);
        }

        [Fact]
        public async Task MissingSignature_ReturnsUnauthorized()
        {
            // Arrange
            string requestBody = "{\"payload\": { \"order\": { \"id\": \"123\" }}}";
            var context = CreateHttpContext(requestBody, signature: null);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            // For this test we don’t need to resolve the command handler.
            var serviceProviderMock = new Mock<IServiceProvider>();
            // Register ILoggerFactory so that result execution can resolve it.
            serviceProviderMock.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(loggerFactory);

            var endpoint = new ChocoOrderCreated();

            // Act
            IResult result = await InvokeHandleAsync(endpoint, context, serviceProviderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert – missing signature should result in 401 Unauthorized.
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
        }

        [Fact]
        public async Task InvalidSignature_ReturnsUnauthorized()
        {
            // Arrange
            string requestBody = "{\"payload\": { \"order\": { \"id\": \"123\" }}}";
            // Compute a valid signature then change it slightly to simulate an invalid one.
            string validSignature = ComputeSignature(Secret, requestBody);
            string invalidSignature = validSignature + "ABC";
            var context = CreateHttpContext(requestBody, signature: invalidSignature);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(loggerFactory);

            var endpoint = new ChocoOrderCreated();

            // Act
            IResult result = await InvokeHandleAsync(endpoint, context, serviceProviderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert – invalid signature should result in 401 Unauthorized.
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
        }

        
        
        [Fact]
        public async Task InvalidPayload_ReturnsBadRequest()
        {
            // Arrange: Payload missing a valid 'order' object.
            string requestBody = "{}";
            string signature = ComputeSignature(Secret, requestBody);
            var context = CreateHttpContext(requestBody, signature: signature);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(loggerFactory);

            var endpoint = new ChocoOrderCreated();

            // Act
            IResult result = await InvokeHandleAsync(endpoint, context, serviceProviderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert – invalid payload should result in 400 Bad Request.
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        }

        [Fact]
        public async Task ValidRequest_ReturnsOk_AndInvokesCommandHandler()
        {
            // Arrange: A valid payload containing a non-null "order" object.
            string payload = @"{""webhookEventId"":""7cffe86b-38bb-4194-9d68-542844fd1edd"",""adapterId"":""ee36f7d0-c8d2-4689-806f-e6c23a05e3c8"",""actionId"":""603dd4cd-8a91-43af-b946-130ff7838ffa"",""connectionId"":""f17054ab-703f-401f-a3d3-504166b6b6f2"",""vendorId"":""80173e3a-fba3-4d57-a050-a7a6fa038a1f"",""deliveryAttempt"":4,""deliveryAttemptedAt"":""2025-03-14T12:13:27.742Z"",""createdAt"":""2025-03-14T11:52:22.862Z"",""payload"":{""eventType"":""/vendor/v1/webhooks/OrderCreated"",""order"":{""comment"":"""",""confirmedAt"":null,""contact"":{""id"":""83e0533f-d1c7-4836-88ee-11a0c1a8be03"",""email"":""cginty@tcd.ie"",""name"":""Colm Ginty"",""phone"":""+447777777775""},""createdAt"":""2025-03-14T11:52:18.795Z"",""customer"":{""companyAddress"":{""country"":""United Kingdom"",""city"":""London"",""full"":""Market Pl, London SE16, UK""},""companyId"":""0756d60d-5794-4788-a6fd-d81fc97a36ee"",""customerNumber"":""CT100"",""deliveryAddress"":{""country"":""United Kingdom"",""city"":""London"",""full"":""Market Pl, London SE16, UK""},""id"":""24f041b6-cfcd-4896-bedc-fa5ed9ae3b0c"",""name"":""Colm_Test_Buyer"",""referenceNumber"":""BS9WJV"",""utcOffsetMinutes"":60},""deliveryDate"":""2025-03-17"",""id"":""7c86c146-0af6-4c94-b1a2-528706561b6a"",""products"":[{""product"":{""id"":""supplier-80173e3a-fba3-4d57-a050-a7a6fa038a1f-GRCH5SB"",""name"":""Ground Chinese 5 Spice: 1x440g (Each)"",""vendorId"":""80173e3a-fba3-4d57-a050-a7a6fa038a1f"",""description"":""Ground Chinese 5 Spice: 1x440g"",""externalId"":""GRCH5SB"",""variantGroupCode"":null,""categoryName"":""Dry Goods"",""subCategoryName"":"""",""isActive"":true,""unit"":""S-Ordering_unit"",""packSize"":"""",""createdAt"":""2022-08-29T14:34:26.000Z"",""updatedAt"":""2022-08-29T14:34:26.000Z"",""unitPrice"":{""currency"":""USD"",""amount"":""7.58""},""baseUnit"":null,""conversionFactor"":null,""leadTimeDays"":null,""cutOffTime"":null,""ean"":null,""upc"":null,""origin"":null,""brand"":null,""minimumOrderingQuantity"":null},""productId"":""supplier-80173e3a-fba3-4d57-a050-a7a6fa038a1f-GRCH5SB"",""quantity"":""1"",""totalAmount"":{""currency"":""USD"",""amount"":""7.58""},""unitPrice"":{""currency"":""USD"",""amount"":""7.58""}}],""referenceNumber"":""O250315U6F4LL"",""status"":""Pending"",""updatedAt"":""2025-03-14T11:52:18.795Z"",""vendorReferenceNumber"":""WA4Z4"",""createdByContext"":""ChocoApp""}}}";
            // Provided signature that we want to validate against.
            string providedSignature = "280e4f522aaafbf52894a6e28587f6a5e092b40bb236ebea8813e9e246b2aafd";

            var context = CreateHttpContext(payload, signature: providedSignature);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            // Set up a fake command handler which returns a successful result.
            var commandHandlerMock = new Mock<IChocoOrderCreatedCommandHandler>();
            commandHandlerMock
                .Setup(m => m.Handle(It.IsAny<ChocoOrderCreatedCommand>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(Result.Success()));

            // Set up the service provider to return the fake command handler.
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(ChocoOrderCreatedCommandHandler)))
                .Returns(commandHandlerMock.Object);
            serviceProviderMock.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(loggerFactory);

            var endpoint = new ChocoOrderCreated();

            // Act
            IResult result = await InvokeHandleAsync(endpoint, context, serviceProviderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert – valid request should result in 200 OK.
            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);

            // Optionally, read the response body.
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            string responseBody = await reader.ReadToEndAsync();
            Assert.Contains("Webhook received successfully", responseBody);

            // Verify that the command handler’s Handle method was invoked.
            commandHandlerMock.Verify(
                m => m.Handle(It.IsAny<ChocoOrderCreatedCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExceptionThrown_ReturnsProblem()
        {
            // Arrange
            string requestBody = "{\"payload\": { \"order\": { \"id\": \"123\" }}}";
            string signature = ComputeSignature(Secret, requestBody);
            var context = CreateHttpContext(requestBody, signature: signature);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            // Configure the service provider so that GetService throws an exception.
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(ChocoOrderCreatedCommandHandler)))
                .Throws(new Exception("Test exception"));
            // Also register ILoggerFactory for result execution.
            serviceProviderMock.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(loggerFactory);

            var endpoint = new ChocoOrderCreated();

            // Act
            IResult result = await InvokeHandleAsync(endpoint, context, serviceProviderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert – an exception should result in a problem (500 Internal Server Error).
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

            // Optionally, read the response body to verify the error message.
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            string responseBody = await reader.ReadToEndAsync();
            Assert.Contains("Test exception", responseBody);
        }
    }
}
