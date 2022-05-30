using System;
using System.Linq;

namespace Test_Hyunsoo_Ha
{
    // this partial class contains the main running method and methods for menu #1, 2, 3, 4, 5, as well as some additional methods that I added.
    partial class Functions
    {
        public static string[] menu_options = new string[] {"1. Print out Hello World", "2. Enter user information and print",
        "3. Change the color of texts and background", "4. Print out the date", "5. Enter two numbers and find which one is bigger",
        "6. Generate a random number and guess it", "7. Create a text file", "8. Read a text file (from #7)",
        "9. Get a square root and exponents", "10. Print a multiplication table from 1 to 10",
        "11. Create two arrays and fill one with random numbers, the other with sorted random numbers", "12. Check for a palindrome",
        "13. Find all numbers between two numbers", "14. Enter a sequence and get sorted odd and even numbers", "15. Get a sum of all input numbers",
        "16. Create characters", "17. Exit"};

        Random random = new Random();

        public void Run()
        {
            int menu_selected = 0;

            // enter loop for the menu selection

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

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
                        case 0: // 1. Print out Hello World
                            print_Hello();
                            break;
                        case 1: // 2. Enter user information and print
                            get_user_info();
                            break;
                        case 2: // 3. Change the color of texts and background
                            change_color();
                            break;
                        case 3: // 4. Print out the date
                            get_date();
                            break;
                        case 4: // 5. Enter two numbers and find which one is bigger
                            compare_numbers();
                            break;
                        case 5: // 6. Generate a random number and guess it
                            guess_number();
                            break;
                        case 6: // 7. Create a text file
                            create_textfile();
                            break;
                        case 7: // 8. Read a text file (from #7)
                            read_textfile();
                            break;
                        case 8: // 9. Get a square root and exponents
                            get_root_exponents();
                            break;
                        case 9: // 10. Print a multiplication table from 1 to 10
                            get_multiplication_table();
                            break;
                        case 10: // 11. Create two arrays and fill one with random numbers, the other with sorted random numbers
                            create_two_arrays();
                            break;
                        case 11: // 12. Check for a palindrome
                            check_palindrome();
                            break;
                        case 12: // 13. Find all numbers between two numbers
                            find_between_two_nums();
                            break;
                        case 13: // 14. Enter a sequence and get sorted odd and even numbers
                            sort_odd_even();
                            break;
                        case 14: // 15. Get a sum of all input numbers
                            get_sum();
                            break;
                        case 15: // 16. Create characters
                            create_characters();
                            break;
                        case 16: // 17. Exit
                            Console.WriteLine();
                            return;
                    }
                    print_and_wait();
                }
            } // while loop ends
        }

        public void print_Hello()
        {
            Console.WriteLine("Hello World");
        }

        public void get_user_info()
        {
            string first_name;
            string last_name;
            int age;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter your first name: ");
                    first_name = Console.ReadLine();
                    if (!first_name.All(Char.IsLetter))
                    {
                        throw new Exception();
                    }
                    Console.WriteLine("Enter your last name: ");
                    last_name = Console.ReadLine();
                    if (!last_name.All(Char.IsLetter))
                    {
                        throw new Exception();
                    }
                    Console.WriteLine("Enter your age: ");
                    age = Convert.ToInt32(Console.ReadLine());
                    if (age < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter from the beginning.\n");
                }
            }
            Console.WriteLine("\nHello, " + first_name + " " + last_name + "!\nYou are currently " + age + " year(s) old.");
        }

        public void change_color()
        {
            string color_input;
            string font_color;
            string background_color;
            string[] color_options = new string[] { "red", "magenta", "yellow", "cyan", "green", "blue", "white", "black", "gray" };
            if (Console.ForegroundColor != ConsoleColor.Gray || Console.BackgroundColor != ConsoleColor.Black)
            {
                Console.ResetColor();
                Console.WriteLine("It is back to gray on black now.");
            }
            else
            {
                Console.WriteLine("We assume that the default foreground color is gray and background color is black.\n");
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Choose a color for font and background from below. Write in the form of \"Fontcolor on Backgroundcolor\"");
                        for (int i = 0; i < color_options.Length; i++)
                        {
                            Console.Write(color_options[i] + " ");
                        }
                        Console.WriteLine();
                        color_input = Console.ReadLine().ToLower();
                        string[] division = color_input.Split(' ');
                        if ((Array.IndexOf(color_options, division[0]) > -1) && (Array.IndexOf(color_options, division[2]) > -1))
                        {
                            // if both colors exist in the options
                            font_color = char.ToUpper(division[0][0]) + division[0].Substring(1);
                            background_color = char.ToUpper(division[2][0]) + division[2].Substring(1);
                            if (font_color == background_color)
                            { // user cannot read 
                                throw new ArgumentException();
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("You chose the same color for both. You won't be able to read! Try again.\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Please enter again.\n");
                    }
                }

                // change, using inputs
                if (Enum.TryParse(background_color, out ConsoleColor background))
                {
                    Console.BackgroundColor = background;
                }
                if (Enum.TryParse(font_color, out ConsoleColor foreground))
                {
                    Console.ForegroundColor = foreground;
                }

                Console.WriteLine(font_color + " on " + background_color);
                Console.WriteLine("Now the colors have changed!");
            }
        }

        public void get_date()
        {
            DateTime date = DateTime.Today;
            Console.WriteLine("It is " + date.ToString("dd/MM/yyyy") + " today.");
        }

        public void compare_numbers()
        {
            int num1;
            int num2;
            Console.WriteLine("You can enter two numbers and check which one is bigger.");
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the first number: ");
                    num1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the second number: ");
                    num2 = Convert.ToInt32(Console.ReadLine());
                    if (num1 == num2)
                    {
                        throw new ArgumentException();
                    }
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("They are same number. Try again.\n");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter from the beginning.\n");
                }
            }
            Console.WriteLine("\n" + Math.Max(num1, num2) + " is bigger than " + Math.Min(num1, num2));
        }

        


        // additional methods for running the program

        public int[] get_sequence()
        { // this method is added for function 15 and 16
            int[] nums;
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter numbers. Use comma to separate each number.");
                    // lambda. To get rid of white space. This way, user may enter with or without space.
                    String input = new String(Console.ReadLine().ToCharArray().Where(x => !Char.IsWhiteSpace(x)).ToArray());
                    nums = input.Split(',').Select(Int32.Parse).ToArray();
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter again.");
                }
            }
            return nums;
        }

        public int[] quick_sort_array(int[] nums, int left, int right)
        { // I chose to implement quick sort among many sorting methods.
            int i = left;
            int j = right;
            int pivot = nums[i];

            while (i <= j)
            {
                while (nums[i] < pivot)
                {
                    i++;
                }
                while (nums[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                    i++;
                    j--;
                }
            }
            if (left < j)
                quick_sort_array(nums, left, j);
            if (i < right)
                quick_sort_array(nums, i, right);

            return nums;
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

        public void print_and_wait()
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
            Console.WriteLine("Welcome to the function simulator!");
            Console.WriteLine("To continue, press any button.");
            Console.ReadKey();

            var my_functions = new Functions();
            my_functions.Run();

            Console.WriteLine("We hope you enjoyed our simulator!");
            Console.Write("Press any key to terminate . . . ");
            Console.ReadKey(true);
        }
    }
}