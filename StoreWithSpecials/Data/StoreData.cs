using StoreWithSpecials.Models;
using System.Collections.Generic;
using System.Linq;

namespace StoreWithSpecials.Data
{
    public class StoreData
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public List<Special> Specials { get; set; } = new List<Special>();

        public List<SpecialActivateCondition> SpecialActivateConditions { get; set; } = new List<SpecialActivateCondition>();

        public List<SpecialValueCondition> SpecialValueConditions { get; set; } = new List<SpecialValueCondition>();

        public StoreData()
        {
            Products.Add(
                new Product
                {
                    Id = 1,
                    Name = "Pepperoni Pizza",
                    Type = "Pizza",
                    Tier = "Medium",
                    Description = "A pizza with pepperoni",
                    Price = 8
                });

            Products.Add(
                new Product
                {
                    Id = 2,
                    Name = "Cheese Pizza",
                    Type = "Pizza",
                    Tier = "Medium",
                    Description = "A pizza with only cheese",
                    Price = 8
                });

            Products.Add(
                new Product
                {
                    Id = 3,
                    Name = "Chocolate Brownie",
                    Type = "Dessert",
                    Description = "Brownie made of chocolate",
                    Price = 5
                });

            Products.Add(
                new Product
                {
                    Id = 4,
                    Name = "Chocolate Chip Cookies",
                    Type = "Dessert",
                    Description = "Chocolate Chip Cookies",
                    Price = 5
                });

            Products.Add(
                new Product
                {
                    Id = 5,
                    Name = "Rootbeer",
                    Type = "Drink",
                    Description = "Rootbeer soda pop",
                    Price = 1
                });

            Products.Add(
                new Product
                {
                    Id = 6,
                    Name = "Sprite",
                    Type = "Drink",
                    Description = "Sprite soda pop",
                    Price = 1
                });

            SpecialActivateConditions.Add(
                new SpecialActivateCondition
                {
                    Id = 1,
                    Name = "Two Pizzas, One Dessert, One Drink",
                    ProductGroupsAndQuanities = new Dictionary<IEnumerable<int>, int>
                    {
                        {GetProductGroup("Pizza"), 2},
                        {GetProductGroup("Dessert"), 1},
                        {GetProductGroup("Drink"), 1}
                    }
                });

            SpecialActivateConditions.Add(
                new SpecialActivateCondition
                {
                    Id = 2,
                    Name = "Two Pizzas",
                    ProductGroupsAndQuanities = new Dictionary<IEnumerable<int>, int>
                    {
                        {GetProductGroup("Pizza"), 2}
                    }
                });

            SpecialValueConditions.Add(
                new SpecialValueCondition
                {
                    Id = 1,
                    Name = "One Dessert Off",
                    ProductGroupsAndQuanities = new Dictionary<IEnumerable<int>, int>
                    {
                        {GetProductGroup("Dessert"), 1}
                    }
                });

            SpecialValueConditions.Add(
                new SpecialValueCondition
                {
                    Id = 2,
                    Name = "One Pizza Off",
                    ProductGroupsAndQuanities = new Dictionary<IEnumerable<int>, int>
                    {
                        {GetProductGroup("Pizza"), 1}
                    }
                });

            Specials.Add(
                new Special
                {
                    Id = 1,
                    Name = "Sweet Deal",
                    Description = "Get the highest dessert off when you buy two pizzas, a desert, and a soda",
                    ActivateCondition = SpecialActivateConditions[0],
                    ValueCondition = SpecialValueConditions[0]
                });

            Specials.Add(
                new Special
                {
                    Id = 2,
                    Name = "B1G1 Pizza",
                    Description = "Buy 1 Pizza Get 1 free.",
                    ActivateCondition = SpecialActivateConditions[1],
                    ValueCondition = SpecialValueConditions[1]
                });

            Specials.Add(
                new Special
                {
                    Id = 3,
                    Name = "Everything Off",
                    Description = "Get everything off with a code.",
                    PercentOff = 1,
                    ActivateCondition = new SpecialActivateCondition {  ActivationCode = "EverythingOff" }
                });

            Specials.Add(
                new Special
                {
                    Id = 4,
                    Name = "Five Dollars off",
                    Description = "Get five dollars off",
                    PriceOff = 5
                });

            Specials.Add(
                new Special
                {
                    Id = 5,
                    Name = "Ten percent off",
                    Description = "Ten percent off",
                    PercentOff = 10,
                });
        }

        /// <summary>
        /// Gives all of the Ids of the type or tier
        /// This Could be even better but is good enough for this lab
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tier"></param>
        /// <returns>Will return all Product Ids if no parameters are given</returns>
        public IList<int> GetProductGroup(string type = null, string tier = null)
        {
            if (type == null && tier == null)
            {
                return Products.Select(product => product.Id).ToList();
            }

            List<int> productGroup = new List<int>();
            
            if (tier != null && type != null)
            {
                productGroup.AddRange(Products.Where(product => product.Type == type && product.Tier == tier).Select(product => product.Id));
            }
            else if (type != null)
            {
                productGroup.AddRange(Products.Where(product => product.Type == type).Select(product => product.Id));
            }

            return productGroup;
        }

        public IList<Product> GetProductsOfTypeAndTier(string type = null, string tier = null)
        {
            if (type == null && tier == null)
            {
                return Products;
            }

            var products = new List<Product>();

            if (tier != null && type != null)
            {
                products.AddRange(Products.Where(product => product.Type == type && product.Tier == tier));
            }
            else if (type != null)
            {
                products.AddRange(Products.Where(product => product.Type == type));
            }

            return products;
        }
    }
}
