using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices; 
using System.Text;
using System.Threading.Tasks;

namespace projekt
{

    class Program
    {
        private const int maxmistakes = 6;
        private const int wordlelength = 5;
        
        static string difficultyselect()
        {
            string difficulty;
            string difficultynull = null;
            Console.WriteLine("Select difficulty \n easy \n medium \n hard");
            difficulty = Console.ReadLine().ToString().ToLower();
            if (difficulty == "easy" || difficulty == "medium" || difficulty == "hard")
            {
                return difficulty;
            }
            else
            {
                return difficultynull;
            }
        }
        static string wordselect() 
        {

            string difficulty = null;
            while (difficulty == null)
            {
                difficulty = difficultyselect();
            }
            StreamReader sr = new StreamReader($"{difficulty}w.txt");

            List<string> wordslist = new List<string>();

            while (!sr.EndOfStream)
            {
                wordslist.Add(sr.ReadLine().ToLower());
            }
            Random rnd = new Random();

            int wordselector = rnd.Next(0, wordslist.Count);

            string word = wordslist[wordselector];
            return word;

        }

        static bool IsValidGuess(string guess)
        {
            return guess.Length == wordlelength && guess.All(char.IsLetter);
        }



        static void wordle_game()
        {

            string word = wordselect();
            
            for (int i = 0; i < word.Length; i++)
            {
                Console.Write("_ ");
                
            }
            Console.Write("\n");
            Console.WriteLine(word);
            string Guess;
            int Guesscount = 0;
            
            while (maxmistakes>Guesscount)
            {
                Console.WriteLine($"You have {(6-Guesscount)} guesses left \n What is your next guess? ");
                
                Guess = Console.ReadLine().ToLower();
                bool isguessword = IsValidGuess(Guess);
                    if (isguessword == true)
                    {
                    
                        string answer = null;
                        for (int i = 0; i < Guess.Length; i++)
                        {
                            if (word.Contains(Guess[i]))
                            {
                                if (word[i] == Guess[i])
                                {
                                    answer += "!";
                                }
                                else
                                {
                                    answer += "?";
                                }
                            }
                            else
                            {
                                answer += "X";
                            }
                        }
                        Console.WriteLine(answer);
                        if (answer == "!!!!!")
                        {
                            Console.WriteLine("Congratulations you have won!");
                            return;
                        }
                        Guesscount++;
                        if (Guesscount==maxmistakes)
                        {
                            Console.WriteLine("Im sorry you have lost!");
                            return;
                        }
                    }
                    
                
                else
                {
                    Console.WriteLine("Invalid input please try again!");
                    continue;
                }
                
                
            }
        }
        static void hangman_game()
        {

            string word = wordselect();
            List<char> guesslist = new List<char>();

            List<char> Guessword = new List<char>();
            Console.WriteLine("Here is the word:");
            for (int i = 0; i < word.Length; i++)
            {
                Console.Write("_ ");
                Guessword.Add('_');
            }
            Console.WriteLine(word); //to fix issues and to check if the game is working properly obviously in a real enviroment this would not stay in the final product
            int mistakecount = 0;
            Console.WriteLine("You have " + (maxmistakes - mistakecount) + " guesses left");
            while (mistakecount < maxmistakes)
            {

                char guess;
                Console.WriteLine("What's your guess?");
                try
                {
                    guess = Console.ReadLine().ToLower()[0];
                }
                catch (Exception)
                {

                    Console.WriteLine("Invalid input");
                    continue;
                }
                if (!char.IsLetter(guess))
                {
                    Console.WriteLine("Write a valid character!");
                    continue;
                }
                if (guesslist.Contains(guess))
                {
                    Console.WriteLine("You already guessed that write another letter!");
                    continue;
                }
                guesslist.Add(guess);
                if (word.Contains(guess))
                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == guess)
                        {
                            Guessword[i] = guess;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("It doesn't contain that letter");
                    mistakecount++;
                }
                for (int i = 0; i < Guessword.Count; i++)
                {
                    Console.Write(Guessword[i]);

                }
                Console.WriteLine();
                if (!Guessword.Contains('_'))
                {
                    Console.WriteLine("Congratulations you have won!");
                    break;
                }

                Console.WriteLine("You have " + (maxmistakes - mistakecount) + " guesses left");
                Console.WriteLine("You have guessed:");
                for (int i = 0; i < guesslist.Count; i++)
                {

                    Console.Write(guesslist[i] + ",");

                }
                Console.WriteLine("\n" + "so far");

            }
            if (mistakecount == maxmistakes)
            {
                Console.WriteLine("You failed");
                Console.WriteLine("The word was " + word);
            }
        }
        
        static void Main(string[] args)
        {
            bool IsRunning = true;
            while (IsRunning)
            {
                Console.WriteLine("Would you like to play the hangman game or  wordle?");
                Console.WriteLine("Press 'h' for hangman and 'w' for wordle and 'x' to quit");
                string x;
                x = Console.ReadLine().ToLower();
                if (x == "h")
                {
                    hangman_game();
                }
                else if (x == "w")
                {
                    wordle_game();
                }
                else if (x == "x")
                {
                    IsRunning = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid input");
                }
            }


            Console.ReadKey();


        }
    }
}
