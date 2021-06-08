namespace Aggregation
{

    public class SpecialDeposit : Deposit
    {
        public SpecialDeposit(decimal depositAmount, int depositPeriod)
                : base(depositAmount, depositPeriod)
        {
        }

        public override decimal Income()
        {
            decimal interest = 0.01m;
            decimal amount = Amount;
            {
                if (Period == 0)
                {
                    return 0;
                }
                for (int i = 0; i < Period; i++)
                {
                    amount += System.Math.Round(amount * interest, 2);
                    interest += 0.01m;
                }

            }
            return System.Math.Round(amount - Amount, 2);
        }
    }
}