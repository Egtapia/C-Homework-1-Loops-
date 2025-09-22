using System;

// The DisplayMultiplicationTable class contains logic to:
// Generate a multiplication table (default 1–10, or user-chosen size)
// Optionally run a "practice" quiz where the user solves a multiplication question
class DisplayMultiplicationTable
{
    // The Main method is the entry point of the program.
    // It coordinates the flow: ask table size , print table , run practice (optional).
    static void Main()
    {
        Console.WriteLine("Multiplication Table (1–10) :)");

        // Ask the user for the table size (default 10 if they don’t customize)
        int size = GetTableSize();

        // Print the multiplication table up to 'size'
        PrintTable(size);

        // Ask if the user wants to practice a random multiplication question
        Console.Write("\nWould you like to practice? (y/n): ");
        string practiceChoice = Console.ReadLine();

        // If they type "y" (case-insensitive), run practice mode
        if (practiceChoice?.ToLower() == "y")
        {
            RunPractice(size);
        }

        Console.WriteLine("\nThanks for using the Multiplication Table app! See you ;)");
    }

    // This method asks the user if they want a custom size for the table.
    // If not, it defaults to 10. If invalid input is given, it also defaults to 10.
    static int GetTableSize()
    {
        Console.Write("Would you like to set a custom table size? (y/n): ");
        string choice = Console.ReadLine();

        int size = 10; // Default value

        // If user types "y", ask them for a number
        if (choice?.ToLower() == "y")
        {
            Console.Write("Enter the maximum number for the table: ");

            // Try to parse the input as an integer
            if (!int.TryParse(Console.ReadLine(), out size) || size < 1)
            {
                // If parsing fails or user gives a number < 1, revert to default
                Console.WriteLine("Invalid input, using default size 10.");
                size = 10;
            }
        }
        return size;
    }

    // This method prints the multiplication table from 1 to 'size'
    static void PrintTable(int size)
    {
        Console.WriteLine($"\nGenerating a {size} x {size} multiplication table...\n");

        // ---- HEADER ROW ----
        Console.Write("    |");
        for (int i = 1; i <= size; i++)
            Console.Write($"{i,4}"); // Print each number aligned in 4 spaces
        Console.WriteLine();

        // Print a separator line under the header
        Console.WriteLine("----" + new string('-', 4 * size));

        // ---- TABLE BODY ----
        for (int i = 1; i <= size; i++)
        {
            Console.Write($"{i,3} |"); // Print the row label
            for (int j = 1; j <= size; j++)
                Console.Write($"{i * j,4}"); // Print product aligned nicely
            Console.WriteLine(); // New line after each row
        }
    }

    // This method runs a practice question where the user answers a random multiplication.
    static void RunPractice(int size)
    {
        Random rand = new Random();

        // Generate two random numbers between 1 and 'size'
        int a = rand.Next(1, size + 1);
        int b = rand.Next(1, size + 1);

        // Ask the user to calculate the product
        Console.Write($" What is {a} x {b}? ");
        string answerInput = Console.ReadLine();

        // Validate that the input is a number
        if (int.TryParse(answerInput, out int answer))
        {
            // Compare answer with correct result
            if (answer == a * b)
                Console.WriteLine("Correct! ☻");
            else
                Console.WriteLine($"Oops! The correct answer is {a * b} :D");
        }
        else
        {
            // If input wasn’t a number
            Console.WriteLine("Oops! Not a valid number...");
        }
    }
}