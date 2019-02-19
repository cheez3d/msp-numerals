using System;
using System.IO;


namespace Numerals
{
    class Program
    {
        private static readonly TextReader TextReaderDefault = Console.In;
        private static readonly TextWriter TextWriterDefault = Console.Out;


        static void Main()
        {
            Console.WriteLine("ARABIC TO ROMAN NUMERAL CONVERTER");
            Console.WriteLine();


            Console.WriteLine("Input file name to read from or press ENTER for manual entry.");
            string inputFileName = Console.ReadLine();

            TextReader textReader;
            if (!String.IsNullOrEmpty(inputFileName))
            {
                try
                {
                    textReader = File.OpenText(inputFileName);
                }
                catch
                {
                    textReader = TextReaderDefault;

                    Console.WriteLine("Could not open file for reading.");
                }

                Console.WriteLine();
            }
            else
            {
                textReader = TextReaderDefault;
            }

            Console.WriteLine("Input file name to write to or press ENTER to skip writing to file.");
            string outputFileName = Console.ReadLine();

            TextWriter textWriter;
            if (!String.IsNullOrEmpty(outputFileName))
            {
                try
                {
                    textWriter = File.CreateText(outputFileName);
                }
                catch
                {
                    textWriter = TextWriterDefault;

                    Console.WriteLine("Could not open file for writing.");
                }

                Console.WriteLine();
            }
            else
            {
                textWriter = TextWriterDefault;

                Console.WriteLine("Skipping writing to file.");
                Console.WriteLine();
            }


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
                    if (textReader == Console.In)
                    {
                        Console.WriteLine();
                    }

                    break;
                }
                
                if (textReader != Console.In)
                {
                    Console.Write(line + " -> ");
                }

                if (!System.Int32.TryParse(line, out int number))
                {
                    Console.WriteLine("Input is malformed or too large.");

                    goto end;
                }

                numeral.Number = number;

                if (null == numeral.Roman)
                {
                    Console.WriteLine("Number cannot be converted.");
                    Console.WriteLine($"Only numbers between {Numeral.RomanLimitLower} and {Numeral.RomanLimitUpper} can be converted.");

                    goto end;
                }

                Console.WriteLine(numeral.Roman);
                if (textWriter != Console.Out)
                {
                    textWriter.WriteLine(numeral.Roman);
                }

            end:
                Console.WriteLine();
            }

            if (textReader != Console.In)
            {
                textReader.Close();
            }

            if (textWriter != Console.Out)
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
