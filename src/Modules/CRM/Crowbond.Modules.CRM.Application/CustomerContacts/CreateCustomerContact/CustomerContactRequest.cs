﻿namespace Crowbond.Modules.CRM.Application.CustomerContacts.CreateCustomerContact;

public sealed record CustomerContactRequest(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Mobile,
    string Username,
    string Email,
    bool ReceiveInvoice,
    bool ReceiveOrder,
    bool ReceivePriceList);
