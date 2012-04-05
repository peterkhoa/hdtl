using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    
   public class RandomStrings
   {
      //our default string size
      private const int CONST_MaxStringLenght = 10;

      //our default character string set
      private const string CONST_AllowedCharacterLiterals 
         = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

      //our randomizer
      Random randomNumber = new Random();

      /// <summary>
      /// Generate a random string using the default options
      /// </summary>
      /// <returns>the random string</returns>
      public string GenerateRandomString()
      {
         return GenerateRandomString(CONST_MaxStringLenght);
      }

      /// <summary>
      /// Create a random string based on a specified lenght
      /// </summary>
      /// <param name="lenght">the lenght of the desired lenght of the random string</param>
      /// <returns>the randomized string</returns>
      public string GenerateRandomString(int lenght)
      {
         return GenerateRandomString(lenght, CreateRandomStringSet());
      }

      /// <summary>
      /// Creates a random string based on a specific lenght and character set
      /// </summary>
      /// <param name="lenght">the lenght of the desired lenght of the random string</param>
      /// <param name="charsToUse">character set to use</param>
      /// <param name="randomizeSourceSet">defines whether the source set should be scrambled</param>
      /// <returns>the random string</returns>
      public string GenerateRandomString(int lenght, string charsToUse, bool randomizeSourceSet)
      {
         string randomString = String.Empty;

         if (randomizeSourceSet) {
            //scramble and jumble it
            randomString = GenerateRandomString(lenght, CreateRandomStringSet(charsToUse));
         }
         else {
            //use the default charset
            randomString = GenerateRandomString(lenght, charsToUse);
         }

         return randomString;
      }

      /// <summary>
      /// Creates a random string based on a specific lenght and character set
      /// </summary>
      /// <param name="lenght">the lenght of the desired lenght of the random string</param>
      /// <param name="charsToUse">character set to use</param>
      /// <returns>the random string</returns>
      public string GenerateRandomString(int lenght, string charsToUse)
      {
         //Create a new StringBuilder that would hold the random string.
         StringBuilder randomString = new StringBuilder();

         //Create a variable to hold the generated charater.
         char appendedChar;

         //Create a loop that would iterate from 0 to the specified value of intLenghtOfString
         for (int i = 0; i <= lenght; ++i)
         {
            int characterIndex = Convert.ToInt32(randomNumber.Next(i, charsToUse.Length - i));
            //Generate the char and assign it to appendedChar
            appendedChar = charsToUse[characterIndex];
            //Append appendedChar to randomString
            randomString.Append(appendedChar);
         }
         //Convert randomString to String and return the result.
         return randomString.ToString();
      }

      /// <summary>
      /// Returns a random set of characters based on the default literal set
      /// </summary>
      /// <returns>the random string</returns>
      private string CreateRandomStringSet()
      {
         //just use the default character set
         return CreateRandomStringSet(CONST_AllowedCharacterLiterals);
      }

      /// <summary>
      /// A function that returns a new set of characters based on an input set
      /// </summary>
      /// <param name="allowedCharacters">the source set</param>
      /// <returns>the new collection of characters</returns>
      private string CreateRandomStringSet(string allowedCharacters)
      {
         string randomizedString = String.Empty;

         //get a random string set size
         int randomSetLenght = allowedCharacters.Length * randomNumber.Next(1, CONST_MaxStringLenght);

         //while lenght of the random set is not the same as the source string lenght
         while (randomizedString.Length != randomSetLenght)
         {
            //add a new character
            randomizedString += GetRandomCharFromString(allowedCharacters);
         }

         //return our random string
         return randomizedString;
      }

      /// <summary>
      /// Gets a character from the the input string
      /// </summary>
      /// <param name="allowedCharacters">source string</param>
      /// <returns>a random character</returns>
      private char GetRandomCharFromString(string allowedCharacters)
      {
         return allowedCharacters[randomNumber.Next(allowedCharacters.Length - 1)];
      }
   }
}