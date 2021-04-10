/* XMLExtension.cs
 * Author:  Hunter Bennett, Connor Black, James Dunton
 * Desc:    This class prompts for user input on which browser to open a XML file in
 *          and will create it.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Project2_Group_17
{
    public static class XMLExtension
    {
        //class wide filepath variable to be used by multiple helper methods.
        const string FILE_PATH = @"..\..\..\Data\Project_2_Info_5101_XML_Output.xml";
        /// <summary>
        /// Write the document start to a StreamWriter.
        /// </summary>
        /// <param name="docStart">The StreamWriter to add to</param>
        public static void WriteDocumentStart(this StreamWriter s)
        {
            s.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        }

        /// <summary>
        /// Write start root element in the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>
        public static void WriteStartRootElement(this StreamWriter s)
        {
            s.WriteLine($"<root>");
        }

        /// <summary>
        /// Write end root element in the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>

        public static void WriteEndRootElement(this StreamWriter s)
        {
            s.WriteLine($"</root>");
        }

        /// <summary>
        /// Write start element to the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>
        /// <param name="element">The element name</param>
        /// <param name="indents">Number of tabs to be placed before written element</param>
        public static void WriteStartElement(this StreamWriter s, string element, int indents)
        {
            string tabs = new string('\t', indents);
            s.WriteLine($"{tabs}<{element}>");
        }

        /// <summary>
        /// Write end element to the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>
        /// <param name="element">The element name</param>
        /// <param name="indents">Number of tabs to be placed before written element</param>
        public static void WriteEndElement(this StreamWriter s, string element, int indents)
        {
            string tabs = new string('\t', indents);
            s.WriteLine($"{tabs}</{element}>");
        }

        /// <summary>
        /// Write attribute to the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>
        /// <param name="attribute">The attribute to be displayed</param>
        /// <param name="element">The element name</param>
        /// <param name="indents">Number of tabs to be placed before written element</param>
        public static void WriteAttribute(this StreamWriter s, string attribute, string element, int indents)
        {
            string tabs = new string('\t', indents);
            s.WriteLine($"{tabs}<{element}>{attribute}</{element}>");
        }

        /// <summary>
        /// Create XML helper method.
        /// </summary>
        /// <param name="expressions">The infix expressions to be analyzed and printed to the xml file.</param>
        public static void CreateXMLFile(List<InfixExpression> expressions)
        {
            CompareExpressions compareResults = new CompareExpressions();
            try
            {
                FileStream fs = new FileStream(FILE_PATH, FileMode.Create);//creates new file if none exists, overwrites file where file present
                using (StreamWriter s = new StreamWriter(fs, Encoding.UTF8))
                {
                    //pass StreamWriter into the extension methods to build XML document
                    WriteDocumentStart(s);
                    WriteStartRootElement(s);

                    foreach (InfixExpression exp in expressions)
                    {
                        string preConvertedExpression = InfixToPrefixConverter.Convert(exp.Expression);
                        string preEvaluatedResult = ExpressionEvaluation.EvaluatePreFix(preConvertedExpression);

                        string postConvertedExpression = InfixToPostfixConverter.Convert(exp.Expression);
                        string postEvaluatedResult = ExpressionEvaluation.EvaluatePostFix(postConvertedExpression);

                        WriteStartElement(s, "element", 1);
                        WriteAttribute(s, exp.SNO.ToString(), "sno", 2);
                        WriteAttribute(s, exp.Expression, "infix", 2);
                        WriteAttribute(s, preConvertedExpression, "prefix", 2);
                        WriteAttribute(s, postConvertedExpression, "postfix", 2);
                        WriteAttribute(s, preEvaluatedResult, "evaluation", 2);
                        WriteAttribute(s, compareResults.Compare(Convert.ToDouble(preEvaluatedResult), Convert.ToDouble(postEvaluatedResult)) == 0 ? "true" : "false", "comparison", 2);
                        WriteEndElement(s, "element", 1);
                    }
                    WriteEndRootElement(s);
                    s.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This method asks what browser the user would like to use.
        /// </summary>
        public static void OpenXMLInBrowser()
        {

            bool done = false;
            Console.WriteLine("\nBrowser Viewing Options:\n" +
                "\n\t1) Chrome" +
                "\n\t2) Firefox" +
                "\n\t3) Edge" +
                "\n\tQ) Quit");
            do
            {
                Console.Write("\nSelect browser index to view file in (Ex. 1, Q): ");
                string selection = Console.ReadLine().ToLower();
                switch (selection)
                {
                    case ("1"):
                        done = ProcessStarted("chrome", "Chrome");
                        break;
                    case ("2"):
                        done = ProcessStarted("firefox", "FireFox");
                        break;
                    case ("3"):
                        done = ProcessStarted("msedge", "Microsoft Edge");
                        break;
                    case ("q"):
                        done = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid selection. ");
                        break;
                }
            } while (!done);


        }

        /// <summary>
        /// Attempt to open the xml file in the user's selected browser
        /// </summary>
        /// <param name="browser">the command line argument to open the selected browser.</param>
        /// <param name="browserName">The browser name.</param>
        /// <returns>The result.</returns>
        public static bool ProcessStarted(string cmdBrowser, string browserName)
        {
            bool started = false;
            try
            {

                string path = Path.Combine(Environment.CurrentDirectory,FILE_PATH);
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = cmdBrowser;
                path = ($"\"{path}\"");
                process.StartInfo.Arguments = path;
                started = process.Start();
                Console.WriteLine($"The created XML file should now be opened on your {browserName} browser.");
                process.Close();            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return started;
        }
    }
}

