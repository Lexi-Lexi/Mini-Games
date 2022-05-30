using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Test_Hyunsoo_Ha
{
    // this partial class contains the main running method and methods for menu #6, 7, 8, 9, 10, 11
    partial class Functions
    {
        public void guess_number()
        {   // Note that invalid inputs are not counted as a try.

            int guess = 0; // user input
            int count = 0;
            int number = random.Next(1, 101);

            Console.WriteLine("Guess the number!");
            Console.WriteLine("You need to guess a number between 1 and 100.\n");

            do
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Write a number.");
                        guess = Convert.ToInt32(Console.ReadLine());
                        if (guess < 1 || guess > 100)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Hey! It should be between 1 and 100!");
                    }
                    catch
                    {
                        Console.WriteLine("This is not a number. Try again.");
                    }
                }

                // invalid inputs are not counted as a try.

                count++;

                if (guess < number)
                {
                    Console.WriteLine("Your number is too small. You guessed wrong.");
                    if (number - guess <= 3)
                    {
                        Console.WriteLine("But you're pretty close! Try something bigger.");
                    }
                }
                else if (guess > number)
                {
                    Console.WriteLine("Your number is too big. You guessed wrong.");
                    if (guess - number <= 3)
                    {
                        Console.WriteLine("But you're pretty close! Try something smaller.");
                    }
                }
                else
                {
                    Console.WriteLine("\nCorrect! You're good!");
                    break;
                }

                if (count == 5)
                {
                    Console.WriteLine("You have tried 5 rounds, but have yet to guess it right.\n" +
                        "A piece of advice? Next time, try a number in the middle of the given range!\n");
                }
            } while (true);
            Console.WriteLine("It took " + count + " rounds to get the answer.");
        }


        public void create_textfile()
        {
            string file_path = "";
            Console.WriteLine("You can create a text file here. You may find the file in C:\\Temp");
            Console.WriteLine();

            // get file name
            while (true)
            {
                Console.WriteLine("Enter a name for this file. Note that only letters, numbers, and underscores are allowed.");
                String name_input = Console.ReadLine();
                Regex rex = new Regex(@"^[a-zA-Z0-9_]+$");

                if (File.Exists(@"C:\Temp\" + name_input + ".txt"))
                {
                    Console.WriteLine("A file with the same name exists in the folder. Try again.");
                }
                else if (!rex.IsMatch(name_input))
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
                else
                {
                    file_path = name_input;
                    break;
                }
            }
            file_path = @"C:\Temp\" + file_path + ".txt";
            // Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), file_name)

            using (StreamWriter writer = File.CreateText(file_path))
            {
                Console.WriteLine("\nEnter a sentence here.");
                writer.WriteLine(Console.ReadLine());
            }

            Console.WriteLine("\nFile created! Check your C:\\Temp");

        }

        public void read_textfile()
        {
            int choose = 0;
            Console.WriteLine("You can read a text file you created earlier here. The file(s) exist in C:\\Temp\n");
            string[] files = Directory.GetFiles(@"C:\Temp\", "*.txt");
            int i = 1;
            if (files[0] == null)
            {
                Console.WriteLine("There is no file to read. You need to create a file first.");
            }
            else
            {
                Console.WriteLine("Following is a list of files that you have created.");
                foreach (var file in files)
                {
                    Console.WriteLine("#" + i + "  " + file);
                    i++;
                }
            }
            Console.WriteLine();
            while (true)
            {
                try
                {
                    Console.Write("Enter a file number to choose: ");
                    choose = Convert.ToInt32(Console.ReadLine());
                    if (choose > files.Length || choose < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid number. Check the number of files.");
                }
                catch
                {
                    Console.WriteLine("Invalid input. Enter a number.");
                }
            }
            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(files[choose-1]));
        }


        public void get_root_exponents()
        {
            decimal input;
            Console.WriteLine("You can get square root of a number N, N to the 2nd power, N to the 10th power.\n");
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a decimal number: ");
                    input = Convert.ToDecimal(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid input. Enter a decimal number.");
                }
            }
            Console.WriteLine();
            if (input < 0)
            {
                Console.WriteLine("Square root: Cannot calculate from a negative number");
            }
            else
            {
                Console.WriteLine("Square root: " + (decimal)Math.Sqrt((double)input));
            }
            // As I am converting decimal to double. The result is not 100% accurate.
            // I wrote this assuming that we don't deal with extreme ends of the decimal range...
            Console.WriteLine("To the 2nd power: " + (decimal)Math.Pow((double)input, 2));
            Console.WriteLine("To the 10th power: " + (decimal)Math.Pow((double)input, 10));
        }

        public void get_multiplication_table()
        {
            Console.WriteLine("We create a 10x10 multiplication table here.\n");
            Console.WriteLine("Press any button to view.\n");
            Console.ReadKey();

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {

                    Console.Write(i + "x" + j + "=" + i * j + "\t");
                }
                Console.Write("\n");
            }
        }

        public void create_two_arrays()
        {   // size set to 20, range of number set to 1~100

            Console.WriteLine("We create an array of randomly generated integers [1-100] and sort it in ascending order here.\n");

            int[] random_array = new int[20];
            Console.WriteLine("The randomly created array is: ");
            for (int i = 0; i < random_array.Length - 1; i++)
            {
                random_array[i] = random.Next(1, 101);
                Console.Write(random_array[i] + " ");
            }

            Console.WriteLine("\n\nPress any button to sort it in ascending order.");
            Console.ReadKey();

            int[] sorted_array = quick_sort_array(random_array, 0, random_array.Length - 1);
            Console.WriteLine("\nIt is now sorted: ");

            for (int i = 0; i < sorted_array.Length - 1; i++)
            {
                Console.Write(sorted_array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}