using System;
namespace Arena_Fighter
{
    class Round
    {
        private int round_num;
        private string attacker_name;
        private int dice_result;
        private int damage;
        private bool valid_attack;

        Random random = new Random();
        
        public Round(int round_num)
        {
            this.round_num = round_num;          
        }

        /* Each attack inflicts health damage 1 to 6, depending on dice rolls. The damage may be increased by attacker's strength.
        You may automatically dodge an attack, depending on your dexterity. */
        public bool dice_roll(int attacker, bool attack_fail_50, Character enemy)
        {
            valid_attack = true;
            if (attacker == 1)
            {   // player attacks
                attacker_name = "You";

                // check luck for random range
                if (Arena.my_character.get_luck() == 6)
                {   // lucky man! max 6, min 2
                    dice_result = random.Next(2, 7);
                }
                else if (Arena.my_character.get_luck() == 1)
                {   // unlucky man! max 5, min 1
                    dice_result = random.Next(1, 6);
                }
                else
                {   
                    dice_result = random.Next(1, 7);
                }

                damage = dice_result + Arena.my_character.get_strength() + Arena.my_armory.get_user_gear_levels()[0] - 1; // my strength + sword option (level -1)
                damage = damage - 1; // enemy's helmet or armor. always just 1

                if (attack_fail_50 && (random.Next(0, 2) != 0))
                { // attack fails with 50% possibility
                    valid_attack = false;
                }
                else
                {
                    if (enemy.get_health() <= damage)
                    {
                        enemy.get_damaged(enemy.get_health()); // to make 0, not minus health 
                    }
                    else
                    {
                        enemy.get_damaged(damage);
                    }
                }
            }

            else
            { // enemy attacks
                attacker_name = enemy.get_name();

                // check luck for random rage
                if (enemy.get_luck() == 6)
                {
                    dice_result = random.Next(2, 7);
                }
                else if (enemy.get_luck() == 1)
                {
                    dice_result = random.Next(1, 6);
                }
                else
                {
                    dice_result = random.Next(1, 7);
                }

                damage = dice_result + enemy.get_strength(); // enemy strength. sword option always common lvl 1, so no extra damage

                // my gear. randomly given. result retrieved from Battle fight() method
                if (Battle.helmet_or_armor)
                { // helmet
                    damage = damage - Arena.my_armory.get_user_gear_levels()[1]; // my gear. helmet
                }
                else
                {
                    damage = damage - Arena.my_armory.get_user_gear_levels()[2]; // my gear. armor
                }

                if (attack_fail_50 && (random.Next(0, 2) != 0))
                { // attack fails with 50% possibility
                    valid_attack = false;
                }
                else
                {
                    if (Arena.my_character.get_health() <= damage)
                    {
                        Arena.my_character.get_damaged(Arena.my_character.get_health()); // to make 0, not minus health 
                    }
                    else
                    {
                        Arena.my_character.get_damaged(damage);
                    }
                }
            }

            return valid_attack;
        }

        // method for reading log
        public string read_round_log()
        {
            string log;
            string a;
            if (valid_attack)
            {
                a = "Y";
            }
            else
            {
                a = "N";
            }
            log = String.Format("Round {0} |  Attacker: {1} |  Dice: {2} |  Final Damage: {3}  |  Attack Success: {4}", round_num, attacker_name,
                dice_result, damage, a);
            return log;
        }

    }
}