using PriceCalulator.Domain;
using Microsoft.Extensions.Logging;
using PriceCalculator.Services;
using System.Collections.Generic;
using PriceCalculator.Domain;
using PriceCalculator.OfferCalculators;

namespace PriceCalulator.Services
{
    /// <summary>
    /// Class <c>SpecialOfferCalculator</c> calculates the special offers to be applied to a basket
    /// </summary>
    public class SpecialOfferCalculator : ISpecialOfferCalculator
    {
        private readonly ILogger<BasketProvider> _logger;
        private readonly IEnumerable<IOfferCalculator> _offerCalculators;

        public SpecialOfferCalculator(IEnumerable<IOfferCalculator> offerCalculators, ILogger<BasketProvider> logger)
        {
            _offerCalculators = offerCalculators;
            _logger = logger;
        }

        /// <summary>
        /// Calculate the offers by comparing products in the basket to available offers
        /// </summary>
        /// <param name="basket">The basket to applt the offers to.</param>
        /// <param name="transactionId">The unique identifier for the transaction</param>
        /// <returns></returns>
        public IList<AppliedOffer> CalculateOffers(Basket basket)
        {
            _logger.LogInformation($"{basket.TransactionId} - Calculating offers");

            var appliedOffers = new List<AppliedOffer>();

            foreach (IOfferCalculator offerCalculator in _offerCalculators)
            {
                appliedOffers.AddRange(offerCalculator.Calculate(basket));
            }

            return appliedOffers;
        }
    }
}