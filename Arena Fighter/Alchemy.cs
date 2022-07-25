using System;
namespace Arena_Fighter
{
    class Alchemy : Shop
    {
        /* Small potion (10 crystal) - restore health by 5
        Potion (20 crystal) - restore health by 15
        Large potion (30 crystal) - restore health by 25
        Elixir (50 crystal) - recover completely
        Lucky Charm (300 crystal) - increase luck level by 1
        Tabula Rasa (100 crystal) - reassign a random value [1-6] for strength or dexterity */

        private string[,] product_list = new string[6, 3] {{"Small Potion", "5 Health Recovery", "10 Crystal"}, {"Potion", "15 Health Recovery", "20 Crystal"},
            {"Large Potion", "25 Health Recovery", "30 Crystal"}, {"Elixir", "Full Health Recovery", "50 Crystal"},
            {"Lucky Charm", "Increase Luck by 1", "300 Crystal"}, {"Tabula Rasa", "Get a new random value [1-6] for Strength and Dexterity", "100 Crystal"}};

        public void enter()
        {
            // variables
            int choice = 0;
            bool afford = false;
            
            // print items
            Console.WriteLine("Welcome to the Alchemy Shop! You have " + Arena.crystal + " crystal.\n");
            for (int i = 0; i < product_list.GetLength(0); i++)
            {
                Console.WriteLine("#{0}  {1}  |  {2}  |  {3}", i + 1, product_list[i, 0], product_list[i, 1], product_list[i, 2]);
            }

            if (Arena.crystal < 5)
            { // broke
                Console.WriteLine("\nYou do not have enough crystal to purchase anything here. Get out!");
                return;
            }

            // choose an item to purchase
            while (true)
            {   // choose options
                try
                {
                    Console.WriteLine("\nEnter a number [1-6] to purchase. Enter 0 to exit the shop.");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < 0 || choice > 6)
                    { // wrong input
                        throw new Exception();
                    }
                    if (choice == 0)
                    { // exit the shop
                        break;
                    }
                    else if (choice == 2 && Arena.crystal < 15)
                    {
                        throw new ArgumentException();
                    }
                    else if (choice == 3 && Arena.crystal < 25)
                    {
                        throw new ArgumentException();
                    }
                    else if (choice == 4 && Arena.crystal < 50)
                    {
                        throw new ArgumentException();
                    }
                    else if (choice == 5 && Arena.my_character.get_luck() == 6)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    else if (choice == 5 && Arena.crystal < 300)
                    {
                        throw new ArgumentException();
                    }
                    else if (choice == 6 && Arena.crystal < 100)
                    {
                        throw new ArgumentException();
                    }
                    afford = true;
                    break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("You already have Luck level 6.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("You do not have enough crystal to purchase this item. Choose something else.");
                }
                catch
                {
                    Console.WriteLine("Invalid input.Enter again.");
                }
            }

            if (afford)
            {   // purchase action here 
                switch (choice - 1)
                {
                    case 0:
                        small_potion();
                        break;
                    case 1:
                        potion();
                        break;
                    case 2:
                        large_portion();
                        break;
                    case 3:
                        elixir();
                        break;
                    case 4:
                        lucky_charm();
                        break;
                    case 5:
                        tabula_rasa();
                        return;
                }
                Console.WriteLine("Transaction completed.");
            }
            Console.WriteLine("\nGood bye. We hope you continue to do business with us.");

        }

        // items
        public void small_potion()
        {
            Arena.crystal = Arena.crystal - 10;
            Arena.my_character.heal(5);
        }
        public void potion()
        {
            Arena.crystal = Arena.crystal - 20;
            Arena.my_character.heal(15);
        }
        public void large_portion()
        {
            Arena.crystal = Arena.crystal - 30;
            Arena.my_character.heal(25);
        }
        public void elixir()
        {
            Arena.crystal = Arena.crystal - 50;
            Arena.my_character.heal(50);
        }
        public void lucky_charm()
        {
            Arena.crystal = Arena.crystal - 300;
            Arena.my_character.increase_luck();
        }

        public void tabula_rasa()
        {
            Arena.crystal = Arena.crystal - 100;
            string choice = "";
            Console.WriteLine("\nCurrent Strength Level: {0} | Current Dexterity Level: {1}\n",
                Arena.my_character.get_strength(), Arena.my_character.get_dexterity());
            while (true)
            {
                Console.WriteLine("Enter 1 to reset strength, and 2 to reset dexterity.");
                choice = Console.ReadLine();
                if (choice != "1" && choice != "2")
                {
                    Console.WriteLine("Invalid input. Enter again.");
                }
                else
                {
                    break;
                }
            }
            Arena.my_character.stat_reassign(Convert.ToInt32(choice));
        }

    }
}