using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace CSVSerializer
{
    /// <summary>
    /// This class allows you to Serialize a document or a list of rows
    /// </summary>
    public class Serializer
    {
        private List<Row> Rows;
        private Document Document;
        private String FilePath;

        /// <summary>
        /// Constructor that requires a list of rows and the path of the file
        /// </summary>
        /// <param name="Rows">The body of the document</param>
        /// <param name="FilePath">Path of the file to be serialized</param>
        public Serializer(List<Row> Rows, String FilePath)
        {
            this.Rows = Rows;
            this.FilePath = FilePath;
        }

        /// <summary>
        /// Constructor that requires a Document and the path of the file to be serialized
        /// </summary>
        /// <param name="Document">The document to be serialize</param>
        /// <param name="FilePath">Path of the file to be serialized</param>
        public Serializer(Document Document, String FilePath)
        {
            this.Document = Document;
            this.FilePath = FilePath;
        }

         /// <summary>
         /// Serializes the specified document to the specified file path
         /// </summary>
         /// <returns>True if the task completes with no errors</returns>
        public async Task Serialize()
        {
            //if it's a document we first add the headers, otherwise we assume that the headers are in the first row
            if (!File.Exists(FilePath))
                File.Create(FilePath);
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                //constructor allows to input a List<List<Value>> or a Document
                if (Document != null)
                {
                    short index = 0; //to prevent the ',' to be added at the end of the line
                    foreach (String Header in Document.Headers) {
                        if (index == 0)
                            await sw.WriteAsync(String.Format("\"{0}\"", Header));
                        else
                            await sw.WriteAsync(String.Format(",\"{0}\"", Header));
                        ++index;
                    }
                    await sw.WriteAsync(sw.NewLine);
                }
                foreach (Row row in (Document != null? Document.Rows : Rows))
                {
                    short index = 0;
                    foreach (Value<object> value in row.Values)
                    {
                        if (index == 0)
                            await sw.WriteAsync(String.Format("\"{0}\"", value));
                        else
                            await sw.WriteAsync(String.Format(",\"{0}\"", value));
                        ++index;
                    }
                    await sw.WriteAsync(sw.NewLine);
                }
                sw.Close();
            }
        }

        public static async Task<Stream> SerializeObjects<T>(IEnumerable<T> objs, Stream stream, ObjectMapper<T> objectMapper = null)
        {
            int offset = 0;
            Dictionary<int, Tuple<string, string>> propertyNamesAliases;

            if (objectMapper == null)
            {
                // Resolve all the properties
                PropertyInfo[] properties = typeof(T).GetProperties();
                int index = 0;
                propertyNamesAliases = properties.ToDictionary(p => index++, p => new Tuple<string, string>(GetString(p.Name), GetString(p.Name)));  
            }
            else
            {
                // Get the relevant fields
                propertyNamesAliases = objectMapper.GetPropertyMap();
            }

            // Write headers (using the aliases)
            var headerStr = string.Join(",", propertyNamesAliases.Select(e => e.Value.Item2)) + "\n";
            var headerBytes = Encoding.UTF8.GetBytes(headerStr);
            await stream.WriteAsync(headerBytes, offset, headerBytes.Length);
            offset += headerBytes.Length;

            // Write values
            foreach (T obj in objs)
            {  
                List<Tuple<int, string>> values = new List<Tuple<int, string>>();
                var enumerator = propertyNamesAliases.GetEnumerator();
                while(enumerator.MoveNext())
                {
                    var currentPropName = enumerator.Current.Value.Item1;
                    var currentPropIndex = enumerator.Current.Key;
                    var currentPropValue = GetString(typeof(T).GetProperty(currentPropName).GetValue(obj, null));
                    var valueWithIndex = new Tuple<int, string>(currentPropIndex, currentPropValue);
                    values.Add(valueWithIndex);
                }

                // Sort by index
                var orderedValues = values.OrderBy(v => v.Item1);

                var row = string.Join(",", orderedValues) + "\n";
                var rowBytes = Encoding.UTF8.GetBytes(row);
                await stream.WriteAsync(rowBytes, offset, rowBytes.Length);
                offset += rowBytes.Length;
            }

            return stream;
        }

        private static string GetString(object obj)
        {
            string output = obj.ToString();
            if (output.Contains(' '))
            {
                output = string.Format($"\"{output}\"");
            }
            return output;
        }
    }
}
