using System;
using System.IO;
using CSVSerializer; //import the library, also from Project > Add Reference... > Choose the DLL file
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
            await SimpleSerialization.Serialize();

        }
    }
}
