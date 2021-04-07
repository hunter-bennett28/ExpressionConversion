using System;
using System.Collections.Generic;

namespace Project2_Group_17
{
    class Program
    {
        static void Main(string[] args)
        {
            string underline = new string('=', Console.WindowWidth);
            // Parse expressions from CSV file
            List<InfixExpression> expressions = CSVFile.CSVDeserialize("../../../Data/Project 2_Info_5101.csv");

            /* Prefix */

            // Print converted Postfix expressions
            Console.WriteLine("Prefix Expressions:");
            Console.WriteLine(underline);
            foreach (InfixExpression exp in expressions)
            {
                // TODO
            }

            // Print expression results
            Console.WriteLine("\nPrefix Results:");
            Console.WriteLine(underline);
            foreach (InfixExpression exp in expressions)
            {
                // TODO
            }

            /* Postfix */

            // Print converted Postfix expressions
            Console.WriteLine("\nPostfix Expressions:");
            Console.WriteLine(underline);
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                Console.WriteLine($"{exp.SNO}:\t{convertedExpression}");
            }

            // Print expression results
            Console.WriteLine("\nPostfix Results:");
            Console.WriteLine(underline);
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                string evaluatedResult = ExpressionEvaluation.EvaluatePostFix(convertedExpression);
                Console.WriteLine($"{exp.SNO}:\t{evaluatedResult}");
            }

            // TODO: Print sumary report

            // TODO: Prompt user to launch web browser XML output
        }
    }
}
