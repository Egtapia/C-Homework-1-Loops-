using System;

// ----- Interface for input -----
// Defines an abstraction for reading temperature input.
// This supports the Dependency Inversion Principle (DIP) by depending on an interface, not a concrete class.
public interface ITemperatureReader
{
    string ReadInput(); // Method to read input from any source (console, file, API, etc.)
}

// ----- Console implementation -----
// Concrete implementation of ITemperatureReader that reads from the console.
// This follows the Open/Closed Principle (OCP) — we can add new readers without changing existing code.
public class ConsoleTemperatureReader : ITemperatureReader
{
    public string ReadInput()
    {
        Console.Write("Enter a temperature ('q' to quit): ");
        return Console.ReadLine(); // Reads user input from the console
    }
}

// ----- Validator -----
// Responsible only for validating temperature values.
// This follows the Single Responsibility Principle (SRP) — one class, one reason to change.
public class TemperatureValidator
{
    private readonly float _min; // Minimum allowed temperature
    private readonly float _max; // Maximum allowed temperature

    public TemperatureValidator(float min = -20, float max = 130)
    {
        _min = min;
        _max = max;
    }

    // Checks if the temperature is within the allowed range
    public bool IsValid(float temp) => temp >= _min && temp <= _max;
}

// ----- Statistics -----
// Handles only the calculation of temperature statistics.
// This also follows SRP — no input/output logic here, only math.
public class TemperatureStatistics
{
    private float _sum = 0;   // Sum of all temperatures entered
    private int _count = 0;   // Number of valid temperatures entered

    // Adds a temperature to the statistics
    public void AddTemperature(float temp)
    {
        _sum += temp;
        _count++;
    }

    public int Count => _count; // Total number of valid entries
    public float Average => _count > 0 ? _sum / _count : float.NaN; // Average temperature
}

// ----- App Orchestrator -----
// Coordinates the workflow: reading input, validating, and updating statistics.
// This class depends on abstractions (ITemperatureReader) — DIP in action.
public class DailyTempsApp
{
    private readonly ITemperatureReader _reader;       // Input source
    private readonly TemperatureValidator _validator;  // Validator
    private readonly TemperatureStatistics _stats;     // Statistics calculator

    public DailyTempsApp(ITemperatureReader reader, TemperatureValidator validator, TemperatureStatistics stats)
    {
        _reader = reader;
        _validator = validator;
        _stats = stats;
    }

    public void Run()
    {
        while (true)
        {
            string input = _reader.ReadInput(); // Read from the chosen input source

            if (input?.ToLower() == "q") // Exit condition
                break;

            if (!float.TryParse(input, out float temp)) // Validate numeric format
            {
                Console.WriteLine("Invalid number, please try again!");
                continue;
            }

            if (_validator.IsValid(temp)) // Check if within range
            {
                _stats.AddTemperature(temp); // Add to statistics
            }
            else
            {
                Console.WriteLine("Temperature must be between -20 and 130 Fahrenheit.");
            }
        }

        // Display results
        Console.WriteLine($"\nTotal temperatures entered: {_stats.Count}");
        Console.WriteLine($"Average temperature: {_stats.Average:F2}");
    }
}

// ----- Entry point -----
// Creates and wires up all dependencies, then runs the app.
// This is the only place where concrete classes are instantiated.
public class Program
{
    public static void Main()
    {
        var reader = new ConsoleTemperatureReader(); // Concrete input source
        var validator = new TemperatureValidator();  // Validator with default range
        var stats = new TemperatureStatistics();     // Statistics tracker

        var app = new DailyTempsApp(reader, validator, stats); // Inject dependencies
        app.Run(); // Start the application
    }
}
