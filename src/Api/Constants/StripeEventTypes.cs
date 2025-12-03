namespace Abyat.Api.Constants;

/// <summary>
/// Stripe event type constants.
/// </summary>
public static class StripeEventTypes
{
    public const string CustomerSubscriptionCreated = "customer.subscription.created";
    public const string CustomerSubscriptionDeleted = "customer.subscription.deleted";
    public const string InvoicePaymentSucceeded = "invoice.payment_succeeded";
    public const string InvoicePaymentFailed = "invoice.payment_failed";
    public const string CustomerCreated = "customer.created";
}

