using System.Collections.Generic;

namespace StoreWithSpecials.Models
{
    public class SpecialValueCondition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<IEnumerable<int>, int> ProductGroupsAndQuanities { get; set; } = new Dictionary<IEnumerable<int>, int>();
    }
}
