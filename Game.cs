using System;
using System.Collections.Generic;

public class Game
{
    private List<Dice> _dice;
    private Dice? _userDice;
    private Dice? _computerDice;

    public Game(List<Dice> dice)
    {
        _dice = dice;
    }

    public void Start()
    {
        while (true) // Loop to allow full game restart
        {
            Console.WriteLine("Let's determine who makes the first move.");

            string key = RandomGenerator.GenerateKey();
            int computerChoice = RandomGenerator.GenerateRandomNumber(2);
            string hmac = RandomGenerator.GenerateHMAC(key, computerChoice);

            Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac}).");
            Console.Write("Try to guess my selection (0 or 1): ");

            if (!int.TryParse(Console.ReadLine(), out int userGuess) || (userGuess != 0 && userGuess != 1))
            {
                Console.WriteLine("Invalid input. Please enter 0 or 1.");
                continue; // Restart the first move determination
            }

            Console.WriteLine($"My selection: {computerChoice} (KEY={key}).");
            Console.WriteLine("You can verify the HMAC using the key and the number I selected.\n");

            if (userGuess == computerChoice)
            {
                Console.WriteLine("You go first!");
                UserSelectDice();
            }
            else
            {
                Console.WriteLine("I go first!");
                ComputerSelectDice();
                UserSelectDice();
            }

            PlayRounds();

            // Ask if the user wants to restart the game
            Console.Write("Play again? (yes/no): ");
            if (Console.ReadLine()?.ToLower() != "yes")
            {
                Console.WriteLine("Exiting the game. Thank you for playing!");
                return; // Exit the program if the user says "no"
            }

            // Reset game state for a full restart
            _userDice = null;
            _computerDice = null;
        }
    }



    private void UserSelectDice()
    {
        while (true)
        {
            // Display the dice options
            Console.WriteLine("\nChoose your dice:");
            for (int i = 0; i < _dice.Count; i++)
            {
                Console.WriteLine($"{i} - [{string.Join(",", _dice[i].Faces)}]");
            }
            Console.WriteLine("X - Exit game");
            Console.WriteLine("? - Display help table");

            // Prompt the user for input
            Console.Write("Your selection: ");
            string input = Console.ReadLine()?.Trim().ToLower();

            // Handle exit option
            if (input == "x")
            {
                Console.WriteLine("Exiting the game. Thank you for playing!");
                Environment.Exit(0); // Terminate the program
            }

            // Handle help option
            if (input == "?")
            {
                var probabilities = ProbabilityCalculator.CalculateProbabilities(_dice);
                HelpTable.Display(_dice, probabilities);
                continue; // Redisplay the dice selection menu after showing help
            }

            // Handle dice selection
            if (int.TryParse(input, out int choice) && choice >= 0 && choice < _dice.Count && _dice[choice] != _computerDice)
            {
                _userDice = _dice[choice];
                Console.WriteLine($"You chose the dice [{string.Join(",", _userDice.Faces)}].");

                // Ensure computer selects its dice if not already selected
                if (_computerDice == null)
                {
                    ComputerSelectDice();
                }
                break;
            }

            // Handle invalid input
            Console.WriteLine("Invalid choice. Please select a valid dice, X to exit, or ? for help.");
        }
    }



    private void ComputerSelectDice()
    {
        foreach (var dice in _dice)
        {
            if (dice != _userDice) // Ensure the computer selects a different dice
            {
                _computerDice = dice;
                Console.WriteLine($"I choose the dice [{string.Join(",", _computerDice.Faces)}].");
                return; // Exit the loop once a dice is selected
            }
        }

        // Fallback in case no valid dice is found (should not happen)
        Console.WriteLine("Error: Could not select a valid dice for the computer. Exiting the game.");
        Environment.Exit(1);
    }


    private void PlayRounds()
    {
        while (true)
        {
            Console.WriteLine("\nNew round!");

            // Ensure dice are selected before proceeding
            if (_computerDice == null || _userDice == null)
            {
                Console.WriteLine("Error: Dice have not been properly selected. Exiting the game.");
                return;
            }

            // Computer's turn
            string computerKey = RandomGenerator.GenerateKey();
            int computerRollIndex = RandomGenerator.GenerateRandomNumber(6);
            string computerHMAC = RandomGenerator.GenerateHMAC(computerKey, computerRollIndex);
            Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={computerHMAC}).");

            // User's turn
            Console.WriteLine("Add your number modulo 6.");
            for (int i = 0; i < 6; i++) Console.WriteLine($"{i} - {i}");
            Console.WriteLine("X - Exit game\n? - Display help table");
            Console.Write("Your selection: ");

            string input = Console.ReadLine()?.Trim().ToLower();
            if (input == "x") break;
            if (input == "?")
            {
                var probabilities = ProbabilityCalculator.CalculateProbabilities(_dice);
                HelpTable.Display(_dice, probabilities);
                continue;
            }
            if (!int.TryParse(input, out int userRollIndex) || userRollIndex < 0 || userRollIndex > 5) continue;

            // Reveal computer's roll
            Console.WriteLine($"My number is {computerRollIndex} (KEY={computerKey}).");
            int computerRoll = _computerDice.Roll(computerRollIndex);
            int userRoll = _userDice.Roll(userRollIndex);

            Console.WriteLine($"You rolled: {userRoll}, I rolled: {computerRoll}");

            if (userRoll > computerRoll) Console.WriteLine("You win!");
            else if (userRoll < computerRoll) Console.WriteLine("I win!");
            else Console.WriteLine("It's a tie!");

            break; // End round
        }
    }


}
