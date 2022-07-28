package Arena_Fighter;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

class Arena {
	public static String[] menu_options = new String[] { "1. Manual for a new fighter", "2. Start a battle",
			"3. Check status", "4. Alchemy shop", "5. Armory", "6. Retire" };

	/* Crystal - Currency for Alchemy shop and Armory. start with 50 crystal. 100
	 * crystal for each win. 20 crystal bonus for battles with the number of attacks
	 * exchanged 13+ times. */
	public static int crystal = 50; // needed only for player. start with 50
	public static int win_num = 0;
	public static boolean dead = false;
	

	public static Scanner sc = new Scanner(System.in);
	public static Character my_character = new Character(select_name());
	public static Alchemy my_alchemy = new Alchemy();
	public static Armory my_armory = new Armory();
	public static Battle my_battle;

	static boolean manual_read = false;
	static boolean retire_flag = false;
	static List<Battle> my_battle_record = new ArrayList<Battle>(); // dynamic

	// main program
	public void run() throws IOException {
		int selected_menu = 0;
		boolean menu_selected;
		// enter loop for the menu selection
		while (true) {
			menu_selected = false;
			clear_console();
			if (dead) {
				break;
			}

			print_menu();
			try {
				selected_menu = Integer.parseInt(Arena.sc.nextLine());
				menu_selected = true;
			}
			catch (Exception e){
				System.out.println("Enter a valid input. [1-6]");
			}
			
			if (menu_selected) {
				System.out.println();
				switch (selected_menu - 1) {
				case 0: // 1. Instructions for a new fighter
					print_manual();
					break;
				case 1: // 2. Start a battle
					if (!manual_read) {
						System.out.println("Read the manual first unless you want to die at your first battle.");
					} else {
						my_battle = new Battle();
						if (win_num >= 50 && win_num < 60) {
							System.out.println(
									"You already have become the living legend of Arena.\nYou are getting old and your body may not last the next battle.");
							System.out.println("Think wisely. Retire soon.");
							my_battle.fight();
							my_battle_record.add(my_battle);

						} else if (win_num >= 60) {
							System.out.println(
									"Make space for younger gladiators. You may not enter any more battles.\nTime to retire.");
							retire();

						} else // 0 ~ 49 wins
						{
							my_battle.fight();
							my_battle_record.add(my_battle);
						}
					}
					if (dead) {
						death_retire();
					}
					break;
				case 2: // 3. Check status
					/*
					 * Check your current health and purchase potions to recover for the next
					 * battle.
					 */
					System.out.println(my_character.get_stats());
					break;
				case 3: // 4. Alchemy shop
					my_alchemy.enter();
					break;
				case 4: // 5. Armory
					my_armory.enter();
					break;
				case 5: // 6. Retire
					if (win_num == 0) {
						System.out.println("A gladiator only gets to retire after a glorious victory.");
						break;
					} else {
						retire();
						break;
					}
				}

				print_and_wait();
				if (retire_flag) {
					break;
				}
			}
		} // while loop ends
	}

	public void print_manual() throws IOException
        {
            // manual.txt should be located in the project's root directory 
		
			try (InputStream is = Arena.class.getResourceAsStream("manual.txt");
				Scanner sc2 = new Scanner(is).useDelimiter("\\A")) {
				
				String text = sc2.hasNext() ? sc2.next() : "";
				System.out.println(text);

				manual_read = true;
			}
        }

	public static String select_name() {
		// create user character here
		String input_name;
		boolean name_exists;
		while (true) {
			name_exists = false;
			System.out.println("Choose your name.");
			input_name = sc.nextLine();
			if (input_name.equals("")) {
				System.out.println("Hey, you need a name!");
			} else {
				for (String a : Character.enemy_names) {
					if (a.equals(input_name)) {
						System.out.println("This name is already taken.\n");
						name_exists = true;
						break;
					}
				}
			}
			if (!name_exists) {
				System.out.println("Your name is " + input_name + ". Do you confirm? y or n");
				String input_confirm = sc.nextLine();
				if (input_confirm.equals("y") || input_confirm.equals("Y")) {
					break;
				} else if (input_confirm.equals("n") || input_confirm.equals("N")) {
				} else {
					System.out.println("Invalid input. Please enter from beginning.\n");
				}

			}
		}
		System.out.println("\nWelcome, " + input_name + "! Will you be the next legend? We will see...");
		Arena.print_and_wait();
		clear_console();

		return input_name;
	}

	public void retire() {
		System.out.println("* * * * * * * * * * * * * * * * * * * * * * * *\n");
		if (win_num == 1) {
			System.out.printf("%d win | Beginner's luck? You survived the first fight but decided to leave this Arena. Some may say it was the best decision of yours.\n",
					win_num);
		} else if (win_num >= 2 && win_num < 15) {
			System.out.printf("%d wins | You retire as a modest gladiator. Surviving is what matters.\n", win_num);
		} else if (win_num >= 15 && win_num < 35) {
			System.out.printf("%d wins | You have made the Arena of Gory and Glory gorier and more glorious. Your retirement is celebrated by some.\n",
					win_num);
		} else if (win_num >= 35 && win_num < 50) {
			System.out.printf("%d wins | You are the best gladiator of your generation. Many people will remember your name.\n",
					win_num);
		} else { // // 50 - 70
			System.out.printf("%d wins | You are the greatest gladiator this Arena has ever seen."
					+ "\nYour name will remain in history and people will remember your name for hundreds of years.\n",
					win_num);
		}
		System.out.println("\n* * * * * * * * * * * * * * * * * * * * * * * *\n");
		retire_flag = true;
		view_log();
	}

	// retired by death
	public static void death_retire() {
		System.out.println("* * * * * * * * * * * * * * * * * * * * * * * *\n");
		if (win_num == 0) {
			System.out.println("You died at your first battle. Your body is burned along with other novice gladiators with unmemorable deaths.");
		} else if (win_num == 1) {
			System.out.printf("%d win | Beginner's luck? You survived the first fight but that was all for you. You die as a no-name gladiator.\n",
					win_num);
		} else if (win_num >= 2 && win_num < 15) {
			System.out.printf("%d wins | You die as a modest gladiator. Some may say that you were just about to bloom.\n",
					win_num);
		} else if (win_num >= 15 && win_num < 35) {
			System.out.printf("%d wins | You have made the Arena of Gory and Glory gorier and more glorious. Your grave is visited by some old fans.\n",
					win_num);
		} else if (win_num >= 35 && win_num < 50) {
			System.out.printf("%d wins | You proved yourself to be the best gladiator of your generation. Many people will remember your name."
							+ "\nIt is a shame that you did not leave when you were ahead.\n",
					win_num);
		} else { // 50 - 70
			System.out.printf("%d wins | You were the greatest gladiator this Arena has ever seen."
					+ "\nYou embraced your death at the place you shined most."
					+ "\nYour name will remain in history and people will remember your name for hundreds of years.\n",
					win_num);
		}
		System.out.println("\n* * * * * * * * * * * * * * * * * * * * * * * *\n");
		retire_flag = true;
		view_log();

	}

	public static void view_log()
        {
            int which_battle = 0;
            System.out.println("Press any key to view your battle logs.");
            sc.nextLine();
            clear_console();
            System.out.printf("Your score is %d based on the number of victories.\n\n", win_num);
            while (true)
            {
                while (true)
                {
                    try
                    {
                        System.out.printf("Enter the battle number [1-%d] to view the log. Enter 0 to exit.\n", Battle.battle_num);
                        which_battle = Integer.parseInt(sc.nextLine());
                        if (which_battle == 0)
                        {
                            break;
                        }
                        else if (which_battle < 0 || which_battle > my_battle_record.size())
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch (WrongIndexException we)
                    {
                        System.out.println("Such battle number does not exist. Enter again.\n");
                    }
                    catch (Exception e)
                    {
                        System.out.println("Invalid input. Enter again.\n");
                    }
                }
                if (which_battle == 0)
                {
                    break;
                }
                System.out.println("\n* Luck is reflected on dice. Str, Dex, and gears are reflected on final damage.");
                System.out.println(my_battle_record.get(which_battle - 1).read_battle_log(which_battle));


            }
        }

	public void print_menu() { // this method is added so that the menu printing codes don't get too long.
		System.out.println("Select menu. [1-6]");
		for (int i = 0; i < menu_options.length; i++) {
			System.out.println(menu_options[i]);
		}
		System.out.println();
	}

	public static void print_and_wait() { // this method is added so that clearing up the console does not clear up all the messages right away.
		System.out.println();
		System.out.println("Press any button to go back to menu.");
		sc.nextLine();
	}
	
	public static void clear_console() { // this method works same as Console.Clear() in C#.
	        try {
	            String operatingSystem = System.getProperty("os.name");
	              
	            if(operatingSystem.contains("Windows")) {        
	                ProcessBuilder pb = new ProcessBuilder("cmd", "/c", "cls");
	                Process startProcess = pb.inheritIO().start();
	                startProcess.waitFor();
	            } else {
	                ProcessBuilder pb = new ProcessBuilder("clear");
	                Process startProcess = pb.inheritIO().start();

	                startProcess.waitFor();
	            } 
	        } catch(Exception e){
	            System.out.println(e);
	        }
	    }
}
