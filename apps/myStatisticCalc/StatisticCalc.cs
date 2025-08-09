
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myStatisticCalc
{
    class StatisticCalc
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Your Data: ");
            string input = Console.ReadLine();
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            double[] data = new double[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                double.TryParse(parts[i], out data[i]);
            }

            Console.Write("Enter your Statistics Operation: ");
            string opdata = Console.ReadLine();
            string[] inputop = opdata.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach(string op in inputop)
            {
                switch (op)
                {
                    case "sum":
                        Console.WriteLine("The Sum of the Data is: " + SumOfData(data));
                        break;
                    case "avg":
                        Console.WriteLine("The Mean of the Data is: " + MeanOfData(data));
                        break;
                    case "min":
                        Console.WriteLine("The Minimum Value of the Data is: " + MinNumber(data));
                        break;
                    case "max":
                        Console.WriteLine("The Maximum Value of the Data is: " + MaxNumber(data));
                        break;
                }
            }





            Console.ReadLine();
        }

        static double SumOfData(double[] data)
        {
            double sum = 0;
            for(int i = 0; i <data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }
        static double MeanOfData(double[] data)
        {
            double mean = 0;
            for (int i = 0; i < data.Length; i++)
            {
                mean += data[i] / data.Length;
            }
            return mean;
        }
        static double MinNumber(double[] data)
        {
            double min = data[0];
            for(int i = 0; i < data.Length; i++)
            {
                if (min < data[i])
                {
                    min = data[i];
                }
            }
            return min;
        }
        static double MaxNumber(double[] data)
        {
            double max = data[0];
            for (int i = 0; i < data.Length; i++)
            {
                if (max > data[i])
                {
                    max = data[i];
                }
            }
            return max;
        }
    }
}
