using System;
using System.IO;


namespace Numerals
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ARABIC TO ROMAN NUMERAL CONVERTER");
            Console.WriteLine();


            Console.WriteLine("Input file name to read from or press ENTER for manual entry.");
            string inFileName = Console.ReadLine();

            TextReader textReader;

            if (!String.IsNullOrEmpty(inFileName))
            {
                try { textReader = File.OpenText(inFileName); }
                catch {
                    Console.WriteLine("Could not open file for reading.");

                    textReader = Console.In;
                }
            }
            else { textReader = Console.In; }
            Console.WriteLine();


            Console.WriteLine("Input file name to write to or press ENTER to skip writing to file.");
            string outFileName = Console.ReadLine();

            TextWriter textWriter;

            if (!String.IsNullOrEmpty(outFileName))
            {
                try { textWriter = File.CreateText(outFileName); }
                catch {
                    textWriter = null;
                    Console.WriteLine("Could not open file for writing.");
                }
            }
            else
            {
                textWriter = null;
                Console.WriteLine("Skipping writing to file.");
            }
            Console.WriteLine();


            if (textReader == Console.In)
            {
                Console.WriteLine("Input numbers to be converted.");
                Console.WriteLine("When done, input EOF (Ctrl+Z).");
                Console.WriteLine();
            }

            Numeral numeral = new Numeral();
            
            while (true)
            {
                string line = textReader.ReadLine();
                if (null == line) {
                    if (textReader == Console.In) { Console.WriteLine(); }

                    break;
                }
                
                if (textReader != Console.In) { Console.Write(line + " -> "); }

                if (!System.Int32.TryParse(line, out int number))
                {
                    Console.WriteLine("Input is malformed or too large.");
                    goto finally_;
                }

                numeral.Number = number;

                if (null == numeral.Roman)
                {
                    Console.WriteLine("Number cannot be converted.");
                    goto finally_;
                }

                if (null != textWriter) { textWriter.WriteLine(numeral.Roman); } 
                Console.WriteLine(numeral.Roman);

            finally_:
                Console.WriteLine();
            }
            textReader.Close();


            if (null != textWriter)
            {
                textWriter.Close();
                Console.WriteLine("All conversions written to file.");
                Console.WriteLine();
            }

            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}
