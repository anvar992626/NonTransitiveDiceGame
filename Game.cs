using System;
using System.Collections.Generic;

public class Game
{
    private List<Dice> _dice;
    private Dice? _userDice;
    private Dice? _computerDice;
    private int _userScore = 0;
    private int _computerScore = 0;

    public Game(List<Dice> dice)
    {
        _dice = dice;
    }


    public void Start()
    {
        Console.WriteLine("Welcome to the Non-Transitive Dice Game!");
        while (true)
        {
            DetermineFirstMove();

            PlayRounds(); // Play rounds, exiting here returns control to replay prompt.

            Console.Write("Play again? (yes/no): ");
            string? input = Console.ReadLine()?.ToLower();
            if (input != "yes")
            {
                Console.WriteLine("Exiting the game. Thank you for playing!");
                break; // Exit the main loop
            }

            // Reset game state for replay
            _userDice = null;
            _computerDice = null;
            _userScore = 0;
            _computerScore = 0;
        }
    }



    private void DetermineFirstMove()
    {
        Console.WriteLine("\nLet's determine who makes the first move.");

        // Generate secure random number and HMAC
        string key = RandomGenerator.GenerateKey();
        int computerChoice = RandomGenerator.GenerateRandomNumber(2);
        string hmac = RandomGenerator.GenerateHMAC(key, computerChoice);

        Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac}).");
        Console.WriteLine("Try to guess my selection:");
        Console.WriteLine("0 - 0\n1 - 1\nX - exit\n? - help");

        string input;
        while (true)
        {
            input = Console.ReadLine()?.Trim().ToLower();
            if (input == "x")
            {
                Environment.Exit(0);
            }
            else if (input == "?")
            {
                HelpTable.Display(_dice, ProbabilityCalculator.CalculateProbabilities(_dice));
                Console.WriteLine("\nTry to guess my selection:");
                Console.WriteLine("0 - 0\n1 - 1\nX - exit\n? - help");
                continue; // Loop back to re-prompt the user
            }
            else if (int.TryParse(input, out int userGuess) && (userGuess == 0 || userGuess == 1))
            {
                Console.WriteLine($"My selection: {computerChoice} (KEY={key}).");

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
                break; // Exit the loop once the selection process is completed
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 0, 1, X to exit, or ? for help.");
            }
        }
    }



    private void UserSelectDice()
    {
        while (true)
        {
            Console.WriteLine("\nChoose your dice:");
            for (int i = 0; i < _dice.Count; i++)
            {
                Console.WriteLine($"{i} - [{string.Join(",", _dice[i].Faces)}]");
            }
            Console.WriteLine("X - Exit game\n? - Display help table");

            string input = Console.ReadLine()?.Trim().ToLower();
            if (input == "x") Environment.Exit(0);
            if (input == "?")
            {
                HelpTable.Display(_dice, ProbabilityCalculator.CalculateProbabilities(_dice));
                continue;
            }

            if (int.TryParse(input, out int choice) && choice >= 0 && choice < _dice.Count && _dice[choice] != _computerDice)
            {
                _userDice = _dice[choice];
                Console.WriteLine($"You chose the dice [{string.Join(",", _userDice.Faces)}].");
                if (_computerDice == null) ComputerSelectDice();
                break;
            }

            Console.WriteLine("Invalid choice. Try again.");
        }
    }

    private void ComputerSelectDice()
    {
        var availableDice = new List<Dice>(_dice);
        availableDice.Remove(_userDice);
        _computerDice = availableDice[RandomGenerator.GenerateRandomNumber(availableDice.Count)];
        Console.WriteLine($"I choose the dice [{string.Join(",", _computerDice.Faces)}].");
    }

    private void PlayRounds()
    {
        while (true)
        {
            Console.WriteLine("\nIt's time for my throw.");

            // Computer's throw with HMAC generation
            string computerKey = RandomGenerator.GenerateKey();
            int computerRollIndex = RandomGenerator.GenerateRandomNumber(6);
            string computerHMAC = RandomGenerator.GenerateHMAC(computerKey, computerRollIndex);

            Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={computerHMAC}).");

            // User's throw
            int userRollIndex;
            while (true)
            {
                Console.WriteLine("Add your number modulo 6.");
                for (int i = 0; i < 6; i++) Console.WriteLine($"{i} - {i}");
                Console.WriteLine("X - exit\n? - help");
                Console.Write("Your selection: ");

                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "x") Environment.Exit(0);
                if (input == "?")
                {
                    HelpTable.Display(_dice, ProbabilityCalculator.CalculateProbabilities(_dice));
                    continue;
                }

                if (int.TryParse(input, out userRollIndex) && userRollIndex >= 0 && userRollIndex < 6)
                    break;

                Console.WriteLine("Invalid input. Please enter a number between 0 and 5, or use X to exit and ? for help.");
            }

            Console.WriteLine($"My number is {computerRollIndex} (KEY={computerKey}).");

            // Compute the result
            int result = (computerRollIndex + userRollIndex) % 6;
            Console.WriteLine($"The result is {computerRollIndex} + {userRollIndex} = {result} (mod 6).");

            // Roll the dice
            int computerRoll = _computerDice.Roll(result);
            int userRoll = _userDice.Roll(userRollIndex);

            Console.WriteLine($"My throw is {computerRoll}.");
            Console.WriteLine($"Your throw is {userRoll}.");

            // Determine winner
            if (userRoll > computerRoll)
            {
                Console.WriteLine("You win this round!");
                _userScore++;
            }
            else if (userRoll < computerRoll)
            {
                Console.WriteLine("I win this round!");
                _computerScore++;
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }

            // Show scores
            Console.WriteLine($"Scores -> You: {_userScore}, Me: {_computerScore}");

            // Ask to play another round
            Console.Write("Do you want to play another round? (yes/no): ");
            string continueInput = Console.ReadLine()?.Trim().ToLower();
            if (continueInput != "yes")
            {
                Console.WriteLine("Exiting the game. Thank you for playing!");
                break;
            }
        }
    }



}
