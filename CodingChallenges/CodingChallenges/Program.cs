using CodingChallenges;

Console.WriteLine("Enter a alphanumeric string:");

string? input = Console.ReadLine();

if (input == null)
{
    Console.WriteLine("that was blank.");
}
else
{
    Console.WriteLine( TextEdit.GetLargestNumber(input));
}

Console.WriteLine();

Console.ReadLine();