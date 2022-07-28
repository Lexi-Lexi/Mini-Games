package Arena_Fighter;

class Alchemy implements Shop {
	
	/* Small potion (10 crystal) - restore health by 5
    Potion (20 crystal) - restore health by 15
    Large potion (30 crystal) - restore health by 25
    Elixir (50 crystal) - recover completely
    Lucky Charm (300 crystal) - increase luck level by 1
    Tabula Rasa (100 crystal) - reassign a random value [1-6] for strength or dexterity */

	private String[][] product_list = {{"Small Potion", "5 Health Recovery", "10 Crystal"}, {"Potion", "15 Health Recovery", "20 Crystal"},
            {"Large Potion", "25 Health Recovery", "30 Crystal"}, {"Elixir", "Full Health Recovery", "50 Crystal"},
            {"Lucky Charm", "Increase Luck by 1", "300 Crystal"}, {"Tabula Rasa", "Get a new random value [1-6] for Strength and Dexterity", "100 Crystal"}};

	public void enter()
        {
            // variables
            int choice = 0;
            boolean afford = false;
            
            // print items
            System.out.println("Welcome to the Alchemy Shop! You have " + Arena.crystal + " crystal.\n");
            for (int i = 0; i < product_list.length; i++)
            {
                System.out.printf("#%d  %s  |  %s  |  %s\n", i + 1, product_list[i][0], product_list[i][1], product_list[i][2]);
            }

            if (Arena.crystal < 5)
            { // broke
                System.out.println("\nYou do not have enough crystal to purchase anything here. Get out!");
                return;
            }

            // choose an item to purchase
            while (true)
            {   // choose options
                try
                {
                    System.out.println("\nEnter a number [1-6] to purchase. Enter 0 to exit the shop.");
                    choice = Integer.parseInt(Arena.sc.nextLine());
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
                        throw new InsufficientCrystalException();
                    }
                    else if (choice == 3 && Arena.crystal < 25)
                    {
                        throw new InsufficientCrystalException();
                    }
                    else if (choice == 4 && Arena.crystal < 50)
                    {
                        throw new InsufficientCrystalException();
                    }
                    else if (choice == 5 && Arena.my_character.get_luck() == 6)
                    {
                        throw new MaxLevelException();
                    }
                    else if (choice == 5 && Arena.crystal < 300)
                    {
                        throw new InsufficientCrystalException();
                    }
                    else if (choice == 6 && Arena.crystal < 100)
                    {
                        throw new InsufficientCrystalException();
                    }
                    afford = true;
                    break;
                }
                catch (MaxLevelException me)
                {
                    System.out.println("You already have Luck level 6.");
                }
                catch (InsufficientCrystalException ie)
                {
                    System.out.println("You do not have enough crystal to purchase this item. Choose something else.");
                }
                catch(Exception e)
                {
                    System.out.println("Invalid input.Enter again.");
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
                System.out.println("Transaction completed.");
            }
            System.out.println("\nGood bye. We hope you continue to do business with us.");

        }

	// items
	public void small_potion() {
		Arena.crystal = Arena.crystal - 10;
		Arena.my_character.heal(5);
	}

	public void potion() {
		Arena.crystal = Arena.crystal - 20;
		Arena.my_character.heal(15);
	}

	public void large_portion() {
		Arena.crystal = Arena.crystal - 30;
		Arena.my_character.heal(25);
	}

	public void elixir() {
		Arena.crystal = Arena.crystal - 50;
		Arena.my_character.heal(50);
	}

	public void lucky_charm() {
		Arena.crystal = Arena.crystal - 300;
		Arena.my_character.increase_luck();
	}

	public void tabula_rasa() {
		Arena.crystal = Arena.crystal - 100;
		String choice = "";
		System.out.printf("\nCurrent Strength Level: %d | Current Dexterity Level: %d\n\n",
				Arena.my_character.get_strength(), Arena.my_character.get_dexterity());
		while (true) {
			System.out.println("Enter 1 to reset strength, and 2 to reset dexterity.");
			choice = Arena.sc.nextLine();
			if (!choice.equals("1") && !choice.equals("2")) {
				System.out.println("Invalid input. Enter again.");
			} else {
				break;
			}
		}
		Arena.my_character.stat_reassign(Integer.parseInt(choice));
	}

}

@SuppressWarnings("serial")
class InsufficientCrystalException extends Exception{
}

@SuppressWarnings("serial")
class MaxLevelException extends Exception{
}

@SuppressWarnings("serial")
class WrongIndexException extends Exception{
}