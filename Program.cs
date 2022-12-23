using System;
using System.Diagnostics;
using System.Text;
namespace App
{
    class main
    {
        public static void Main(string[] args)
        {
            string command = @"FileInput.py";
            var psi = new ProcessStartInfo();
            psi.FileName = "/usr/bin/python3";
            psi.Arguments = command;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using var process = Process.Start(psi);

            process.WaitForExit();

            bool gameIsOn = true;
            string path = @"Lists.txt";
            string[] words = File.ReadAllLines(path);
            while (gameIsOn)
            {
                Random ran = new Random();
                
                int length = words.Length;
                int num = ran.Next(length);

                string? WordToBeGuessed = words[num];

                int wordLength = WordToBeGuessed.Length;
                int moveCount = wordLength + 5;
                string? wordText = "";
                bool letterFound = false;
                bool gameWon = false;
            
                Console.WriteLine($"HangMan by ToirtoiseShell");
                Console.WriteLine($"-------------------------------");
            

                for (int i = 0; i < wordLength; i++)
                {
                    wordText += "_";
                }
                StringBuilder strB = new StringBuilder(wordText);
                Console.WriteLine($"{strB}");
            
                while (moveCount > 0)
                {
                    letterFound = false;
                    Console.WriteLine($"Enter a letter: ");
                    string? guess = Console.ReadLine();

                    for (int i = 0; i < wordLength; i++)
                    {
                        if (guess[0] == WordToBeGuessed[i])
                        {
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
                        else if (wordLength - i == 1 && !letterFound)
                        {
                            moveCount--;
                        }
                    }
                    Console.WriteLine($"{strB} \t Moves left: {moveCount}");

                    if (strB.ToString() == WordToBeGuessed)
                    {
                        Console.WriteLine($"You won!!!!");
                        gameWon = true;
                        break;
                    }
            
                }

                if (!gameWon)
                {
                    Console.WriteLine(words[num]);
                }

                Console.WriteLine($"Would you like to play again?  (y/n) ");
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