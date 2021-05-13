using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDOL_CaseStudy
{
    class Program
    {
        public static void Main(string[] args)
        {
            ISedolValidator sedolValidator = new SedolValidator();
            //ISedolValidationResult sedolValidationResult = new SedolValidationResult();
            string SEDOLVal;

            Console.WriteLine("Please enter a SEDOL string:");
            SEDOLVal = Console.ReadLine();

            ISedolValidationResult sedolValidationResult = sedolValidator.ValidateSedol(SEDOLVal);

            Console.WriteLine("InputString Test Value | IsValidSedol | IsUserDefined | ValidationDetails");
            Console.WriteLine("{0} | {1} | {2} | {3} ", sedolValidationResult.InputString, sedolValidationResult.IsValidSedol, sedolValidationResult.IsUserDefined, sedolValidationResult.ValidationDetails);
            Console.ReadLine();
            //string input = "9ABCDE1";
            //char[] ch = new char[input.Length];
            //for (int i = 0; i < input.Length; i++)
            //{
            //    ch[i] = input[i];
            //}
            //for (int i = 0; i < ch.Length; i++)
            //{
            //    int n;
            //    Console.WriteLine(ch[i]);
            //    if (Char.IsDigit(ch[i]))
            //        n = (int)Char.GetNumericValue(ch[i]);
            //    else
            //        n = char.ToUpper(ch[i]) - 55;
            //    Console.WriteLine(n);

            //}
            //Console.ReadLine();
        }
    }
}
