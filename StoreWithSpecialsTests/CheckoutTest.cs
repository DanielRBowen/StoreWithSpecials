using StoreWithSpecials;
using StoreWithSpecials.Data;
using StoreWithSpecials.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StoreWithSpecialsTests
{
    public class CheckoutTest
    {
        [Fact]
        public void CalculateTotal_TwoMediumPizzas_16()
        {
            var checkout = new Checkout();
            int[] cart = { 1, 2 }; // Pepperoni and cheese medium pizzas
            decimal total = checkout.CalculateTotal(cart);
            Assert.Equal(16, total);
        }

        [Fact]
        public void CalculateSpecialValue_DesertOff_5()
        {
            var checkout = new Checkout();
            var storeData = new StoreData();
            checkout.CalculateSpecialValue(out decimal dessertValue, out double percent, storeData.Specials.First(special => special.Name == "Sweet Deal"));
            Assert.Equal(5, dessertValue);
        }

        [Fact]
        public void TotalWithSpcialsApplied_2Pizza1Dessert1Drink_DessertOff()
        {
            var checkout = new Checkout();
            var storeData = new StoreData();
            int[] cart = { 1, 2, 3, 5 }; // Pepperoni, cheese medium pizza, brownie, and rootbeer.
            var activeSpecials = new List<Special>
            {
                storeData.Specials.First(special => special.Name == "Sweet Deal")
            };
            (var newTotal, var specialsPrice, var specialsPercent) = checkout.TotalWithSpecialsApplied(cart, checkout.CalculateTotal(cart), activeSpecials);
            Assert.Equal(5, specialsPrice);
            Assert.Equal(0, specialsPercent);
            Assert.Equal(17, newTotal);
        }

        [Fact]
        public void TotalWithSpcialsApplied_BuyAPizzaGetOneOff_PizzaOff()
        {
            var checkout = new Checkout();
            var storeData = new StoreData();
            int[] cart = { 1, 2, 3, 5 }; // Pepperoni, cheese medium pizza, brownie, and rootbeer.
            var activeSpecials = new List<Special>
            {
                storeData.Specials.First(special => special.Name == "B1G1 Pizza")
            };
            (var newTotal, var specialsPrice, var specialsPercent) = checkout.TotalWithSpecialsApplied(cart, checkout.CalculateTotal(cart), activeSpecials);
            Assert.Equal(8, specialsPrice);
            Assert.Equal(0, specialsPercent);
            Assert.Equal(14, newTotal);
        }

        [Fact]
        public void TotalWithSpcialsApplied_EveryThingOffCode_0()
        {
            var checkout = new Checkout();
            var storeData = new StoreData();
            int[] cart = { 1, 2, 3, 5 }; // Pepperoni, cheese medium pizza, brownie, and rootbeer.
            var activeSpecials = new List<Special>
            {
                storeData.Specials.First(special => special.Name == "Everything Off")
            };
            (var newTotal, var specialsPrice, var specialsPercent) = checkout.TotalWithSpecialsApplied(cart, checkout.CalculateTotal(cart), activeSpecials, "EverythingOff");
            Assert.Equal(0, specialsPrice);
            Assert.Equal(1, specialsPercent);
            Assert.Equal(0, newTotal);
        }
    }
}
