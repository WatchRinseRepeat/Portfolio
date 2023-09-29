using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenges
{
    public class TextEdit
    {
        public static string RemoveVowels(string input)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' }; //vowel reference array
            StringBuilder output = new("", input.Length); //create a string builder for adding to when parsing the input.

            for (int i = input.Length - 1; i >= 0; i--)  //loop through each char in input starting at the back to the front.
            {
                bool test = false; //bool to test if vowel
                foreach (char vowel in vowels) //check char against vowel reference
                {
                    if (input[i] == vowel) //if input is vowwl set test to true and break the foreach loop.
                    {
                        test = true;
                        break;
                    }
                }
                if (!test) output.Insert(0, input[i]);  //if test was never set true then add the beginning of the stringbuilder.               
            }

            return output.ToString(); //turn stringbuilder to string and return.
        }

        public static string GetLargestNumber(string input)
        {
            string output; //string for output
            char[] chars = input.ToCharArray();  //turn string into char array so we can us the IsDigit method

            int largest = 0; //track what is largest int found

            for (int i = 0; i < chars.Length; i++)   //loop through the char array
            {
                int current;  // int to track the current int.
                if (char.IsDigit(chars[i]))  //if the current char is an int
                {
                    bool isChar = true; //bool to track if next char is int
                    current = Convert.ToInt32(chars[i].ToString());    //track the value of the int char.
                    int j = i; //track iteration through do while loop
                    do
                    {
                        if (++j < chars.Length && char.IsDigit(chars[j]))  //make sure we don't look outside the bounds of the array and see if the next char is int
                        {
                            current = (current * 10) + Convert.ToInt32(chars[j].ToString());  //if so multiply the current by 10 and add the next char
                        }
                        else
                        {
                            isChar = false; //otherwise the number is complete.
                        }
                    }
                    while (isChar);

                    if (current > largest)  // if current is larger than largest make largetst equal current.
                    {
                        largest = current;  
                    }
                }
            }

            output = largest.ToString();  //convert largest to string

            return output;
        }

    }



}
