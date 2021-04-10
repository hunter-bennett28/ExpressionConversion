/* CSVFile.cs
 * Author:  Hunter Bennett, Connor Black, James Dunton
 * Desc:    This class opens and deserializes a CSV file.
 */

using System;
using System.Collections.Generic;
using System.IO;

namespace Project2_Group_17
{
    public static class CSVFile
    {
        /// <summary>
        /// Parses a CSV at given file path. Expects file to have lines containing
        /// a serial/expression number and an infix notation expression on each line.
        /// Returns null if read fails or if no valid expressions found this way.
        /// Ex. 1,1+2
        /// </summary>
        /// <param name="filePath">A file path to a CSV file</param>
        /// <returns>A List of pairs containing the serial/expression number and the expression</returns>
        public static List<InfixExpression> CSVDeserialize(string filePath)
        {
            List<InfixExpression> expressions = new List<InfixExpression>();
            try
            {
                // Get an array of each line in the designated file
                string fileContents = File.OpenText(filePath).ReadToEnd();
                string[] fileLines = fileContents.Split('\n');
                for(int i = 1; i < fileLines.Length; ++i) // skip first header line
                {
                    // Get sno and expression and save to list
                    string[] parts = fileLines[i].Split(',');
                    expressions.Add(new InfixExpression(int.Parse(parts[0]), parts[1].Trim()));
                }

                // If no expressions found, return null for easy failure checking
                return expressions.Count > 0 ? expressions : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing {filePath}: {ex.Message}");
                return null;
            }
        }
    }
}
