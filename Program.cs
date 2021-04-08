using System;
using System.Collections.Generic;

namespace Project2_Group_17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 116;
            string underline = new string('═', Console.WindowWidth);
            // Parse expressions from CSV file
            List<InfixExpression> expressions = CSVFile.CSVDeserialize("../../../Data/Project 2_Info_5101.csv");

            /* Prefix */

            // Print converted Postfix expressions
            Console.WriteLine(underline);
            Console.WriteLine("Prefix Expressions:");
            Console.WriteLine($"{underline}\n");
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                Console.WriteLine($"{exp.SNO}:\t{convertedExpression}");
            }

            // Print expression results
            Console.WriteLine($"\n{underline}");
            Console.WriteLine("Prefix Results:");
            Console.WriteLine($"{underline}\n");
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                string evaluatedResult = ExpressionEvaluation.EvaluatePreFix(convertedExpression);
                Console.WriteLine($"{exp.SNO}:\t{evaluatedResult}");
            }

            /* Postfix */

            // Print converted Postfix expressions
            Console.WriteLine($"\n{underline}");
            Console.WriteLine("Postfix Expressions:");
            Console.WriteLine($"{underline}\n");
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                Console.WriteLine($"{exp.SNO}:\t{convertedExpression}");
            }

            // Print expression results
            Console.WriteLine($"\n{underline}");
            Console.WriteLine("Postfix Results:");
            Console.WriteLine($"{underline}\n");
            foreach (InfixExpression exp in expressions)
            {
                string convertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                string evaluatedResult = ExpressionEvaluation.EvaluatePostFix(convertedExpression);
                Console.WriteLine($"{exp.SNO}:\t{evaluatedResult}");
            }

            /* Summary Report*/

            // Header
            string title = "Summary Report";
            Console.WriteLine($"\n{underline}");
            Console.WriteLine($"{new string(' ', (Console.WindowWidth-title.Length) / 2)}{title}");
            Console.WriteLine($"{underline}\n");
            Console.WriteLine("╔═════╦════════════════════════╦══════════════════════╦══════════════════════╦═══════════════╦═══════════════╦═════╗");
            Console.WriteLine("║{0,-5}║{1,-24}║{2,-22}║{3,-22}║{4,-15}║{5,-15}║{6,-5}║", "Sno", "Infix", "Prefix", "Postfix", "Prefix Result", "Postfix Result", "Match");
            Console.WriteLine("╠═════╬════════════════════════╬══════════════════════╬══════════════════════╬═══════════════╬═══════════════╬═════╣");

            //Body
            CompareExpressions compareResults = new CompareExpressions();
            foreach (InfixExpression exp in expressions)
            {
                string preConvertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                string preEvaluatedResult = ExpressionEvaluation.EvaluatePreFix(preConvertedExpression);

                string postConvertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                string postEvaluatedResult = ExpressionEvaluation.EvaluatePostFix(postConvertedExpression);

                Console.WriteLine("║{0,-5}║{1,-24}║{2,-22}║{3,-22}║{4,-15}║{5,-15}║{6,-5}║",
                    exp.SNO, exp.Expression, preConvertedExpression, postConvertedExpression, preEvaluatedResult, postEvaluatedResult, compareResults.Compare(Convert.ToDouble(preEvaluatedResult), Convert.ToDouble(postEvaluatedResult)) == 0 ? "true" : "false");
            }
            Console.WriteLine("╚═════╩════════════════════════╩══════════════════════╩══════════════════════╩═══════════════╩═══════════════╩═════╝");

            // TODO: Prompt user to launch web browser XML output
        }
    }
}
