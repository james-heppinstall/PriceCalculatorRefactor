using PriceCalculator.Domain;
using PriceCalulator.Domain;
using System.Collections.Generic;

namespace PriceCalculator.Services
{
    public interface ISpecialOfferCalculator
    {
        IList<AppliedOffer> CalculateOffers(Basket basket);
    }
}