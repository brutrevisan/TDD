namespace TDDStudies
{
    public class Sum : IExpression
    {
        public IExpression Augend { get; set; }
        public IExpression Addend { get; set; }

        public Sum(IExpression augend, IExpression addend)
        {
            Augend = augend;
            Addend = addend;
        }

        public Money Reduce(Bank bank, string to)
        {
            int amount = Augend.Reduce(bank, to).Amount + Addend.Reduce(bank, to).Amount;
            return new Money(amount, to);
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public IExpression Times(int mutiplier)
        {
            return new Sum(Augend.Times(mutiplier), Addend.Times(mutiplier));
        }
    }
}
