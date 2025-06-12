using System;

namespace Malshinon2._0
{
    internal class Menu
    {
        public void Run()
        {
            int choice = 0;

            while (choice != 5)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1 - First action");
                Console.WriteLine("2 - Second action");
                Console.WriteLine("3 - Third action");
                Console.WriteLine("4 - Fourth action");
                Console.WriteLine("5 - Exit");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please try again.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You chose option 1.\n");
                        break;
                    case 2:
                        Console.WriteLine("You chose option 2.\n");
                        break;
                    case 3:
                        Console.WriteLine("You chose option 3.\n");
                        break;
                    case 4:
                        Console.WriteLine("You chose option 4.\n");
                        break;
                    case 5:
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
    }
}