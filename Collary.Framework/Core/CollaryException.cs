using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collary.Framework.Core;

public class CollaryException : Exception
{
    public CollaryException(string message, Exception error)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine("");
        Console.WriteLine("-------------------------");
        Console.WriteLine("----- Collary Error -----");
        Console.WriteLine("-------------------------");
        Console.WriteLine("");

        Console.BackgroundColor = ConsoleColor.White;

        Console.WriteLine(message);

        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine("");
        Console.WriteLine("-------------------------");
        Console.WriteLine("------ Error Ended ------");
        Console.WriteLine("-------------------------");
        Console.WriteLine("");

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;

        throw error;
    }
}
