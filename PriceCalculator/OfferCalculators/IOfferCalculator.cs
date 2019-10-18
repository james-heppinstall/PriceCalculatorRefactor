using System.Collections.Generic;
using PriceCalculator.Domain;
using PriceCalulator.Domain;

namespace PriceCalculator.OfferCalculators
{
    public interface IOfferCalculator
    {
        IList<AppliedOffer> Calculate(Basket basket);
        bool CanApplyOffer(Basket basket);
    }
}