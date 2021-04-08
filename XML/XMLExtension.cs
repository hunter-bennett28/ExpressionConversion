﻿/// XMLExtension.cs
/// Authors: James Dunton, Hunter Bennett, Connor Black
/// Desc: Extension and helper methods to build and view a new XML file.
/// 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Project2_Group_17.XML
{
    public static class XMLExtension
    {
        //class wide filepath variable to be used by multiple helper methods.
        const string FILE_PATH = "../../../Data/Project 2_Info_5101_XML_Output.xml";
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
        public static void WriteStartElement(this StreamWriter s, string element)
        {
            s.WriteLine($"<{element}>");
        }

        /// <summary>
        /// Write end element to the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>
        /// <param name="element">The element name</param>
        public static void WriteEndElement(this StreamWriter s, string element)
        {
            s.WriteLine($"</{element}>");
        }

        /// <summary>
        /// Write attribute to the StreamWriter.
        /// </summary>
        /// <param name="s">The StreamWriter to add to</param>
        /// <param name="attribute">The attribute to be displayed</param>
        /// <param name="element">The element name</param>
        public static void WriteAttribute(this StreamWriter s, string attribute, string element)
        {
            s.WriteLine($"<{element}>{attribute}</{element}>");
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

                        WriteStartElement(s, "element");
                        WriteAttribute(s, exp.SNO.ToString(), "sno"); ;
                        WriteAttribute(s, exp.Expression, "infix");
                        WriteAttribute(s, preConvertedExpression, "prefix");
                        WriteAttribute(s, postConvertedExpression, "postfix");
                        WriteAttribute(s, preEvaluatedResult, "evaluation");
                        WriteAttribute(s, compareResults.Compare(Convert.ToDouble(preEvaluatedResult), Convert.ToDouble(postEvaluatedResult)) == 0 ? "true" : "false", "comparison");
                        WriteEndElement(s, "element");
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
        /// This method opens the xml file to be viewed on chrome
        /// </summary>
        public static void OpenXMLtoChrome()
        {
            
            try
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "chrome";
                string path = ($"\"{FILE_PATH}\"");
                process.StartInfo.Arguments = path;
                process.Start();
                Console.WriteLine("The created xml file should now be opened on your Chrome browser.");
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
