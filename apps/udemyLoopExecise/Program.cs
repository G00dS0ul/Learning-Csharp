using System;

namespace udemyLoopExecise
{
    class Program
    {
        static void Main(string[] args)
        {
            //initilize the sum to 0
            int sum = 0;
            while (true)
            {
                Console.Write("Enter Your Number or type OK to exit: ");
                var input = Console.ReadLine();
                var exitLoop = "OK"; 
                if(input == exitLoop)  //Condition to exit the loop
                {
                    break;
                }
                
                if (int.TryParse(input, out var number))
                {
                    sum += number;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please enter a valid number or enter OK to exit: ");
                }
                
            }
            Console.WriteLine($"Sum of number entered: {sum}");

                
            
        }
    }
}
