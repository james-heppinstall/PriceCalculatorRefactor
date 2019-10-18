using System;

namespace PriceCalculator.Domain
{
    public class MultibuyOffer
    {
        public string ItemName { get; set; }
        public int PercentageDiscount { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}