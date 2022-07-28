package Arena_Fighter;

    class Armory implements Shop
    {
        /* Your enemy: always given common sword. always either common helmet or armor
        You: always given a sword. always either a helmet or armor.
        You can upgrade gears and get them in battles instead.
        Common sword - no extra attack damage.
        Common helmet and armor, respectively - 1 less health damage

        4 levels for each gear, which are Common, Rare, Epic and Legendary.
        For each level upgrade, the sword adds attack damage by 1.
        For each level upgrade, the helmet and armor respectively reduce damage by 1. */

        private int[] user_gears = { 1, 1, 1 }; // sword - helmet - armor. For 1, 2, 3, 4, it is Common - Unique - Epic - Legendary 
        private String[] gear_names = { "Common", "Rare", "Epic", "Legendary" };
        private String[] user_gear_names = { "Common", "Common", "Common" };

        private static boolean afford = false;

        public void enter()
        {
            System.out.println("Welcome to the Armory! You have " + Arena.crystal + " crystal.");
            System.out.println("You can upgrade your gear here. Each gear - sword, helmet , armor - needs to be upgraded separately.");
            System.out.println("\nPrice List\nCommon (lvl 1) |\nRare (lvl 2) | 100 crystal\nEpic (lvl 3) | 200 crystal\nLegendary (lvl 4) | 400 crystal");

            System.out.println("\nYour Gears");
            String which;
            for (int i = 0; i < user_gears.length; i++)
            {
                if (i == 0)
                {
                    which = "Sword";
                }
                else if (i == 1)
                {
                    which = "Helmet";
                }
                else
                {
                    which = "Armor";
                }
                System.out.printf("#%d  Current %s: %s (lvl %d)\n", i + 1, which, user_gear_names[i], user_gears[i]);

            }

            if (Arena.crystal < 100)
            {
                System.out.println("\nHey! You cannot afford anything here. Get out!");
                return;
            }

            int choice = choose_upgrade();
            
            if (afford)
            {   // purchase action here 
                System.out.println();
                switch (choice - 1) // this way, 0, 1, 2
                {
                    case 0:
                        upgrade_gear(choice - 1);
                        break;
                    case 1:
                        upgrade_gear(choice - 1);
                        break;
                    case 2:
                        upgrade_gear(choice - 1);
                        return;
                }
            }
            System.out.println("\nGood bye. We hope you continue to do business with us.");

        }

        // get input and handle errors for choosing a gear to upgrade

        public int choose_upgrade()
        {
            int choice = 0;
            while (true)
            {   // choose options
                try
                {
                    System.out.println("\nEnter a number [1-3] to upgrade a gear. Enter 0 to exit the shop.");
                    choice = Integer.parseInt(Arena.sc.nextLine());
                    if (choice < 0 || choice > 3)
                    { // wrong input
                        throw new Exception();
                    }
                    if (choice == 0)
                    { // exit the shop
                        break;
                    }
                    else if (choice == 1)
                    {
                        if (user_gears[0] == 4)
                        {
                            throw new MaxLevelException();
                        }
                        if (user_gears[0] == 3 && Arena.crystal < 400)
                        {
                            throw new InsufficientCrystalException();
                        }
                        if (user_gears[0] == 2 && Arena.crystal < 200)
                        {
                            throw new InsufficientCrystalException();
                        }
                        if (user_gears[0] == 1 && Arena.crystal < 100)
                        {
                            throw new InsufficientCrystalException();
                        }
                    }
                    else if (choice == 2)
                    {
                        if (user_gears[1] == 4)
                        {
                            throw new MaxLevelException();
                        }
                        if (user_gears[1] == 3 && Arena.crystal < 400)
                        {
                            throw new InsufficientCrystalException();
                        }
                        if (user_gears[1] == 2 && Arena.crystal < 200)
                        {
                            throw new InsufficientCrystalException();
                        }
                        if (user_gears[1] == 1 && Arena.crystal < 100)
                        {
                            throw new InsufficientCrystalException();
                        }
                    }
                    else if (choice == 3)
                    {
                        if (user_gears[2] == 4)
                        {
                            throw new MaxLevelException();
                        }
                        if (user_gears[2] == 3 && Arena.crystal < 400)
                        {
                            throw new InsufficientCrystalException();
                        }
                        if (user_gears[2] == 2 && Arena.crystal < 200)
                        {
                            throw new InsufficientCrystalException();
                        }
                        if (user_gears[2] == 1 && Arena.crystal < 100)
                        {
                            throw new InsufficientCrystalException();
                        }
                    }
                    afford = true;
                    break;
                }
                catch (MaxLevelException me)
                {
                    System.out.println("Your gear is already Legendary level.");
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
            return choice;
        }

        // upgrade
        public void upgrade_gear(int choice) // choice is original choice - 1. this way, 0, 1, 2
        {
            // charge
            if (user_gears[choice] == 1)
            { // if level 1 
                Arena.crystal = Arena.crystal - 100;
            }
            else if (user_gears[choice] == 2)
            {
                Arena.crystal = Arena.crystal - 200;
            }
            else if (user_gears[choice] == 3)
            {
                Arena.crystal = Arena.crystal - 400;
            }

            System.out.println("Upgrade completed!");

            user_gears[choice]++;
            user_gear_names[choice] = gear_names[user_gears[choice] - 1];

            // upgrade
            if (choice == 0)
            {
                System.out.printf("Current sword: %d (lvl %d)\n", user_gear_names[choice], user_gears[choice]);
                System.out.printf("It increases your attack damage by 1, compared to the previous level. In total, it is %d extra damage.\n", user_gears[0] - 1);
            }
            else if (choice == 1)
            {
                System.out.printf("Current helmet: %d (lvl %d)\\n", user_gear_names[choice], user_gears[choice]);
                System.out.printf("It reduces attack damage by 1, compared to the previous level. In total, it is %d extra damage.\n", user_gears[1] - 1);
            }
            else if (choice == 2)
            {
                System.out.printf("Current armor: %d (lvl %d)\\n", user_gear_names[choice], user_gears[choice]);
                System.out.printf("It reduces attack damage by 1, compared to the previous level. In total, it is %d extra damage.\n", user_gears[2] - 1);
            }
        }

        // getter
        public int[] get_user_gear_levels()
        {
            return user_gears;
        }

        public String get_user_gear_name(int level)
        {
            String name = "";
            switch(level)
                {
                case 1:
                    name = "Common";
                    break;
                case 2:
                    name = "Rare";
                    break;
                case 3:
                    name = "Epic";
                    break;
                case 4:
                    name = "Legendary";
                    break;
            }
            return name;
        }
    }