using System;
using System.Collections.Generic;
using System.Linq;
using PriceCalculator.Domain;

namespace PriceCalulator.Domain
{
    /// <summary>
    /// Class <c>Basket</c> represents a basket of shopping.
    /// </summary>
    public class Basket
    {
        private List<AppliedOffer> basketOffers = new List<AppliedOffer>();
        private readonly List<BasketItem> basketItems = new List<BasketItem>();
        private int subtotal;
        private Guid transactionId;

        public Basket(Guid transactionId)
        {
            this.transactionId = transactionId;
        }

        public void AddItem(string itemName, int unitPrice)
        {
            var existingItem = basketItems.SingleOrDefault(item => item.ItemName == itemName);

            if(existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                basketItems.Add(new BasketItem(itemName, unitPrice, 1));
            }

            subtotal += unitPrice;
        }

        internal bool ContainsItems(IEnumerable<string> items)
        {
            return items.Intersect(basketItems.Select(item => item.ItemName)).Any();
        }

        public BasketItem GetBasketItem(string itemName)
        {
            return basketItems.SingleOrDefault(item => item.ItemName == itemName);
        }

        public int Subtotal { get => subtotal; }
        

        public int TotalOfferDiscount
        {
            get
            {
                int offerDiscount = 0;
                foreach (AppliedOffer offer in basketOffers)
                {
                    offerDiscount += offer.Discount;
                }

                return offerDiscount;
            }
        }

        public IList<BasketItem> BasketItems { get => basketItems; }
        public IList<AppliedOffer> BasketOffers { get => basketOffers; }
        public Guid TransactionId { get => transactionId; }

        internal void AddOffers(IList<AppliedOffer> appliedOffers)
        {
            basketOffers.AddRange(appliedOffers);
        }
    }

    public class BasketItem
    {
        public BasketItem(string itemName, int unitPrice, int quantity)
        {
            ItemName = itemName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; internal set; }
    }
}