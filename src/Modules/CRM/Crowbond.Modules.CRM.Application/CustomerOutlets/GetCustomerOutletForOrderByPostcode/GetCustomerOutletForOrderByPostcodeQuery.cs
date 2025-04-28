using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrderByPostcode;

public sealed record GetCustomerOutletForOrderByPostcodeQuery(string postcode, Guid customerId) : IQuery<CustomerOutletForOrderResponse>;
