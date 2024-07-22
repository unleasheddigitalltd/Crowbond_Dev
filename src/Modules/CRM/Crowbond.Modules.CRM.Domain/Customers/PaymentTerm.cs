using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowbond.Modules.CRM.Domain.Customers;
public enum PaymentTerm
{
    CashOnDelivery = 0,
    DaysAfterShippedDate = 1,
    DaysAfterEndOfInvoiceMonth = 2
}
