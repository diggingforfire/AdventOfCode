using System;
using System.Linq.Expressions;

namespace _07._02
{
    internal class Wire
    {
        public Expression Expression { get; set; }
        public string StringExpression { get; set; }
        public int? Signal { get; private set; }

        public void SetSignal(ushort signal)
        {
            Signal = signal;
        }

        public void SetSignal(Expression expression)
        {
            Signal = (ushort)Expression.Lambda<Func<int>>(expression).Compile()();
        }

        public void ClearSignal()
        {
            Signal = null;
        }

        public override string ToString()
        {
            return StringExpression;
        }
    }
}