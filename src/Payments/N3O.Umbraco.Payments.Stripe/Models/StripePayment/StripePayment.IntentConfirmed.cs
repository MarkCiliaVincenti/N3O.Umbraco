using Stripe;

namespace N3O.Umbraco.Payments.Stripe.Models {
    public partial class StripePayment {
        public void IntentConfirmed(PaymentIntent paymentIntent) {
            IntentUpdated(paymentIntent);
        }
    }
}