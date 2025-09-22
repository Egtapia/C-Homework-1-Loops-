/*
 EXERCISE #10 PERFECT NUMBERS

Write an application named Perfect that displays every perfect number 
from 1 through 10,000.A number is perfect if it equals the sum of all the smaller 
positive integers that divide evenly into it.
For example, 6 is perfect because 1, 2, and 3 divide evenly into it and their sum is 6.
(5.1, 5.2, 5.4, 5.5)
*/

using System;

class Perfect
{
    static void Main()
    {
        Console.WriteLine("Perfect Numbers between 1 and 10,000:");
        Console.WriteLine("-------------------------------------");

        // Loop through numbers 1–10000
        for (int number = 1; number <= 10000; number++)
        {
            if (IsPerfect(number)) // check if the current number is perfect
            {
                Console.WriteLine($"{number} is PERFECT!");
            }
        }

        // Fun extra: allow user to test their own number
        Console.Write("\nWould you like to test your own number? (y/n): ");
        string choice = Console.ReadLine();

        if (choice?.ToLower() == "y")
        {
            Console.Write("Enter a positive integer: ");
            if (int.TryParse(Console.ReadLine(), out int testNumber) && testNumber > 0)
            {
                if (IsPerfect(testNumber))
                    Console.WriteLine($"{testNumber} is PERFECT!");
                else
                    Console.WriteLine($"{testNumber} is NOT perfect.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid positive number.");
            }
        }

        Console.WriteLine("\nThanks for checking perfect numbers! See you next time");
    }

    // Method that checks if a number is perfect
    static bool IsPerfect(int number)
    {
        int sum = 0;

        // find divisors up to number/2 (no need to check beyond half)
        for (int i = 1; i <= number / 2; i++)
        {
            if (number % i == 0) // i is a divisor
                sum += i;
        }

        return sum == number; // true if sum of divisors equals the number
    }
}
