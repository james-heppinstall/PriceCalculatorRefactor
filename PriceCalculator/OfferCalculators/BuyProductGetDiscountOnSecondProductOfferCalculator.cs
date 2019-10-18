using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Domain;
using PriceCalulator.Domain;

namespace PriceCalculator.OfferCalculators
{
    public class BuyProductGetDiscountOnSecondProductOfferCalculator : IOfferCalculator
    {
        private IList<BuyProductGetDiscountOnSecondProductOffer> offers;

        public bool CanApplyOffer(Basket basket)
        {
            return basket.ContainsItems(offers.Select(offer => offer.PrimaryItemName));
        }

        public IList<AppliedOffer> Calculate(Basket basket)
        {
            var appliedOffers = new List<AppliedOffer>();

            offers = getBuyProductGetDiscountOnSecondProductOffers();

            if (CanApplyOffer(basket))
            {
                foreach (BuyProductGetDiscountOnSecondProductOffer offer in offers)
                {
                    int SecondaryItemCount = 0;

                    var primaryItem = basket.GetBasketItem(offer.PrimaryItemName);
                    var primaryItemCount = primaryItem.Quantity;
                    var secondaryItem = basket.GetBasketItem(offer.SecondaryItemName);

                    if (secondaryItem != null)
                    {
                        SecondaryItemCount = secondaryItem.Quantity;

                        // keep calculating offers until we run out of products that match
                        while (primaryItemCount >= offer.PrimaryItemCount && SecondaryItemCount > 0)
                        {
                            var totalDiscount = secondaryItem.UnitPrice * (offer.PercentageDiscount / 100);
                            appliedOffers.Add(new AppliedOffer((int)totalDiscount, $"Buy {offer.PrimaryItemCount} {primaryItem.ItemName}, get {offer.PercentageDiscount}% off {secondaryItem.ItemName}: -{totalDiscount}p"));
                            primaryItemCount -= offer.PrimaryItemCount;
                            SecondaryItemCount--;
                        }
                    }
                }
            }

            return appliedOffers;
        }

        private IList<BuyProductGetDiscountOnSecondProductOffer> getBuyProductGetDiscountOnSecondProductOffers()
        {
            // TODO : fetch from datastore
            return new List<BuyProductGetDiscountOnSecondProductOffer>
            {
                new BuyProductGetDiscountOnSecondProductOffer{ PrimaryItemName = "Beans", PrimaryItemCount = 2, SecondaryItemName = "Bread", PercentageDiscount = 50 },
            };
        }

    }

    internal class BuyProductGetDiscountOnSecondProductOffer
    {
        public string PrimaryItemName { get; internal set; }
        public string SecondaryItemName { get; internal set; }
        public int PrimaryItemCount { get; internal set; }
        public float PercentageDiscount { get; internal set; }
    }
}