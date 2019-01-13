using StoreWithSpecials.Data;
using StoreWithSpecials.Models;
using System.Collections.Generic;
using System.Linq;

namespace StoreWithSpecials
{
    public class Checkout
    {
        private static StoreData _storeData { get; set; }

        public Checkout()
        {
            _storeData = new StoreData();
        }

        public decimal CalculateTotal(IEnumerable<int> cart)
        {
            decimal total = new decimal(0);

            foreach (int productId in cart)
            {
                total += _storeData.Products.Single(product => product.Id == productId).Price;
            }

            return total;
        }

        public (decimal totalWithSpecials, decimal specialsPriceTotal, double specialsPercentTotal) TotalWithSpecialsApplied(IEnumerable<int> cart, decimal total, IEnumerable<Special> activeSpecials, string activationCode = null)
        {
            decimal specialsPriceTotal = new decimal(0);
            double specialsPercentTotal = new double();

            foreach (Special activeSpecial in activeSpecials)
            {
                if (activeSpecial.ActivateCondition != null)
                {
                    // Check if the cart has the quantity of each group nessesary for the special to activate.
                    if (activeSpecial.ActivateCondition.ProductGroupsAndQuanities.Any())
                    {
                        int productGroupCount = activeSpecial.ActivateCondition.ProductGroupsAndQuanities.Count();
                        int activateGroupChecks = productGroupCount;

                        foreach (KeyValuePair<IEnumerable<int>, int> productGroup in activeSpecial.ActivateCondition.ProductGroupsAndQuanities)
                        {
                            IEnumerable<int> productGroupIds = productGroup.Key;
                            IEnumerable<int> productGroupIdsInCart = productGroupIds.Intersect(cart);

                            if (productGroupIdsInCart.Count() == productGroup.Value)
                            {
                                activateGroupChecks--;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (activateGroupChecks == 0)
                        {
                            // Give the percent off or price off
                            CalculateSpecialValue(out specialsPriceTotal, out specialsPercentTotal, activeSpecial);
                        }
                    }
                    // Activate special above certain price.
                    else if (activeSpecial.ActivateCondition.ActivatePrice > 0)
                    {
                        CalculateSpecialValue(out specialsPriceTotal, out specialsPercentTotal, activeSpecial);
                    }
                    // With activation code
                    else if (!string.IsNullOrWhiteSpace(activeSpecial.ActivateCondition.ActivationCode))
                    {
                        if (activationCode == activeSpecial.ActivateCondition.ActivationCode)
                        {
                            CalculateSpecialValue(out specialsPriceTotal, out specialsPercentTotal, activeSpecial);
                        }
                    }
                }
                else
                {
                    //No activation condition but, has a price off value or percent off values anyway
                    CalculateSpecialValue(out specialsPriceTotal, out specialsPercentTotal, activeSpecial);
                }
            }

            var newTotal = new decimal(0);
            
            if (specialsPercentTotal > 0)
            {
                newTotal = total - (total * (decimal)specialsPercentTotal);
            }
            else if (specialsPriceTotal > 0)
            {
                newTotal = total - specialsPriceTotal;
            }

            return (newTotal, specialsPriceTotal, specialsPercentTotal);
        }

        public void CalculateSpecialValue(out decimal specialsPriceTotal, out double specialsPercentTotal, Special activeSpecial, IEnumerable<int> cart = null)
        {
            decimal specialPrice = new decimal(0);
            double specialPercent = new double();

            // Take off the max of each product group off the price if any.
            if (activeSpecial.ValueCondition != null && activeSpecial.ValueCondition.ProductGroupsAndQuanities.Any())
            {
                Dictionary<IEnumerable<int>, int> valueProductGroups = activeSpecial.ValueCondition.ProductGroupsAndQuanities;
                foreach (KeyValuePair<IEnumerable<int>, int> productGroup in valueProductGroups)
                {
                    IEnumerable<int> productIds = productGroup.Key;
                    IEnumerable<decimal> productGroupPrices = from productId in productIds
                                                              select
                                                              _storeData.Products
                                                              .Where(product => product.Id == productId)
                                                              .Select(product => product.Price)
                                                              .SingleOrDefault();

                    specialPrice += productGroupPrices.Max();
                }
            }
            else
            {
                if (activeSpecial.PriceOff > 0)
                {
                    specialPrice += activeSpecial.PriceOff;
                }
                else if (activeSpecial.PercentOff > 0)
                {
                    specialPercent += activeSpecial.PercentOff;
                }
            }

            specialsPriceTotal = specialPrice;
            specialsPercentTotal = specialPercent;
        }
    }
}
