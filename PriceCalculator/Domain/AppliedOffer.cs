namespace PriceCalculator.Domain
{
    public class AppliedOffer
    {
        private int discount;
        private string offerMessage;

        public AppliedOffer(int discount, string offerMessage)
        {
            this.discount = discount;
            this.offerMessage = offerMessage;
        }

        public int Discount { get => discount; }
        public string OfferMessage { get => offerMessage; }

    }
}