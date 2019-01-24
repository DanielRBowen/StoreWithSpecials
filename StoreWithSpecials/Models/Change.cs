namespace StoreWithSpecials.Models
{
    public class Change
    {
        public decimal AmountGiven { get; set; }

        public decimal AmountDue { get; set; }

        public decimal ChangeAmount
        {
            get
            {
                if (AmountGiven >= AmountDue)
                {
                    return AmountGiven - AmountDue;
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal AmountRemainingDue
        {
            get
            {
                if (AmountGiven < AmountDue)
                {
                    return AmountDue - AmountGiven;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_50
        {
            get
            {
                if (ChangeAmount >= 50)
                {
                    return (int)ChangeAmount / 50;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_20
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50);

                if (remainingChange >= 20)
                {
                    return (int)remainingChange / 20;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_10
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20);

                if (remainingChange >= 10)
                {
                    return (int)remainingChange / 10;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_5
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20) - (10 * Denomination_10);

                if (remainingChange >= 5)
                {
                    return (int)remainingChange / 5;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_1
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20) - (10 * Denomination_10) - (5 * Denomination_5);

                if (remainingChange >= 1)
                {
                    return (int)remainingChange / 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_0_25
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20) - (10 * Denomination_10) - (5 * Denomination_5) - (1 * Denomination_1);

                if (remainingChange >= (decimal)0.25)
                {
                    return (int)(remainingChange / (decimal)0.25);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_0_10
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20) - (10 * Denomination_10) - (5 * Denomination_5) - (1 * Denomination_1) - ((decimal)0.25 * Denomination_0_25);

                if (remainingChange >= (decimal)0.1)
                {
                    return (int)(remainingChange / (decimal)0.1);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_0_05
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20) - (10 * Denomination_10) - (5 * Denomination_5) - (1 * Denomination_1) - ((decimal)0.25 * Denomination_0_25) - ((decimal)0.1 * Denomination_0_10);

                if (remainingChange >= (decimal)0.05)
                {
                    return (int)(remainingChange / (decimal)0.05);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Denomination_0_01
        {
            get
            {
                var remainingChange = ChangeAmount - (50 * Denomination_50) - (20 * Denomination_20) - (10 * Denomination_10) - (5 * Denomination_5) - (1 * Denomination_1) - ((decimal)0.25 * Denomination_0_25) - ((decimal)0.1 * Denomination_0_10) - ((decimal)0.05 * Denomination_0_05);

                if (remainingChange >= (decimal)0.01)
                {
                    return (int)(remainingChange / (decimal)0.01);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Sets change for an amount given and an amout due
        /// </summary>
        /// <param name="amountGiven"></param>
        /// <param name="amountDue"></param>
        public Change(decimal amountGiven, decimal amountDue)
        {
            AmountGiven = amountGiven;
            AmountDue = amountDue;
        }
    }
}
