using System;
using System.Text;


namespace Numerals
{
    class Numeral
    {
        public static readonly int RomanLimitLower = 1;
        public static readonly int RomanLimitUpper = 3999;

        private static readonly char[][] RomanSymbolTable; // symbol table for conversion

        static Numeral()
        {
            // initialize symbol table
            RomanSymbolTable = new char[4][];

            RomanSymbolTable[0] = new char[2] { 'I', 'V' };
            RomanSymbolTable[1] = new char[2] { 'X', 'L' };
            RomanSymbolTable[2] = new char[2] { 'C', 'D' };
            RomanSymbolTable[3] = new char[1] { 'M',     };
        }


        private int _number;

        private string _arabic;
        private string _roman;


        public int Number {
            get { return _number; }

            set
            {
                _arabic = null;
                _roman = null;

                _number = value;
            }
        }

        public string Arabic
        {
            get
            {
                if (null != _arabic) { return _arabic; }

                _arabic = Convert.ToString(_number);

                return _arabic;
            }
        }

        public string Roman
        {
            get
            {
                if (null != _roman) {
                    return _roman;
                }
                if ((_number < RomanLimitLower) || (_number > RomanLimitUpper)) {
                    return null;
                }

                StringBuilder stringBuilder = new StringBuilder();

                int digitCount = (int)Math.Floor(Math.Log10(Math.Abs(_number)) + 1);

                // traverse number by digit from left to rigth (MSD to LSD)
                for (int digitIndex = digitCount-1; digitIndex >= 0; --digitIndex)
                {
                    int digit = Math.Abs(_number) / (int)Math.Pow(10, digitIndex) % 10;
                    if (0 == digit) {
                        continue;
                    }

                    if ((1 <= digit) && (digit <= 3))
                    {
                        stringBuilder.Append(RomanSymbolTable[digitIndex][0], digit);
                    }
                    else if (4 == digit)
                    {
                        stringBuilder.Append(RomanSymbolTable[digitIndex][0]);
                        stringBuilder.Append(RomanSymbolTable[digitIndex][1]);
                    }
                    else if ((5 <= digit) && (digit <= 8))
                    {
                        stringBuilder.Append(RomanSymbolTable[digitIndex][1]);
                        stringBuilder.Append(RomanSymbolTable[digitIndex][0], digit % 5);
                    }
                    else if (9 == digit)
                    {
                        stringBuilder.Append(RomanSymbolTable[digitIndex][0]);
                        stringBuilder.Append(RomanSymbolTable[digitIndex+1][0]);
                    }
                }

                _roman = stringBuilder.ToString();

                return _roman;
            }
        }
    }
}
