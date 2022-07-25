using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Arena_Fighter
{
    class Arena
    {
        public static string[] menu_options = new string[] {"1. Manual for a new fighter", "2. Start a battle",
            "3. Check status", "4. Alchemy shop", "5. Armory", "6. Retire"};

        /* Crystal - Currency for Alchemy shop and Armory.
        start with 50 crystal. 100 crystal for each win. 20 crystal bonus for battles with the number of attacks exchanged 13+ times. */
        public static int crystal = 50; // needed only for player. start with 50
        public static int win_num = 0;
        public static bool dead = false;

        public static Character my_character = new Character(select_name());
        public static Alchemy my_alchemy = new Alchemy();
        public static Armory my_armory = new Armory();
        public static Battle my_battle;

        static bool manual_read = false;
        static List<Battle> my_battle_record = new List<Battle>(); // dynamic

        // main program
        public void run()
        {
            int menu_selected = 0;

            // enter loop for the menu selection
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                if (dead)
                {
                    break;
                }
                // if-statements for showing the cursor

                print_menu(menu_selected);
                var keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.DownArrow && menu_selected != menu_options.Length - 1)
                { // if user is pressing down arrow and the cursor has not reached the end of the options array,
                    menu_selected++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && menu_selected >= 1)
                { // if user is pressing up arrow and the cursor has not reached the start of the options array,
                    menu_selected--;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                { // if user makes a decision
                    Console.WriteLine();
                    switch (menu_selected)
                    {
                        case 0: // 1. Instructions for a new fighter
                            print_manual();
                            break;
                        case 1: // 2. Start a battle
                            if (!manual_read)
                            {
                                Console.WriteLine("Read the manual first unless you want to die at your first battle.");
                            }
                            else
                            {
                                my_battle = new Battle();
                                if (win_num >= 50 && win_num < 60)
                                {
                                    Console.WriteLine("You already have become the living legend of Arena.\nYou are getting old and your body may not last the next battle.");
                                    Console.WriteLine("Think wisely. Retire soon.");
                                    my_battle.fight();
                                    my_battle_record.Add(my_battle);

                                }
                                else if (win_num >= 60)
                                {
                                    Console.WriteLine("Make space for younger gladiators. You may not enter any more battles.\nTime to retire.");
                                    retire();

                                }
                                else // 0 ~ 49 wins 
                                {
                                    my_battle.fight();
                                    my_battle_record.Add(my_battle);
                                }
                            }
                            if (dead)
                            {
                                death_retire();
                            }
                            break;
                        case 2: // 3. Check status
                            /* Check your current health and purchase potions to recover for the next battle.*/
                            Console.WriteLine(my_character.get_stats());
                            break;
                        case 3: // 4. Alchemy shop
                            my_alchemy.enter();
                            break;
                        case 4: // 5. Armory
                            my_armory.enter();
                            break;
                        case 5: // 6. Retire
                            if (win_num == 0)
                            {
                                Console.WriteLine("A gladiator only gets to retire after a glorious victory.");
                                break;
                            }
                            else
                            {
                                retire();
                                return;
                            }
                    }

                    print_and_wait();
                }
            } // while loop ends
        }

        
        public void print_manual()
        {
            string text = "";

            // manual.txt should be located in the project's root directory 
            TextReader tr = new StreamReader(@"manual.txt");
            text = tr.ReadToEnd();

            Console.WriteLine(text);

            manual_read = true;
        }

        public static string select_name()
        {
            // create user character here
            string input_name;
            while (true)
            {
                Console.WriteLine("Choose your name");
                input_name = Console.ReadLine();
                if (Character.enemy_names.Contains(input_name)) // there is a list of possible enemy names. compare with it.
                {
                    Console.WriteLine("This name is already taken.\n");
                }
                else if (input_name == "") // only check empty string
                {
                    Console.WriteLine("Hey, you need a name!");
                }
                else
                {
                    Console.WriteLine("Your name is " + input_name + ". Do you confirm? y or n");
                    string input_confirm = Console.ReadLine();
                    if (input_confirm == "y" || input_confirm == "Y")
                    {
                        break;
                    }
                    else if (input_confirm == "n" || input_confirm == "N")
                    {
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter from beginning.\n");
                    }
                }
            }
            Console.WriteLine("\nWelcome, " + input_name + "! Will you be the next legend? We will see...");
            Arena.print_and_wait();
            Console.Clear();

            return input_name;
        }

        public void retire()
        {
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * *\n");
            if (win_num == 1)
            {
                Console.WriteLine("{0} win | Beginner's luck? You survived the first fight but decided to leave this Arena. Some may say it was the best decision of yours.", win_num);
            }
            else if (win_num >= 2 && win_num < 15)
            {
                Console.WriteLine("{0} | You retire as a modest gladiator. Surviving is what matters.", win_num);
            }
            else if (win_num >= 15 && win_num < 35)
            {
                Console.WriteLine("{0} | You have made the Arena of Gory and Glory gorier and more glorious. Your retirement is celebrated by some.", win_num);
            }
            else if (win_num >= 35 && win_num < 50)
            {
                Console.WriteLine("{0} | You are the best gladiator of your generation. Many people will remember your name.", win_num);
            }
            else
            { // // 50 - 70
                Console.WriteLine("{0} | You are the greatest gladiator this Arena has ever seen." +
                    "\nYour name will remain in history and people will remember your name for hundreds of years.", win_num);
            }
            Console.WriteLine("\n* * * * * * * * * * * * * * * * * * * * * * * *\n");
            view_log();
        }

        // retired by death
        public static void death_retire()
        {
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * *\n");
            if (win_num == 0)
            {
                Console.WriteLine("You died at your first battle. Your body is burned along with other novice gladiators with unmemorable deaths.");
            }
            else if (win_num == 1)
            {
                Console.WriteLine("{0} win | Beginner's luck? You survived the first fight but that was all for you. You die as a no-name gladiator.", win_num);
            }
            else if (win_num >= 2 && win_num < 15)
            {
                Console.WriteLine("{0} wins | You die as a modest gladiator. Some may say that you were just about to bloom.", win_num);
            }
            else if (win_num >= 15 && win_num < 35)
            {
                Console.WriteLine("{0} wins | You have made the Arena of Gory and Glory gorier and more glorious. Your grave is visited by some old fans.", win_num);
            }
            else if (win_num >= 35 && win_num < 50)
            {
                Console.WriteLine("{0} wins | You proved yourself to be the best gladiator of your generation. Many people will remember your name." +
                    "It is a shame that you did not leave when you were ahead.", win_num);
            }
            else
            { // 50 - 70
                Console.WriteLine("{0} | You were the greatest gladiator this Arena has ever seen." +
                    "\nYou embraced your death at the place you shined most." +
                    "\nYour name will remain in history and people will remember your name for hundreds of years.", win_num);
            }
            Console.WriteLine("\n* * * * * * * * * * * * * * * * * * * * * * * *\n");
            view_log();

        }

        public static void view_log()
        {
            int which_battle = 0;
            Console.WriteLine("Press any key to view your battle logs.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Your score is {0} based on the number of victories.\n", win_num);
            while (true)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter the battle number [1-{0}] to view the log. Enter 0 to exit.", Battle.battle_num);
                        which_battle = Convert.ToInt32(Console.ReadLine());
                        if (which_battle == 0)
                        {
                            break;
                        }
                        else if (which_battle < 0 || which_battle > my_battle_record.Count())
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Such battle number does not exist. Enter again.\n");
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input. Enter again.\n");
                    }
                }
                if (which_battle == 0)
                {
                    break;
                }
                Console.WriteLine("\n* Luck is reflected on dice. Str, Dex, and gears are reflected on final damage.");
                Console.WriteLine(my_battle_record.ElementAt(which_battle - 1).read_battle_log(which_battle));


            }
        }

        public void print_menu(int menuSelected)
        { // this method is added so that the menu printing codes don't get too long.
            for (int i = 0; i < menu_options.Length; i++)
            {
                Console.Write(menu_options[i]);
                if (i == menuSelected)
                {
                    Console.Write(" <--");
                }
                Console.Write("\n");
            }
        }

        public static void print_and_wait()
        { // this method is added so that Console.Clear() in the menu selection does not clear up all the messages right away.
            Console.WriteLine();
            Console.WriteLine("Press any button to go back to menu.");
            Console.ReadKey();
        }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * *");
            Console.WriteLine("WELCOME TO ARENA OF GORY AND GLORY");
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * *\n");
            Console.WriteLine("To continue, press any button.");
            Console.ReadKey();
            Console.WriteLine();

            var my_arena = new Arena();
            my_arena.run();

            Console.WriteLine("We hope you enjoyed Arena of Gory and Glory!");
            Console.Write("Press any key to terminate . . . ");
            Console.ReadKey(true);

        }
        
    }
}