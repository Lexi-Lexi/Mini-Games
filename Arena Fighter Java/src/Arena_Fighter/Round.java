package Arena_Fighter;

import java.util.Random;

class Round {
	private int round_num;
	private String attacker_name;
	private int dice_result;
	private int damage;
	private boolean valid_attack;

	private Random random = new Random();

	public Round(int round_num) {
		this.round_num = round_num;
	}

	/* Each attack inflicts health damage 1 to 6, depending on dice rolls. The damage may be increased by attacker's strength.
	 * You may automatically dodge an attack, depending on your dexterity. */
	
	public boolean dice_roll(int attacker, boolean attack_fail_50, Character enemy) {
		valid_attack = true;
		if (attacker == 1) { // player attacks
			attacker_name = "You";

			// check luck for random range
			if (Arena.my_character.get_luck() == 6) {
				dice_result = random.nextInt(5) + 2; // lucky man! max 6, min 2
			} else if (Arena.my_character.get_luck() == 1) {
				dice_result = random.nextInt(5) + 1; // unlucky man! max 5, min 1
			} else {
				dice_result = random.nextInt(6) + 1; // max 6, min 1
			}

			damage = dice_result + Arena.my_character.get_strength() + Arena.my_armory.get_user_gear_levels()[0] - 1; // my strength + sword option (level -1)
			damage = damage - 1; // enemy's helmet or armor. always just 1

			if (attack_fail_50 && (random.nextInt(2) != 0)) { // attack fails with 50% possibility
				valid_attack = false;
			} else {
				if (enemy.get_health() <= damage) {
					enemy.get_damaged(enemy.get_health()); // to make 0, not minus health
				} else {
					enemy.get_damaged(damage);
				}
			}
		}

		else { // enemy attacks
			attacker_name = enemy.get_name();

			// check luck for random rage
			if (enemy.get_luck() == 6) {
				dice_result = random.nextInt(5) + 2; // lucky man! max 6, min 2
			} else if (enemy.get_luck() == 1) {
				dice_result = random.nextInt(5) + 1; // unlucky man! max 5, min 1
			} else {
				dice_result = random.nextInt(6) + 1; // max 6, min 1
			}

			damage = dice_result + enemy.get_strength(); // enemy strength. sword option always common lvl 1, so no extra damage

			
			// my gear. randomly given. result retrieved from Battle fight() method
			if (Battle.helmet_or_armor) { // helmet
				damage = damage - Arena.my_armory.get_user_gear_levels()[1]; // my gear. helmet
			} else {
				damage = damage - Arena.my_armory.get_user_gear_levels()[2]; // my gear. armor
			}

			if (attack_fail_50 && (random.nextInt(2) != 0)) { // attack fails with 50% possibility
				valid_attack = false;
			} else {
				if (Arena.my_character.get_health() <= damage) {
					Arena.my_character.get_damaged(Arena.my_character.get_health()); // to make 0, not minus health
				} else {
					Arena.my_character.get_damaged(damage);
				}
			}
		}

		return valid_attack;
	}

	// method for reading log
	public String read_round_log() {
		String log;
		String a;
		if (valid_attack) {
			a = "Y";
		} else {
			a = "N";
		}
		log = String.format("Round %d |  Attacker: %s |  Dice: %d |  Final Damage: %d  |  Attack Success: %s",
				round_num, attacker_name, dice_result, damage, a);
		return log;
	}

}