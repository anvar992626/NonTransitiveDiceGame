Non-Transitive Dice Game
This repository contains a console-based implementation of a Non-Transitive Dice Game written in C#. The game demonstrates a provably fair system using cryptographic HMAC and modular arithmetic to ensure unbiased gameplay.

Features
Customizable Dice Configurations: Accepts arbitrary dice configurations with at least 6 faces through command-line arguments.
Provably Fair Randomness: Uses cryptographically secure random number generation and HMAC for verifiability.
Help Menu: Displays an ASCII probability table showing the chances of winning for each dice pair.
Modular Design: Code is divided into multiple classes for clear separation of concerns.
Replayable: After each game session, players can opt to play again or exit.
Error Handling: Provides detailed error messages for incorrect input configurations.


How to Run
Prerequisites
.NET 6 SDK
Org.BouncyCastle library for cryptographic operations.


Install the required library:
dotnet add package BouncyCastle


Run the Application
dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7


Command-Line Arguments
Provide at least 3 dice configurations.
Each configuration must contain 6 or more comma-separated integers.
Example:
dotnet run 1,2,3,4,5,6 2,2,4,4,9,9 3,3,5,5,7,7


Game Workflow

1. Input Validation
Ensures valid dice configurations.
Displays an error for:
Fewer than 3 dice.
Non-integer or negative face values.
Duplicate configurations.


2. Determining First Move
Uses a cryptographically secure random number and HMAC to determine who goes first.
Players can verify fairness using the provided HMAC key and message.


3. Dice Selection
Players and computer select different dice to play.
CLI menu ensures valid selections.

4. Throws
Each player rolls their dice using random numbers generated fairly.
Modular arithmetic ensures results are consistent and unbiased.

5. Probabilities
The help menu displays a probability table for all dice pairs.
Probabilities are calculated by comparing all possible face combinations.


Classes
1. Dice
Represents a dice with specified face values. Handles dice rolls.

2. DiceParser
Validates and parses dice configurations from command-line arguments.

3. RandomGenerator
Generates cryptographically secure random numbers and HMAC.

4. FairNumberGenerator
Implements the fair random number protocol.

5. ProbabilityCalculator
Calculates probabilities of winning for each pair of dice.

6. HelpTable
Generates and displays the ASCII probability table.

7. Game
Handles the game flow, including player interactions, dice selection, and scoring.

Example Gameplay
Command:
dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7
Output (Snippet):
Welcome to the Non-Transitive Dice Game!
Let's determine who makes the first move.
I selected a random value in the range 0..1 (HMAC=AA29E7275FE17A8D1184E2D4B6B0F46D815224270C94907CF007F2118CF400F7).
Try to guess my selection:
0 - 0
1 - 1
X - exit
? - help
Your selection: 0
My selection: 1 (KEY=7329ABD54A1633D2079EA7A48B401018D7EE6DD4C130AB5C31BC029CC8359637).
I go first!
I choose the dice [1,1,6,6,8,8].

Choose your dice:
0 - [2,2,4,4,9,9]
1 - [3,3,5,5,7,7]
X - Exit game
? - Display help table
Your selection: 0
You chose the dice [2,2,4,4,9,9].

It's time for my throw.
I selected a random value in the range 0..5 (HMAC=652863C27870CCA331458F4658D89413F405736FE5AA19B868FBDDAB5611A406).
Add your number modulo 6.
0 - 0
1 - 1
2 - 2
3 - 3
4 - 4
5 - 5
X - exit
? - help
Your selection: 4
My number is 3 (KEY=92564A82A515DEBC3FE9842D20DCEA3F3AAFB2080314A09A1E9A2CC729EDAF44).
The result is 3 + 4 = 1 (mod 6).
My throw is 8.
Your throw is 9.
You win (9 > 8)!


Error Handling
Examples of Invalid Input
Fewer than 3 dice:

Error: You must provide at least 3 dice configurations as command-line arguments.
Example: dotnet run 2,2,4,4,9,9 1,1,6,6,8,8 3,3,5,5,7,7
Non-integer or duplicate dice:

Error: Dice faces must be non-negative integers.
Error: Duplicate dice configurations are not allowed.
