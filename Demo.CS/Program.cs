using System;
using Demo.CS.Examples;

namespace Demo.CS
{
    /**
      * This is a simple application that will show you how to use the library
      * <see cref="https://github.com/LucaMozzo/CSVSerializer.NET"/>
      */
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Simple serialization:");
            await SimpleSerialization.Serialize().ConfigureAwait(true);

            Console.WriteLine("\nObject serialization:");
            await SerializeObject.Serialize().ConfigureAwait(true);
        }
    }
}
