namespace StoreWithSpecials.Models
{
    public class Special
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double PercentOff { get; set; }

        public decimal PriceOff { get; set; }

        public SpecialActivateCondition ActivateCondition { get; set; }

        public SpecialValueCondition ValueCondition { get; set; }
    }
}
