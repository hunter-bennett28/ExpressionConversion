using System;
using System.Collections.Generic;

namespace Project2_Group_17.Prefix
{
    public class InfixToPrefixConverter
    {
        /// <summary>
        /// Get the precedence of an operator
        /// </summary>
        /// <param name="op">The operator</param>
        /// <returns>The precedence level</returns>
        private static int Precedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;
            }
            return -1;
        }

        private static string infixToNearlyPostFix(string infix)
        {
            infix = '(' + infix + ')';
            Stack<char> stack = new Stack<char>();
            string output = "";

            for (int x = 0; x < infix.Length; x++)
            {
                //If it is a character we add it to the output
                if (char.IsLetterOrDigit(infix[x]))
                    output += infix[x];
                else if (infix[x] == '(')
                    stack.Push('(');
                else if (infix[x] == ')')
                {
                    while (stack.Peek() != '(')
                        output += stack.Pop();
                    stack.Pop(); //Remove the '('
                }
                else //Operand
                {
                    while ((Precedence(infix[x]) <
                            Precedence(stack.Peek()))
                            || (Precedence(infix[x]) <=
                            Precedence(stack.Peek())
                            && infix[x] == '^'))
                        output += stack.Pop();

                    //Push the current operator on the stack
                    stack.Push(infix[x]);
                }
            }
            return output;
        }

        /// <summary>
        /// Convert an infix to prefix using a postfix conversion
        /// </summary>
        /// <param name="exp">The expression to be evaluated</param>
        /// <returns>A prefix string</returns>
        public static string Convert(string exp)
        {
            //Reverse the string
            char[] expReversed = exp.ToCharArray();
            Array.Reverse(expReversed);


            //Replace '(' with ')' and vice versa because the expression was reversed
            for (int x = 0; x < expReversed.Length; x++)
            {
                switch (expReversed[x])
                {
                    case '(':
                        expReversed[x] = ')';
                        x++;
                        break;
                    case ')':
                        expReversed[x] = '(';
                        x++;
                        break;
                }
            }
            //Convert to nearly post fix
            expReversed = infixToNearlyPostFix(String.Concat(expReversed)).ToCharArray();
            Array.Reverse(expReversed);
            return String.Concat(expReversed);
        }
    }
}
