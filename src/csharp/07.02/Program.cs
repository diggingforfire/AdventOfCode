using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _07._02
{
    class Program
    {
        static readonly Dictionary<string, Wire> wires = new Dictionary<string, Wire>();

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                string exp = parts[0].Trim();
                string id = parts[1].Trim();

                wires.Add(id, new Wire { StringExpression = exp });
            }

            var a = wires["a"];
            var expression = GetExpression(a.StringExpression);
            var signal = (ushort)Expression.Lambda<Func<int>>(expression).Compile()();

            foreach (var wire in wires)
                wire.Value.ClearSignal();

            wires["b"].SetSignal(signal);

            expression = GetExpression(a.StringExpression);
            signal = (ushort)Expression.Lambda<Func<int>>(expression).Compile()();

            Console.WriteLine(signal);
            Console.ReadKey();
        }


        static Expression GetExpressionFromToken(string token)
        {
            Expression expression = null;
            int value;

            if (Int32.TryParse(token, out value))
                expression = Expression.Constant(value);
            else
            {
                var wire = wires[token];

                if (wire.Signal != null)
                    expression = Expression.Constant(wire.Signal);
                else
                {
                    expression = GetExpression(wire.StringExpression);
                    wire.SetSignal(expression);
                }
            }

            return expression;
        }

        static Expression GetExpression(string exp)
        {
            var parts = exp.Split(' ');
            Expression expression = null;

            // constant value or wire that needs to be resolved
            if (parts.Length == 1)
            {
                expression = GetExpressionFromToken(parts[0]);
            }
            // not operator
            else if (parts.Length == 2)
            {
                Expression left = GetExpressionFromToken(parts[1]);
                expression = Expression.Not(left);
            }
            // other bitwise operators with 2 operands
            else if (parts.Length == 3)
            {
                Expression left = GetExpressionFromToken(parts[0]);
                Expression right = GetExpressionFromToken(parts[2]);

                switch (parts[1])
                {
                    case "RSHIFT":
                        expression = Expression.RightShift(left, right);
                        break;

                    case "LSHIFT":
                        expression = Expression.LeftShift(left, right);
                        break;

                    case "OR":
                        expression = Expression.Or(left, right);
                        break;

                    case "AND":
                        expression = Expression.And(left, right);
                        break;
                }
            }

            return expression;
        }
    }
}
