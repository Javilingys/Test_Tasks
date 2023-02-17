using RomanCaclulatorLib;
using System.Text;

namespace RomanCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICalculator calulcator = new Calculator();

            string answer = calulcator.Evaluate("(MMMDCCXXIV - MMCCXXIX) * II"); // MMCMXC
            //string answer2 = calulcator.Evaluate("(II + II");
            
            Console.WriteLine(answer);
            //Console.WriteLine(answer2);
        }
    }


}