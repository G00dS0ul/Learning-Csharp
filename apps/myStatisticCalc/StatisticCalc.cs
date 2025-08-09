
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
            //Allow user to input data and store in input
            Console.Write("Enter Your Data: ");
            string input = Console.ReadLine();
            //allow user to input multiple data seperated by a space and spliting it into an array and empty entries are removed
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            double[] data = new double[parts.Length];
            //the new data created is then converted to a double and if the conversion is unsuccefull the value will result 0
            for (int i = 0; i < parts.Length; i++)
            {
                double.TryParse(parts[i], out data[i]);
            }
            //allow user to input the type of statistic operation to do 
            Console.Write("Enter your Statistics Operation: ");
            string opdata = Console.ReadLine();
            //allow user to pass more than one operation also seperated by a space
            string[] inputop = opdata.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //used the switch method to check which kind of operation to execute in order and called up the method to execute on terminal
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
        //Created methods of For-loops to calculate each operation
        //for summation of data method
        static double SumOfData(double[] data)
        {
            double sum = 0;
            for(int i = 0; i <data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        //for average calculation method
        static double MeanOfData(double[] data)
        {
            double mean = 0;
            for (int i = 0; i < data.Length; i++)
            {
                mean += data[i] / data.Length;
            }
            return mean;
        }

        //for minimum value method
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

        //for maximum value method
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
