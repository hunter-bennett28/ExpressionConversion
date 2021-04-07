using System;
using System.Collections.Generic;
using System.Text;

namespace Project2_Group_17
{
    public static class InfixToPostfixConverter
    {
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

        public static string Convert(string exp)
        {
            // initializing empty string for result 
            string result = "";

            // initializing empty stack 
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < exp.Length; ++i)
            {
                char c = exp[i];

                // If the scanned character is an 
                // operand, add it to output. 
                if (char.IsLetterOrDigit(c))
                {
                    result += c;
                }

                // If the scanned character is an '(',
                // push it to the stack. 
                else if (c == '(')
                {
                    stack.Push(c);
                }

                //  If the scanned character is an ')', 
                // pop and output from the stack  
                // until an '(' is encountered. 
                else if (c == ')')
                {
                    while (stack.Count > 0 &&
                            stack.Peek() != '(')
                    {
                        result += stack.Pop();
                    }

                    if (stack.Count > 0 && stack.Peek() != '(')
                    {
                        return "Invalid Expression"; // invalid expression
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else // an operator is encountered
                {
                    while (stack.Count > 0 && Precedence(c) <=
                                      Precedence(stack.Peek()))
                    {
                        result += stack.Pop();
                    }
                    stack.Push(c);
                }

            }

            // pop all the operators from the stack 
            while (stack.Count > 0)
            {
                result += stack.Pop();
            }

            return result;
        }
    }
}
