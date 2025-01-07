using System;

namespace SyntaxExercises
{
    // Class NPC
    public class NPC
    {
        // Property Name: Readable from anywhere, writable only within the class
        public string Name { get; private set; }

        // Private field dialogLines
        private string[] dialogLines;

        // Method Talk
        public void Talk()
        {
            Console.WriteLine("NPC is talking...");
        }
    }

    // Class Position
    public class Position
    {
        // Properties X and Y
        public double X { get; set; }
        public double Y { get; set; }
    }

    // Class Vehicle
    public class Vehicle
    {
        // Property CurrentPosition
        public Position CurrentPosition { get; set; }

        // Virtual Method Move
        public virtual Position Move(Position targetPosition)
        {
            Console.WriteLine("Vehicle is moving...");
            return targetPosition; // Placeholder return value
        }
    }

    // Class Airplane (inherits Vehicle)
    public class Airplane : Vehicle
    {
        // Property Speed: Readable from anywhere, writable only by this class and subclasses
        public double Speed { get; protected set; }

        // Constructor
        public Airplane(double speed)
        {
            Speed = speed;
        }

        // Overriding Move method
        public override Position Move(Position targetPosition)
        {
            Console.WriteLine($"Airplane is flying to {targetPosition.X}, {targetPosition.Y}...");
            return targetPosition; // Placeholder return value
        }
    }

    // Main Program
    public class Program
    {
        public static void Main(string[] args)
        {
            // Array of integers
            int[] numbers = { 10, 20, 30, 40, 50 };

            // While loop to display array elements
            int index = 0;
            Console.WriteLine("Using while loop:");
            while (index < numbers.Length)
            {
                Console.WriteLine(numbers[index]);
                index++;
            }

            // For loop to display array elements
            Console.WriteLine("Using for loop:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            // Foreach loop to display array elements
            Console.WriteLine("Using foreach loop:");
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}