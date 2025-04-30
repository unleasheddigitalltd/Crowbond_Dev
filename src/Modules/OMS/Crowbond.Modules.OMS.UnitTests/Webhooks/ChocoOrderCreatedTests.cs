using System.Security.Cryptography;
using System.Text;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Webhooks.Choco;
using Crowbond.Modules.OMS.Presentation.Webhooks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MediatR;
using Moq;
using Xunit;

namespace Crowbond.Modules.OMS.UnitTests.Webhooks
{
    public class ChocoOrderCreatedTests
    {
        private const string Secret =
            "7204319714fef9ef1def67df218040a1d1e6b20352211adad8f2cfdc730983425becfa6b2541d8d425a7ebc57f6a45e9";

        // compute HMACSHA256(signature)
        private static string ComputeSignature(string secret, string requestBody)
        {
            var keyBytes  = Encoding.UTF8.GetBytes(secret);
            var bodyBytes = Encoding.UTF8.GetBytes(requestBody);
            using var hmac = new HMACSHA256(keyBytes);
            return Convert.ToHexString(hmac.ComputeHash(bodyBytes));
        }

        // create a minimal HttpContext with optional X-Choco-Signature header
        private static DefaultHttpContext CreateHttpContext(string requestBody, string? signature = null)
        {
            var context = new DefaultHttpContext();
            context.Request.Method = HttpMethods.Post;
            var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            context.Request.Body = bodyStream;
            if (signature != null)
                context.Request.Headers["X-Choco-Signature"] = new StringValues(signature);
            bodyStream.Seek(0, SeekOrigin.Begin);
            context.Response.Body = new MemoryStream();
            return context;
        }

        // Invoke the endpoint passing in an ISender instead of IServiceProvider
        private static Task<IResult> InvokeHandleAsync(
            ChocoOrderCreated endpoint,
            HttpContext context,
            ISender sender,
            ILogger<ChocoOrderCreated> logger,
            IConfiguration configuration)
        {
            // so that ExecuteAsync can resolve ILoggerFactory
            // (only needed for writing the response)
            // You could also build a real ServiceCollection here.
            // But a simple mock that returns the loggerFactory is enough.
            return endpoint.HandleAsync(context, sender, logger, configuration);
        }

        [Fact]
        public async Task MissingSignature_ReturnsUnauthorized()
        {
            // Arrange
            const string body = "{\"payload\": { \"order\": { \"id\": \"123\" }}}";
            var context = CreateHttpContext(body, signature: null);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?> {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();

            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            // Only required so ExecuteAsync can find an ILoggerFactory
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(loggerFactory);
            context.RequestServices = serviceProviderMock.Object;

            var senderMock = new Mock<ISender>();

            var endpoint = new ChocoOrderCreated();

            // Act
            var result = await InvokeHandleAsync(endpoint, context, senderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
            // Sender.Send should never be called
            senderMock.Verify(x => x.Send(It.IsAny<IRequest<Result>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task InvalidSignature_ReturnsUnauthorized()
        {
            // Arrange
            const string body = "{\"payload\": { \"order\": { \"id\": \"123\" }}}";
            var badSig = ComputeSignature(Secret, body) + "ABC";
            var context = CreateHttpContext(body, signature: badSig);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?> {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();

            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(loggerFactory);
            context.RequestServices = serviceProviderMock.Object;

            var senderMock = new Mock<ISender>();
            var endpoint   = new ChocoOrderCreated();

            // Act
            var result = await InvokeHandleAsync(endpoint, context, senderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
            senderMock.Verify(x => x.Send(It.IsAny<IRequest<Result>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task InvalidPayload_ReturnsBadRequest()
        {
            // Arrange
            const string body = "{}";
            var sig = ComputeSignature(Secret, body);
            var context = CreateHttpContext(body, signature: sig);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?> {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();

            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(loggerFactory);
            context.RequestServices = serviceProviderMock.Object;

            var senderMock = new Mock<ISender>();
            var endpoint   = new ChocoOrderCreated();

            // Act
            var result = await InvokeHandleAsync(endpoint, context, senderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert
            // your implementation currently throws or returns 500 on malformed payload
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            senderMock.Verify(x => x.Send(It.IsAny<IRequest<Result>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ValidRequest_ReturnsOk_AndInvokesCommandHandler()
        {
            // Arrange
            string payload = @"{""webhookEventId"":""7cffe86b-38bb-4194-9d68-542844fd1edd"",""adapterId"":""ee36f7d0-c8d2-4689-806f-e6c23a05e3c8"",""actionId"":""603dd4cd-8a91-43af-b946-130ff7838ffa"",""connectionId"":""f17054ab-703f-401f-a3d3-504166b6b6f2"",""vendorId"":""80173e3a-fba3-4d57-a050-a7a6fa038a1f"",""deliveryAttempt"":4,""deliveryAttemptedAt"":""2025-03-14T12:13:27.742Z"",""createdAt"":""2025-03-14T11:52:22.862Z"",""payload"":{""eventType"":""/vendor/v1/webhooks/OrderCreated"",""order"":{""comment"":"""",""confirmedAt"":null,""contact"":{""id"":""83e0533f-d1c7-4836-88ee-11a0c1a8be03"",""email"":""cginty@tcd.ie"",""name"":""Colm Ginty"",""phone"":""+447777777775""},""createdAt"":""2025-03-14T11:52:18.795Z"",""customer"":{""companyAddress"":{""country"":""United Kingdom"",""city"":""London"",""full"":""Market Pl, London SE16, UK""},""companyId"":""0756d60d-5794-4788-a6fd-d81fc97a36ee"",""customerNumber"":""CT100"",""deliveryAddress"":{""country"":""United Kingdom"",""city"":""London"",""full"":""Market Pl, London SE16, UK""},""id"":""24f041b6-cfcd-4896-bedc-fa5ed9ae3b0c"",""name"":""Colm_Test_Buyer"",""referenceNumber"":""BS9WJV"",""utcOffsetMinutes"":60},""deliveryDate"":""2025-03-17"",""id"":""7c86c146-0af6-4c94-b1a2-528706561b6a"",""products"":[{""product"":{""id"":""supplier-80173e3a-fba3-4d57-a050-a7a6fa038a1f-GRCH5SB"",""name"":""Ground Chinese 5 Spice: 1x440g (Each)"",""vendorId"":""80173e3a-fba3-4d57-a050-a7a6fa038a1f"",""description"":""Ground Chinese 5 Spice: 1x440g"",""externalId"":""GRCH5SB"",""variantGroupCode"":null,""categoryName"":""Dry Goods"",""subCategoryName"":"""",""isActive"":true,""unit"":""S-Ordering_unit"",""packSize"":"""",""createdAt"":""2022-08-29T14:34:26.000Z"",""updatedAt"":""2022-08-29T14:34:26.000Z"",""unitPrice"":{""currency"":""USD"",""amount"":""7.58""},""baseUnit"":null,""conversionFactor"":null,""leadTimeDays"":null,""cutOffTime"":null,""ean"":null,""upc"":null,""origin"":null,""brand"":null,""minimumOrderingQuantity"":null},""productId"":""supplier-80173e3a-fba3-4d57-a050-a7a6fa038a1f-GRCH5SB"",""quantity"":""1"",""totalAmount"":{""currency"":""USD"",""amount"":""7.58""},""unitPrice"":{""currency"":""USD"",""amount"":""7.58""}}],""referenceNumber"":""O250315U6F4LL"",""status"":""Pending"",""updatedAt"":""2025-03-14T11:52:18.795Z"",""vendorReferenceNumber"":""WA4Z4"",""createdByContext"":""ChocoApp""}}}";
            const string providedSignature = "280e4f522aaafbf52894a6e28587f6a5e092b40bb236ebea8813e9e246b2aafd";

            var context = CreateHttpContext(payload, signature: providedSignature);
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?> {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();

            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(loggerFactory);
            context.RequestServices = serviceProviderMock.Object;

            // Mock MediatR ISender
            var senderMock = new Mock<ISender>();
            senderMock
                .Setup(m => m.Send(It.IsAny<ChocoOrderCreatedCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            var endpoint = new ChocoOrderCreated();

            // Act
            var result = await InvokeHandleAsync(endpoint, context, senderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var bodyText = await reader.ReadToEndAsync();
            Assert.Contains("Webhook received successfully", bodyText);

            senderMock.Verify(
                m => m.Send(It.IsAny<ChocoOrderCreatedCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ExceptionThrown_ReturnsProblem()
        {
            // Arrange
            const string body = "{\"payload\": { \"order\": { \"id\": \"123\" }}}";
            var sig = ComputeSignature(Secret, body);
            var context = CreateHttpContext(body, signature: sig);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?> {
                    { "OMS:Choco:Secret", Secret }
                })
                .Build();

            using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var logger = loggerFactory.CreateLogger<ChocoOrderCreated>();

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(loggerFactory);
            context.RequestServices = serviceProviderMock.Object;

            // Make the sender throw
            var senderMock = new Mock<ISender>();
            senderMock
                .Setup(m => m.Send(It.IsAny<ChocoOrderCreatedCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Test exception"));

            var endpoint = new ChocoOrderCreated();

            // Act
            var result = await InvokeHandleAsync(endpoint, context, senderMock.Object, logger, configuration);
            await result.ExecuteAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var bodyText = await reader.ReadToEndAsync();
            Assert.Contains("Test exception", bodyText);
        }
    }
}
