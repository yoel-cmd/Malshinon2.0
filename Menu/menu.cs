﻿using System;
using Malshinon2._0.MalshinonDAL;

namespace Malshinon2._0
{
    internal class Menu
    {
        public void Run()
        {

            
            servis servis = new servis();
            int choice = 0;

            while (choice != 4)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1 - To informent");
                Console.WriteLine("2 - To view potential agents (if any exist) ");
                Console.WriteLine("3 - To see dangerous targets");           
                Console.WriteLine("4 - Exit");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        servis.run();
                        break;
                    case 2:
                        servis.reternPotentialAgent();                        
                        break;
                    case 3:
                        servis.returnRiskPeopel();
                        
                        break;
                    case 4:
                        servis.Exit();                        
                        break;
                   
                }
            }
        }
    }
}