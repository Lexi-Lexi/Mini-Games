package Arena_Fighter;

import java.util.Scanner;

public class Main {
	public static void main(String[] args)
    {
		try (Scanner sc = new Scanner(System.in)){
			
			System.out.println("* * * * * * * * * * * * * * * * * * * * * * * *");
	        System.out.println("WELCOME TO ARENA OF GORY AND GLORY");
	        System.out.println("* * * * * * * * * * * * * * * * * * * * * * * *\n");
	        System.out.println("Press Enter to continue.");
	        press_to_continue();

	        System.out.println();

	        var my_arena = new Arena();
	        try {
	        	my_arena.run();
	        }
	        catch (Exception e){
	        }
	        
	        System.out.println("We hope you enjoyed Arena of Gory and Glory!");
	        System.out.println("Press Enter to terminate . . . ");
	        press_to_continue();
		}

    }
	private static void press_to_continue()
	{ 
	       Scanner sc = new Scanner(System.in);
	       sc.nextLine();
	}
}
