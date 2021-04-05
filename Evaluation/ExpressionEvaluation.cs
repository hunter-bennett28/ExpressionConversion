using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project2_Group_17
{
    public static class ExpressionEvaluation
    {
        private static char[] Operators = new char[] { '+', '-', '*', '/' };

        /// <summary>
        /// Evaluates a string containing an expression in Postfix notation, and returns the result as a string
        /// </summary>
        /// <param name="exp">A postfix notation expression</param>
        /// <returns></returns>
        public static string EvaluatePostFix(string exp)
        {
            Stack<string> stack = new Stack<string>();
            for (int j = 0; j < exp.Length; j++)
            {
                // If character is an operator, do operation, otherwise put it on the stack
                char ch = exp[j];
                if(Operators.Contains(ch))
                    stack.Push(Evaluate(Convert.ToInt32(stack.Pop()), Convert.ToInt32(stack.Pop()), ch));
                else
                    stack.Push(ch.ToString());
            }

            // return final remaining value
            return stack.Pop();
        }

        /// <summary>
        /// Evaluates two operands using given operator and LINQ Expressions
        /// </summary>
        /// <param name="leftOperand"></param>
        /// <param name="rightOperand"></param>
        /// <param name="op"></param>
        private static string Evaluate(int rightOperand, int leftOperand, char op)
        {
            // Determine expression from operator
            BinaryExpression expression;
            switch(op)
            {
                case '*':
                    expression = Expression.Multiply(Expression.Constant(leftOperand), Expression.Constant(rightOperand));
                    break;
                case '/':
                    expression = Expression.Divide(Expression.Constant(leftOperand), Expression.Constant(rightOperand));
                    break;
                case '+':
                    expression = Expression.Add(Expression.Constant(leftOperand), Expression.Constant(rightOperand));
                    break;
                case '-':
                    expression = Expression.Subtract(Expression.Constant(leftOperand), Expression.Constant(rightOperand));
                    break;
                default:
                    throw new Exception($"Invalid operator {op}");
            }

            // Put the result of the operation back on the stack for any more operations
           return (Expression.Lambda<Func<int>>(expression).Compile())().ToString();
        }
    }
}
