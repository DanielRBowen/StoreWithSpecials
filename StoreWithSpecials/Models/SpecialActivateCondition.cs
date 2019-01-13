using System.Collections.Generic;

namespace StoreWithSpecials.Models
{
    public class SpecialActivateCondition
    {
        public int Id { get; set; }

        /// <summary>
        /// Activates when this is not 0 and is above this value
        /// </summary>
        public decimal ActivatePrice { get; set; }

        public string Name { get; set; }

        public string ActivationCode { get; set; }

        /// <summary>
        /// If Quantity is 0 then any number of that product group will activate.
        /// </summary>
        public IDictionary<IEnumerable<int>, int> ProductGroupsAndQuanities { get; set; } = new Dictionary<IEnumerable<int>, int>();
    }
}
