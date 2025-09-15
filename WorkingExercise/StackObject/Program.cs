namespace StackObject
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack();

            Console.WriteLine("Input Your element you want to enter.");

            while (true)
            {
                int initial = 0;
                int max = 5;
                
                for (var i = initial; i <= max; i++)
                {
                    Console.Write("->");
                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("The Element is empty, Input an element");
                        continue;
                    }
                        
                    if (input.Equals("POP", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            Console.WriteLine("Element Popped: {0} ", stack.Pop());
                        }

                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        continue;
                    }

                    if (input.Equals("CLEAR", StringComparison.OrdinalIgnoreCase))
                    {
                        stack.Clear();
                        Console.WriteLine("Stack Cleared!!!");
                        continue;
                    }

                    if (input.Equals("Show", StringComparison.OrdinalIgnoreCase) || stack.Items.Count == max)
                    {
                        Console.WriteLine("Your Stack Includes: ");
                        
                        foreach (var items in stack.Items)
                            Console.WriteLine(items);
                        continue;
                    }

                    stack.Push(input);
                }
                

                
            }
            

           
        }
    }
}