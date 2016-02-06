using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

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
        public async Task<bool> Serialize()
        {
            //if it's a document we first add the headers, otherwise we assume that the headers are in the first row
            try
            {
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
                    return true;
            }
            catch { return false; }
        }
        //TODO: call this serializeAsync and create a synchronous method as well
    }
}
