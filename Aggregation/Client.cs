namespace Aggregation
{
    public class Client
    {
        private readonly Deposit[] deposits;


        public Client()
        {
            this.deposits = new Deposit[10];
        }

        public bool AddDeposit(Deposit deposit)
        {
            for (int i = 0; i < deposits.Length; i++)
            {
                if (deposits[i] == null)
                {
                    deposits[i] = deposit;
                    return true;
                }
            }
            return false;
        }

        public decimal TotalIncome()
        {
            decimal totalIncome = 0;
            for (int i = 0; i < deposits.Length; i++)
            {
                if (deposits[i] != null)
                    totalIncome += deposits[i].Income();
            }
            return totalIncome;
        }

        public decimal MaxIncome()
        {
            decimal maxValue = 0;
            for (int i = 0; i < deposits.Length; i++)
            {
                if (deposits[i] != null && maxValue < deposits[i].Income())
                {
                    maxValue = deposits[i].Income();
                }
            }
            return maxValue;
        }

        public decimal GetIncomeByNumber(int num)
        {
            decimal getInc = 0m;
            for (int i = 0; i < deposits.Length; i++)
            {
                if (num == (i + 1) && deposits[i] != null)
                    getInc = deposits[i].Income();

            }
            return getInc;
        }

    }
}