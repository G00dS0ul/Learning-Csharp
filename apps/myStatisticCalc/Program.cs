
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myStatisticCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] previousData = null; //store the previous data
            while (true)
            {
                Console.Write("Enter Your Data: ");
                string input = Console.ReadLine();

                double[] data;
               
                //Condition to exit
                if (input == "done" || string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Program Terminate");
                    break;
                }
                if (input == "recall")
                {
                    if (previousData == null)
                    {
                        Console.WriteLine( "No previous Data to recall");
                        continue;
                    }
                    else
                    {
                        data = previousData;
                        Console.WriteLine("Previous Data Inputed is: " + string.Join(" ", data));
                    }
                        
                }
                else
                {
                    //allow user to input multiple data seperated by a space and spliting it into an array and empty entries are removed
                    string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    data = new double[parts.Length];
                    //the new data created is then converted to a double and if the conversion is unsuccefull the value will result 0
                    for (int i = 0; i < parts.Length; i++)
                    {
                        double.TryParse(parts[i], out data[i]);
                    }
                    previousData = data; //save current data as previous
                }
                    
                //allow user to input the type of statistic operation to do 
                Console.Write("Enter your Operation: ");
                string opdata = Console.ReadLine();
                //allow user to pass more than one operation also seperated by a space
                string[] inputop = opdata.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                //used the switch method to check which kind of operation to execute in order and called up the method to execute on terminal

                var statCalc = new StatisticCalc();
                foreach (string op in inputop)
                {
                    switch (op)
                    {
                        case "sum":
                            statCalc.Sum = StatisticCalc.SumOfData(data);
                            Console.WriteLine("The Sum of the Data is: " + statCalc.Sum);
                            break;
                        case "avg":
                            statCalc.Avg = StatisticCalc.MeanOfData(data);
                            Console.WriteLine("The Mean of the Data is: " + statCalc.Avg);
                            break;
                        case "min":
                            statCalc.Min = StatisticCalc.MinNumber(data);
                            Console.WriteLine("The Minimum Value of the Data is: " + statCalc.Min);
                            break;
                        case "max":
                            statCalc.Max = StatisticCalc.MaxNumber(data);
                            Console.WriteLine("The Maximum Value of the Data is: " + statCalc.Max);
                            break;
                        case "mode":
                            double[] modes = StatisticCalc.ModeOfData(data);
                            Console.WriteLine("The Highest frequency of the Data is: " + string.Join(" ", modes));
                            break;
                        case "var":
                            statCalc.Var = StatisticCalc.VarianceOfData(data);
                            Console.WriteLine("The Sample variance of the data is: " + statCalc.Var);
                            break;
                        case "SD":
                            statCalc.SD = StatisticCalc.StandardDeviationOfData(data);
                            Console.WriteLine("The Standard Devition of the data is: " + statCalc.SD);
                            break;
                        case "med":
                            statCalc.Med = StatisticCalc.MedianOfData(data);
                            Console.WriteLine("The Median of the data is: " + statCalc.Med);
                            break;
                    }
                }


            }


        }
      
    }
}
