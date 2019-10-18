using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Domain;
using PriceCalulator.Domain;

namespace PriceCalculator.OfferCalculators
{
    public class MultibuyOfferCalculator : IOfferCalculator
    {
        private IList<MultibuyOffer> multibuyOffers;

        public bool CanApplyOffer(Basket basket)
        {
            return basket.ContainsItems(multibuyOffers.Select(offer => offer.ItemName));
        }

        public IList<AppliedOffer> Calculate(Basket basket)
        {
            var appliedOffers = new List<AppliedOffer>();

            multibuyOffers = getMultibuyOffers();

            if (CanApplyOffer(basket))
            {
                foreach (MultibuyOffer multibuyOffer in multibuyOffers)
                {

                    if (DateTime.Now < multibuyOffer.ExpiryDate)
                    {
                        var basketItem = basket.GetBasketItem(multibuyOffer.ItemName);
                        if (basketItem != null)
                        {
                            var totalDiscount = basketItem.Quantity * basketItem.UnitPrice * ((float)multibuyOffer.PercentageDiscount / 100);
                            appliedOffers.Add(new AppliedOffer((int)totalDiscount, $"{basketItem.ItemName} {multibuyOffer.PercentageDiscount}% off: -{totalDiscount}p"));
                        }
                    }
                }
            }

            return appliedOffers;
        }

        private List<MultibuyOffer> getMultibuyOffers()
        {
            return new List<MultibuyOffer>
            {
                new MultibuyOffer{ ItemName = "Apples", PercentageDiscount = 10, ExpiryDate = DateTime.Now.AddDays(7) },
                new MultibuyOffer{ ItemName = "Pears", PercentageDiscount = 15, ExpiryDate = DateTime.Now.AddDays(7) },
            };
        }
    }
}