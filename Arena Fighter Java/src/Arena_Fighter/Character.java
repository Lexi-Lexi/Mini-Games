package Arena_Fighter;

import java.util.Random;

class Character {
	
	/* - Strength: It decides how much damage your attack inflicts on enemy. Each strength level increases damage by 1, aside from basic damage [1-6]
    ex) If an attack dice is 5 and your strength level is 3, you inflict 8 health damage.
      - Dexterity: It decides the possibility of dodging attack, or succeeding attack.
        If defender's dexterity is 6 and is higher than attacker, attacker may fail the attack with the 50% possibility.
        If attacker's dexterity is 1 and is lower than defender, defender may dodge the attack with the 50% possibility.
      - Luck: It decides dice rolls. You may obtain lucky charms at Alchemy shop to increase your luck level.
        Luck level 6 - the minimum value of each dice roll becomes 2, not 1.
        Luck level 1 - the maximum value of each dice roll becomes 5, not 6. */
	
	private Random random = new Random();
	
	static String[] enemy_names = { "Spartacus", "Crixus", "Gannicus", "Lucretia", "Oenomaus", "Vitus", "Marcus", "Tiberius",
			"Ilithyia", "Agron", "Glaber" };
	// These names are retrieved from an American tv-series Spartacus

	String name;
	private int health = 50; // default 50. Obtain potions to recover at Alchemy shop.
	private int strength = random.nextInt(6) + 1;
	private int dexterity = random.nextInt(6) + 1;; // character may dodge the attack
	private int luck = random.nextInt(6) + 1;

	// constructor
	public Character() { // enemy creation
		name = enemy_names[random.nextInt(enemy_names.length)]; // random name
	}

	public Character(String name) { // character creation
		this.name = name;
	}

	// item usage

	public void heal(int recovery) {
		health = Math.min((health + recovery), 50);
		System.out.println("\nHealed! Current Health: " + health + "/50");
	}

	public void increase_luck() {
		luck++;
		System.out.println("\nLuck increased! Current Luck: " + luck);
	}

	public void stat_reassign(int stat) { // reassign value for stats. This option can be bought in Alchemy shop
		if (stat == 1) {
			strength = random.nextInt(6) + 1;
			System.out.println("\nStrength Reset! Current strength level: " + strength);
		} else {
			dexterity = random.nextInt(6) + 1;
			System.out.println("\nDexterity Reset! Current dexterity level: " + dexterity);
		}
	}

	// get attacked
	public void get_damaged(int damage) {
		health = health - damage;
	}

	// getters
	public String get_name() {
		return name;
	}

	public int get_health() {
		return health;
	}

	public int get_strength() {
		return strength;
	}

	public int get_dexterity() {
		return dexterity;
	}

	public int get_luck() {
		return luck;
	}

	public String get_enemy_stats() { // check status
		return "Name: " + name + ", Health: " + health + ", Strength: " + strength + ", Dexterity: " + dexterity
				+ ", Luck: " + luck;
	}

	public String get_stats() { // check status
		return "Name: " + name + ", Health: " + health + ", Strength: " + strength + ", Dexterity: " + dexterity
				+ ", Luck: " + luck + "\nCrystal: " + Arena.crystal;
	}
}
