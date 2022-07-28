using System;
using System.Collections.Generic;
namespace Arena_Fighter
{
    class Battle
    {
        Character enemy;
        Random random = new Random();
        int round_num = 0;
        string winner;
        
        List<Round> all_rounds = new List<Round>(); // dynamic

        //public variable
        public static int battle_num;
        public static bool helmet_or_armor = true;

        public void fight()
        {
            enemy = new Character();
            string attacker_name;

            // Variables for some dexterity-related rules.
            int my_dexterity = Arena.my_character.get_dexterity();
            int enemy_dexterity = enemy.get_dexterity();
            bool my_attack_fail_50 = get_dexterity_possibility(my_dexterity, enemy_dexterity);
            bool enemy_attack_fail_50 = get_dexterity_possibility(enemy_dexterity, my_dexterity);

            Console.WriteLine("Welcome to Arena of Gory and Glory. We expect a clean fight!\n");
            Console.WriteLine("Your enemy's profile\n" + enemy.get_enemy_stats());


            while (true)
            {
                round_num++;
                Round my_round = new Round(round_num);
                int reward;
                if (round_num == 1)
                {
                    if (random.Next(0, 2) != 0)
                    {
                        Console.WriteLine("You are given {0} helmet.\n", Arena.my_armory.get_user_gear_name(Arena.my_armory.get_user_gear_levels()[1]));
                        helmet_or_armor = true;
                        
                    }
                    else
                    {
                        Console.WriteLine("You are given {0} armor.\n", Arena.my_armory.get_user_gear_name(Arena.my_armory.get_user_gear_levels()[2]));
                        helmet_or_armor = false;
                    }
                }
                if (round_num % 2 == 1)
                {
                    attacker_name = "You (" + Arena.my_character.get_name() + ")";
                }
                else
                {
                    attacker_name = enemy.get_name();
                }
                Console.WriteLine("Round {0} | Attacker: {1}", round_num, attacker_name);

                if (round_num % 2 == 1)
                { // odd rounds attacker - user
                    if (attack(1, my_attack_fail_50, my_round))
                    { // valid attack
                        if (enemy.get_health() <= 0)
                        {
                            Console.WriteLine("That was the final hit. You won!");
                            winner = "You (" + Arena.my_character.get_name() + ")";
                            reward = 100;
                            if (round_num >= 13)
                            { // bonus for hard games
                                reward = reward + 20;
                            }
                            Console.Write("\nYour reward is " + reward + " crystal.");
                            Arena.crystal = Arena.crystal + reward;
                            Arena.win_num++;
                            battle_num++;
                            all_rounds.Add(my_round);
                            round_num = 0;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Attack succeeds! Your enemy's health is now {0}/50", enemy.get_health());
                            all_rounds.Add(my_round);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Swing and miss! Enemy dodged. Next round maybe.");
                        all_rounds.Add(my_round);
                    }
                }
                else if (round_num % 2 == 0)
                { // even rounds attacker - enemy
                    if (attack(2, enemy_attack_fail_50, my_round))
                    { // valid attack
                        if (Arena.my_character.get_health() <= 0)
                        {
                            Console.WriteLine("You took the final hit. You are dead.\n\n");
                            winner = enemy.get_name();
                            Arena.dead = true;
                            battle_num++;
                            all_rounds.Add(my_round);
                            round_num = 0;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You got hit! Your health is now {0}/50", Arena.my_character.get_health());
                            all_rounds.Add(my_round);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Remarkable move! You dodged. Let's keep it this way!");
                        all_rounds.Add(my_round);
                    }
                }
                Console.WriteLine();
            }
        }

        bool attack(int attacker, bool attack_fail_50, Round my_round)
        {

            return my_round.dice_roll(attacker, attack_fail_50, enemy);

        }
        bool get_dexterity_possibility(int attacker_dexterity, int defender_dexterity)
        {
            /* If defender's dexterity is 6 and is higher than attacker, attacker may fail the attack with the 50% possibility.
            If attacker's dexterity is 1 and is lower than defender, defender may dodge the attack with the 50% possibility.
            Otherwise, attacks always succeed. When both have 6 or 1, these specials rules do not apply and attack always succeeds.. */

            bool attack_fail_50 = false;

            if (attacker_dexterity == 1 && attacker_dexterity < defender_dexterity)
            {
                attack_fail_50 = true;
            }
            else if (defender_dexterity == 6 && attacker_dexterity < defender_dexterity)
            {
                attack_fail_50 = true;
            }

            return attack_fail_50;
        }

        public string read_battle_log(int log_choice)
        {
            string log = "\n";

            log = log + String.Format("Battle {0} |  Winner: {1} ", log_choice, winner) + "\n\n";
            for (int i = 0; i < all_rounds.Count; i++) 
            {
                log = log + all_rounds[i].read_round_log() + "\n";
            }

            
            return log;
        }

    }
}