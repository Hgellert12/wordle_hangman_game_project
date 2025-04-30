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
        static void hangman_game()
        {
            Console.WriteLine("What is the name of the file that contains the words?");
            string filename = Console.ReadLine();
            StreamReader sr = new StreamReader($"{filename}.txt");

            List<string> wordslist = new List<string>();

            while (!sr.EndOfStream)
            {
                wordslist.Add(sr.ReadLine().ToLower());
            }
            if (wordslist.Count == 0)
            {
                Console.WriteLine("The file was empty or the file is non existant");
                return;
            }
            Random rnd = new Random();

            int wordselector = rnd.Next(0, wordslist.Count);

            string word = wordslist[wordselector];
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

                    Console.WriteLine("Invalid Imput");
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
        private const int maxmistakes = 6;
        static void Main(string[] args)
        {
            bool o = true;
            while (o)
            {
                Console.WriteLine("Would you like to play the hangman game or  wordle?");
                Console.WriteLine("Press 'h' for hangman and 'w' for wordle and 'x' to quit");
                string x;
                x = Console.ReadLine().ToLower();
                if (x=="h")
                {
                    hangman_game();
                }
                else if (x=="w")
                {
                    continue;
                }
                else if (x=="x")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a valid imput");
                }
            }


            Console.ReadKey();


        }
    }
}
