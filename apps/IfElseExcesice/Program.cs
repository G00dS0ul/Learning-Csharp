namespace IfElseExcesice
{
    class Program
    {

        static void Main(string[] args)
        {
            var speedLimit = 60; // Default SpeedLimit in the program
            Console.Write("Car Speed: "); 
            string carspeed = Console.ReadLine();
            var carSpeed = Convert.ToInt32(carspeed);
            var demeritPoint = 0;
            var overSpeed = 0;

            if (carSpeed < speedLimit)//Driivng at a save range.
            {
                Console.WriteLine("Ok");
            }
            else if (carSpeed > speedLimit) //Accumunilating demerit point
            {
                overSpeed = carSpeed - speedLimit;
                demeritPoint = overSpeed / 5;

                Console.WriteLine("You have a demerit point of " +  demeritPoint);
                if (demeritPoint > 12) //License suspended
                {
                    Console.WriteLine("Your License has been Suspended");
                }
            }
            
        }
    }
}