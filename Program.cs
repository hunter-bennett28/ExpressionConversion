using Project2_Group_17.Comparer;
using Project2_Group_17.Prefix;
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
                string convertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                Console.WriteLine($"{exp.SNO}:\t{convertedExpression}");
            }

            // Print expression results
            Console.WriteLine("\nPrefix Results:");
            Console.WriteLine(underline);
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                string evaluatedResult = ExpressionEvaluation.EvaluatePreFix(convertedExpression);
                Console.WriteLine($"{exp.SNO}:\t{evaluatedResult}");
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

            // Print sumary report
            // Header
            Console.WriteLine(underline);
            string title = "Summary Report";
            Console.WriteLine($"{new string(' ', (Console.WindowWidth-title.Length) / 2)}{title}");
            Console.WriteLine(underline);
            Console.WriteLine("|{0,-5}|{1,-24}|{2,-24}|{3,-24}|{4,-15}|{5,-15}|{6,-5}|", "Sno", "Infix", "Postfix", "Prefix", "Prefix Result", "Postfix Result", "Match");
            Console.WriteLine(underline);

            //Body
            CompareExpressions compareResults = new CompareExpressions();
            foreach (InfixExpression exp in expressions)
            {
                string preConvertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                string preEvaluatedResult = ExpressionEvaluation.EvaluatePreFix(preConvertedExpression);

                string postConvertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                string postEvaluatedResult = ExpressionEvaluation.EvaluatePostFix(postConvertedExpression);

                Console.WriteLine("|{0,-5}|{1,-24}|{2,-24}|{3,-24}|{4,-15}|{5,-15}|{6,-5}|",
                    exp.SNO, exp.Expression, preConvertedExpression, postConvertedExpression, preEvaluatedResult, postEvaluatedResult, compareResults.Compare(preEvaluatedResult, postEvaluatedResult)==1 ? "true" : "false");
            }
            Console.WriteLine(underline);

            // TODO: Prompt user to launch web browser XML output
        }
    }
}
