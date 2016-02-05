using System;
using System.IO;
using CSVSerializer; //import the library, also from Project > Add Reference... > Choose the DLL file

namespace Demo.CS
{
    /**
      * This is a simple application that will show you how to use the library
      * <see cref="https://github.com/LucaMozzo/CSVSerializer.NET"/>
      */
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "users.csv";

            // 1. Make sure the file exists, the exception is not handled
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found!");
                goto exit;
            }

            // 2. Create a deserializer object with the file name as argument for the constructor
            Deserializer deserializer = new Deserializer(fileName);

            // 3. Use Document type to organize the extracted values
            Document doc = deserializer.Deserialize();

            // 4. Loop through the headers
            foreach (String s in doc.Headers)
                Console.Write(s + "\t");
            Console.WriteLine("");

            // 5. Loop through the rows
            foreach (Row r in doc.Rows)
            {
                // 6. Loop through the values
                foreach (Value<object> v in r.Values)
                    Console.Write(v + "\t");
                Console.WriteLine("");
            }

            //Let's change some stuff...
            doc.Rows[0].UpdateValue(0, new Value<object>("Michael"));
            doc.Rows[0].UpdateValue(2, new Value<object>(64));

            // 7. Serializer part - Create a serializer
            Serializer serializer = new Serializer(doc, "users_.csv");
            serialize(serializer);
            exit: Console.ReadLine();
        }

        //we need to create another method because Main can't be async
        private async static void serialize(Serializer s)
        {
            if (await s.Serialize())
                Console.WriteLine("Successfully serialized");
            else
                Console.WriteLine("An error has occurred");
        }
    }
}
