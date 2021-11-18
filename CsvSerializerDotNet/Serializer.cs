using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace CsvSerializer
{
    /// <summary>
    /// This class allows you to Serialize a document or a list of rows
    /// </summary>
    public class Serializer
    {
        private Document Document;
        private String FilePath;

        public Serializer()
        {
            // No-op
        }

        /// <summary>
        /// Constructor that requires a list of rows and the path of the file
        /// </summary>
        /// <param name="Rows">The body of the document</param>
        /// <param name="FilePath">Path of the file to be serialized</param>
        [Obsolete]
        public Serializer(List<Row> Rows, string FilePath)
        {
            Document = new Document();
            Document.AddRows(Rows);
            this.FilePath = FilePath;
        }

        /// <summary>
        /// Constructor that requires a Document and the path of the file to be serialized
        /// </summary>
        /// <param name="Document">The document to be serialize</param>
        /// <param name="FilePath">Path of the file to be serialized</param>
        [Obsolete]
        public Serializer(Document Document, string FilePath)
        {
            this.Document = Document;
            this.FilePath = FilePath;
        }

        /// <summary>
        /// Serializes the specified document to the specified file path
        /// </summary>
        /// <returns>True if the task completes with no errors</returns>
        [Obsolete]
        public async Task Serialize()
        {
            if (Document == null)
            {
                throw new CsvDeserializationExcepion("The input rows or document are not set");
            }

            //if it's a document we first add the headers, otherwise we assume that the headers are in the first row
            if (!File.Exists(FilePath))
                File.Create(FilePath);
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                //constructor allows to input a List<List<Value>> or a Document
                if (Document.Headers.Count > 0)
                {
                    short index = 0; //to prevent the ',' to be added at the end of the line
                    foreach (string Header in Document.Headers) {
                        if (index == 0)
                            await sw.WriteAsync(string.Format("\"{0}\"", Header));
                        else
                            await sw.WriteAsync(string.Format(",\"{0}\"", Header));
                        ++index;
                    }
                    await sw.WriteAsync(sw.NewLine);
                }

                foreach (Row row in Document.Rows)
                {
                    short index = 0;
                    foreach (Value<object> value in row.Values)
                    {
                        if (index == 0)
                            await sw.WriteAsync(string.Format("\"{0}\"", value));
                        else
                            await sw.WriteAsync(string.Format(",\"{0}\"", value));
                        ++index;
                    }
                    await sw.WriteAsync(sw.NewLine);
                }
                sw.Close();
            }
        }

        /// <summary>
        /// Serializes a list of objects to csv
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="objs">The list of objects to write to the stream</param>
        /// <param name="stream">The stream onto which to write</param>
        /// <param name="objectMapper">The property mapper for the object</param>
        /// <returns></returns>
        public async Task SerializeObjects<T>(IEnumerable<T> objs, Stream stream, ObjectMapper<T> objectMapper = null)
        {
            Dictionary<int, Tuple<string, string>> propertyNamesAliases;
            Dictionary<string, Delegate> propertyValueTransformers;

            if (objectMapper == null)
            {
                // Resolve all the properties
                PropertyInfo[] properties = typeof(T).GetProperties();
                int index = 0;
                propertyNamesAliases = properties.ToDictionary(p => index++, p => new Tuple<string, string>(GetString(p.Name), GetString(p.Name)));
                propertyValueTransformers = new Dictionary<string, Delegate>();
            }
            else
            {
                // Get the relevant fields
                propertyNamesAliases = objectMapper.GetPropertyMap();
                propertyValueTransformers = objectMapper.GetTransformFunctions();
            }

            // Write headers (using the aliases)
            var headerStr = string.Join(",", propertyNamesAliases.OrderBy(e => e.Key).Select(e => e.Value.Item2)) + "\n";
            var headerBytes = Encoding.UTF8.GetBytes(headerStr);
            await stream.WriteAsync(headerBytes, 0, headerBytes.Length);

            // Write values
            foreach (T obj in objs)
            {  
                List<Tuple<int, string>> values = new List<Tuple<int, string>>();
                var enumerator = propertyNamesAliases.GetEnumerator();
                while(enumerator.MoveNext())
                {
                    var currentPropName = enumerator.Current.Value.Item1;
                    var currentPropIndex = enumerator.Current.Key;
                    var currentPropValue = typeof(T).GetProperty(currentPropName).GetValue(obj, null);

                    // if value transformer exists, apply transformation
                    if (propertyValueTransformers.ContainsKey(enumerator.Current.Value.Item2))
                    {
                        currentPropValue = propertyValueTransformers[enumerator.Current.Value.Item2].DynamicInvoke(currentPropValue);
                    }

                    var currentPropValueStr = GetString(currentPropValue);
                    var valueWithIndex = new Tuple<int, string>(currentPropIndex, currentPropValueStr);
                    values.Add(valueWithIndex);
                }

                // Sort by index
                var orderedValues = values.OrderBy(v => v.Item1).Select(v => v.Item2);

                var row = string.Join(",", orderedValues) + "\n";
                var rowBytes = Encoding.UTF8.GetBytes(row);
                await stream.WriteAsync(rowBytes, 0, rowBytes.Length);
            }

            await stream.FlushAsync();
        }

        private string GetString(object obj)
        {
            if (obj == null)
                return "";

            string output = obj.ToString();
            if (output.Contains(' ') || output.Contains(','))
            {
                output = string.Format($"\"{output}\"");
            }
            return output;
        }
    }
}
