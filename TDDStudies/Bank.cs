using System;
using System.Collections;

namespace TDDStudies
{
    public class Bank
    {
        private readonly Hashtable rates;

        public Bank()
        {
            rates = new Hashtable();
        }

        public Money Reduce(IExpression source, string to)
        {
            return source.Reduce(this, to);
        }

        public int Rate(string from, string to)
        {
            if (from.Equals(to))
                return 1;

            return (int)rates[new Pair(from, to)];
        }

        public void AddRate(string from, string to, int rate)
        {
            try
            {
                rates.Add(new Pair(from, to), rate);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
