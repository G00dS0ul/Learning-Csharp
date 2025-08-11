using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random(); 
            int randomNumber = random.Next(1, 11);  //Numbers from 1 to 10
            var guessCount = 0;
            var guessLimit = 4;
            bool outOfChance = false;
            int guess = 0;

            Console.WriteLine("Welcome To my Guessing Game:");
            Console.WriteLine($"You have {guessLimit} Chances.");
            Console.WriteLine($"Supposed Random Number is {randomNumber}");  //To confirm the given random number

            do
            {
                if (guessCount < guessLimit)
                {
                    Console.Write("Enter your Guess: ");
                    var input = Console.ReadLine();
                    //confirm if the input passes can be converted to an integer
                    if (!int.TryParse(input, out guess))
                    {
                        Console.WriteLine("Please enter a valid number.");
                        continue;
                    }
                    guessCount++;
                    int guessLeft = guessLimit - guessCount;

                    if (guess != randomNumber && guessCount < guessLimit)
                    {
                        Console.WriteLine($"You have {guessLeft} left!!!");
                    }
                }
                else
                {
                    outOfChance = true;
                }
            }
            while (guess != randomNumber && !outOfChance);
            if (outOfChance)
            {
                Console.WriteLine("You are out of Chance!!! The Correct Number is: " + randomNumber);
            }
            else
            {
                Console.WriteLine("You Got it!");
            }
        }
    }
}

