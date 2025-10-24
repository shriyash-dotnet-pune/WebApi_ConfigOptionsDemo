using Serilog;
using System;
class Program
{
    static int Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console() 
            .WriteTo.File("logs/calculator.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
            .CreateLogger();

        try
        {
            Log.Information("Calculator started.");

            Console.WriteLine("Simple Calculator (type 'exit' to quit)");

            while (true)
            {
                Console.Write("Enter first number: ");
                var inputA = Console.ReadLine();
                if (string.Equals(inputA, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                Log.Information("User input for first number: {InputA}", inputA);

                Console.Write("Enter operator (+, -, *, /): ");
                var op = Console.ReadLine();
                if (string.Equals(op, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                Log.Information("User input for operator: {Operator}", op);

                Console.Write("Enter second number: ");
                var inputB = Console.ReadLine();
                if (string.Equals(inputB, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                Log.Information("User input for second number: {InputB}", inputB);

                if (!double.TryParse(inputA, out double a))
                {
                    Log.Warning("Failed to parse first number: {InputA}", inputA);
                    Console.WriteLine("Invalid first number. Try again.");
                    continue;
                }

                if (!double.TryParse(inputB, out double b))
                {
                    Log.Warning("Failed to parse second number: {InputB}", inputB);
                    Console.WriteLine("Invalid second number. Try again.");
                    continue;
                }

                try
                {
                    double result;

                    switch (op)
                    {
                        case "+":
                            result = a + b;
                            break;
                        case "-":
                            result = a - b;
                            break;
                        case "*":
                            result = a * b;
                            break;
                        case "/":
                            result = Divide(a, b);
                            break;
                        default:
                            throw new InvalidOperationException($"Unsupported operator '{op}'");
                    }


                    Log.Information("Operation performed: {A} {Op} {B} = {Result}", a, op, b, result);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Exception while performing operation. Inputs: {A}, {Op}, {B}", a, op, b);
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Log.Information("Calculator stopped by user.");
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Fatal error in calculator.");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    static double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Attempted to divide by zero.");
        }
        return a / b;
    }
}
