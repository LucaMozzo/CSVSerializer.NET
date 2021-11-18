using CsvSerializer;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Demo.CS.Examples
{
    public static class SimpleSerialization
    {
        public async static Task Serialize()
        {
            var fileContent = Encoding.UTF8.GetBytes(Resource.users);

            // 1. Create a deserializer object with the file name as argument for the constructor
            Deserializer deserializer = new Deserializer(fileContent);

            // 2. Use Document type to organize the extracted values
            Document doc = deserializer.Deserialize();

            // 3. Loop through the headers
            foreach (String s in doc.Headers)
                Console.Write(s + "\t");
            Console.WriteLine("");

            // 4. Loop through the rows
            foreach (Row r in doc.Rows)
            {
                // 5. Loop through the values
                foreach (Value<object> v in r.Values)
                    Console.Write(v + "\t");
                Console.WriteLine("");
            }

            //Let's change some stuff...
            doc.Rows[0].UpdateValue(0, new Value<object>("Michael"));
            doc.Rows[0].UpdateValue(2, new Value<object>(64));

            // 6. Serializer part - Create a serializer
            Serializer serializer = new Serializer(doc, "output.csv");
            await serializer.Serialize().ConfigureAwait(true);
            Console.ReadLine();
        }
    }
}
