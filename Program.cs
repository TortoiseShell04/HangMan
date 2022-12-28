using System;
using System.Diagnostics;
using System.Text;
namespace App
{
    class main
    {
        public static void Main(string[] args)
        {
            // Link to python script responsible for creating 'List.txt' file
            string command = @"./PythonFiles/FileInput.py";
            var psi = new ProcessStartInfo();
            // Python path on your device
            psi.FileName = "/usr/bin/python3";
            // Command to be passed to python
            psi.Arguments = command;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            // Starts process
            using var process = Process.Start(psi);

            process.WaitForExit();

            // Misc. Game variables
            bool gameIsOn = true;
            // Path of word list
            string path = @"Lists.txt";
            // Reads files and sorts it into an array
            string[] words = File.ReadAllLines(path);

            // Main game loop
            while (gameIsOn)
            {
                // Generates random word
                Random ran = new Random();
                
                int length = words.Length;
                int num = ran.Next(length);

                // String with null check 
                // Contains word to be guessed
                string? WordToBeGuessed = words[num];

                int wordLength = WordToBeGuessed.Length;

                // Move count
                int moveCount = wordLength + 5;

                // The word bring completed in the game
                string? wordText = "";

                // Game booleans for winning and finding letters
                bool letterFound = false;
                bool gameWon = false;
            
                // Credits
                Console.WriteLine($"HangMan by ToirtoiseShell");
                Console.WriteLine($"-------------------------------");
            
                // Loop for guesser text
                for (int i = 0; i < wordLength; i++)
                {
                    wordText += "_";
                }

                // Mutable string type to be edited when letters are found
                StringBuilder strB = new StringBuilder(wordText);
                Console.WriteLine($"{strB}");
            
                // Entering letter loop
                while (moveCount > 0)
                {   Console.Clear();
                
                    Console.WriteLine($"{strB} \t Moves left: {moveCount}");
                    letterFound = false;
                    Console.WriteLine($"Enter a letter: ");
                    string? guess;

                    try
                    {
                        // String with null check for player move
                        guess = Console.ReadLine();
                    }
                    catch (System.Exception)
                    {
                        Console.WriteLine($"Invalid Input");
                        
                        throw;
                    }
                

                    // Checking for the letter
                    for (int i = 0; i < wordLength; i++)
                    {
                        // If the letter exists in the word
                        if (guess[0] == WordToBeGuessed[i])
                        {
                            // Editing the mutable string type to match the correct letters
                            int index = i;
                            for (int i1 = 0; i1 < wordLength; i1++)
                            {
                                if (index == i1)
                                {
                                    strB[index] = WordToBeGuessed[i];
                                    letterFound = true;
                                }
                        
                            }
                        }
                        // Decrements move count if the guess is wrong
                        else if (wordLength - i == 1 && !letterFound)
                        {
                            moveCount--;
                        }
                    }
                    

                    // You won??
                    if (strB.ToString() == WordToBeGuessed)
                    {
                        Console.WriteLine($"You won!!!!");
                        gameWon = true;
                        break;
                    }
            
                }

                // You lost!!
                if (!gameWon)
                {
                    Console.WriteLine(words[num]);
                }

                // Replay checking
                Console.WriteLine($"Would you like to play again?  (y/n) ");
                // Char with null check to check for replays
                char? yn = Char.Parse(Console.ReadLine());

                switch (yn)
                {
                    case 'y':
                    case 'Y':
                        Console.WriteLine($"Okay, Restarting game.........");                        
                        break;
                    case 'n':
                    case 'N':
                        gameIsOn = false;
                        break;
                    default:
                        gameIsOn = true;
                        Console.WriteLine($"No valid input was given, Restarting game......");                    
                        break;
                }
                
            }
        }
    }
}