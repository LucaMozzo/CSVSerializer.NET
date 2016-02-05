using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVSerializer
{
    public class Serializer
    {
        private List<Row> Rows;
        private Document Document;
        private String FilePath;
        public Serializer(List<Row> Rows, String FilePath)
        {
            this.Rows = Rows;
            this.FilePath = FilePath;
        }

        public Serializer(Document Document, String FilePath)
        {
            this.Document = Document;
            this.FilePath = FilePath;
        }
        
        /*
         * if it's a document we first add the headers, otherwise we assume that the headers are in the first row
         */
        public async Task<bool> Serialize()
        {
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
                    }
                    foreach (Row row in (Document != null? Document.Rows : Rows))
                    {
                        short index = 0;
                        foreach (Value value in row.Values)
                        {
                            if (value.Type == typeof(String))
                            {
                                if (index == 0)
                                    await sw.WriteAsync(String.Format("\"{0}\"", value));
                                else
                                    await sw.WriteAsync(String.Format(",\"{0}\"", value));
                            }
                            else
                            {
                                if(index == 0)
                                    await sw.WriteAsync(value.ToString());
                                else
                                    await sw.WriteAsync("," + value.ToString());
                            }
                            ++index;
                        }
                        await sw.WriteLineAsync(sw.NewLine);
                    }
                    sw.Close();
                }
                    return true;
            }
            catch { return false; }
        }
    }
}
