using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace Test_Hyunsoo_Ha
{
    // this partial class contains the main running method and methods for menu #12, 13, 14, 15, 16
    partial class Functions
    {
        public void check_palindrome()
        {
            Console.WriteLine("We can check if a word is palindrome here.");
            String input = "";
            bool match = true;

            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter a word or an expression. Note that only alphabets and space characters are allowed.");
                    input = Console.ReadLine();
                    Regex rex = new Regex("^[a-zA-Z ]+$");
                    if (rex.IsMatch(input))
                    {
                        break;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Enter again.");
                }
            }

            char[] each_letter = input.ToLower().Where(x => !Char.IsWhiteSpace(x)).ToArray(); // lambda

            if (each_letter.Length % 2 == 0)
            { // even number length
                for (int i = 0; i < each_letter.Length; i++)
                {
                    if (each_letter[i] != each_letter[each_letter.Length - 1 - i])
                    {
                        match = false;
                    }
                }
            }
            else 
            { // odd number length
                for (int i = 0; i < each_letter.Length; i++)
                {
                    if (i == (each_letter.Length % 2) - 1) // the center letter doesn't have to be checked
                    {
                        continue;
                    }
                    if (each_letter[i] != each_letter[each_letter.Length -1 - i])
                    {
                        match = false;
                    }
                }
            }

            if (match)
            {
                Console.WriteLine("\n" + input + " is a palinedrome!");
            }
            else
            {
                Console.WriteLine("\n" + input + " is not a palinedrome.");
            }   
        }

        public void find_between_two_nums()
        {
            int num1;
            int num2;
            Console.WriteLine("You can enter two numbers and get all the numbers between them.");
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the first number: ");
                    num1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the second number: ");
                    num2 = Convert.ToInt32(Console.ReadLine());
                    if (Math.Abs(num1 - num2) <= 1)
                    {
                        throw new ArgumentException();
                    }
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("They are same or adjacent number. Try again.\n");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter from the beginning.\n");
                }
            }

            Console.Write("\nNumbers between them are: ");
            if (num1 > num2)
            {
                for (int i = num2 + 1; i < num1; i++)
                {
                    Console.Write(i + " ");
                }
            }
            else
            {
                for (int i = num1 + 1; i < num2; i++)
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine();
        }


        public void sort_odd_even()
        {
            int[] nums = get_sequence();
            quick_sort_array(nums, 0, nums.Length - 1);
            string odds = "";
            string evens = "";
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] % 2 == 1)  // odd
                {
                    odds = odds + nums[i] + " ";
                }
                else
                {
                    evens = evens + nums[i] + " ";
                }
            }
            Console.WriteLine("\nOdd numbers are: " + odds + "\nEven numbers are: " + evens);
        }

        public void get_sum()
        {
            int[] nums = get_sequence();
            int sum = 0;
            for (int i = 0; i< nums.Length; i++)
            {
                sum = sum + nums[i];
            }
            Console.WriteLine("\nThe sum of the sequence is " + sum);
        }


        public void create_characters()
        {
            Character[] slots = new Character[2];
            Console.WriteLine("You can create your own character and your enemy character here. No restrictions for special characters or length!\n");
            string your_name = "";
            string enemy_name = "";
            bool name_set = false;
            string confirm;

            // get names
            while (!name_set)
            {
                Console.WriteLine("Enter a name for your character: ");
                your_name = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("Are you sure you want to name your character " + your_name + "? Enter 'y' or 'n'");
                    confirm = Console.ReadLine().ToLower();
                    if (confirm != "y" && confirm != "n")
                    {
                        Console.Write("Invalid input. Please enter again.");
                    }
                    else if (confirm == "y")
                    {
                        name_set = true;
                        break;
                    }
                    else if (confirm == "n")
                    {
                        break;
                    }
                }
            }
            name_set = false;
            Console.WriteLine();
            while (!name_set)
            {
                Console.WriteLine("Enter a name for enemy character: ");
                enemy_name = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("Are you sure you want to name the enemy character " + enemy_name + "? Enter 'y' or 'n'");
                    confirm = Console.ReadLine().ToLower();
                    if (confirm != "y" && confirm != "n")
                    {
                        Console.WriteLine("Invalid input. Please enter again.");
                    }
                    else if (confirm == "y")
                    {
                        name_set = true;
                        break;
                    }
                    else if (confirm == "n")
                    {
                        break;
                    }
                }
            }

            // randomly add health, strength, dexterity, intelligence, luck
            Console.WriteLine("\nPress any key to add stats for the characters. Each stat is randomly generated [1-100].");
            Console.ReadKey();
            Character your_character = new Character(your_name, random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101));
            Character enemy_character = new Character(enemy_name, random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101), random.Next(1, 101));
            slots[0] = your_character;
            slots[1] = enemy_character;

            Console.WriteLine("\nYour character stats: " + "\n" + your_character.get_stats());
            Console.WriteLine("\nEnemy character stats: " + "\n" + enemy_character.get_stats());
        }
    }

    class Character
    {
        private string name;
        private int health;
        private int mana;
        private int strength;
        private int dexterity;
        private int intelligence;
        private int luck;
        private int charisma;

        // constructor
        public Character(string name, int health, int mana, int strength, int dexterity, int intelligence, int luck, int charisma)
        {
            this.name = name;
            this.health = health;
            this.mana = mana;
            this.strength = strength;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
            this.luck = luck;
            this.charisma = charisma;
        }
        public string get_stats()
        {
            return "Name: " + this.name + ", Health: " + this.health + ", Mana: " + this.mana + ", Strength: " + this.strength +
                ", Dexterity: " + this.dexterity + ", Intelligence: " + this.intelligence + ", Luck: " + this.luck + ", Charisma: " + this.charisma;
        }
    }
}
