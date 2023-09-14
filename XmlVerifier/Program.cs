// See https://aka.ms/new-console-template for more information
using XmlVerifier;
var keepConsoleOpen = true;
do
{
    Console.Clear();
    Console.WriteLine("Hi, hello!\n");
    Console.WriteLine("In this program here we parse some XML strings!\n");

    Console.Write("Give it a go (put your XML string as input): ");
    var xmlInput = Console.ReadLine();
    var validatorResult = XmlValidator.DetermineXml(xmlInput) ? "Valid! Great job!" : "Invalid! What a shame!";

    Console.WriteLine($"\nDoing my magic I found that your input was: {validatorResult}\n");

    Console.Write("Want to try again? (Any input other than Y/y terminates the application): ");
    var tryAgainInput = Console.ReadLine();
    keepConsoleOpen = !string.IsNullOrEmpty(tryAgainInput) && tryAgainInput.ToLower() == "y";

} while (keepConsoleOpen);

