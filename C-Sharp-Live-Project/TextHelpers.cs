using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Helpers
{
    public class TextHelpers
    {
        public static string Truncate(string entry, int limit)
        {
            char[] lead = new char[limit]; //Create a char array to get the chars of the string from.
            string newEntry= ""; //Create a string for returning
            entry.CopyTo(0, lead, 0, limit); //Use CopyTo to copy chars from the passed string up to the passed int amount
            foreach (char c in lead) //add the chars one at a time to the string variable
            {
                newEntry += c;
            }
            newEntry += "..."; //Add elipsis to the end of the string
            return newEntry; //Retrung the string.
        }
    }
}