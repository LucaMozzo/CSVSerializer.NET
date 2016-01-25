using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVSerializer.NET.CS
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
        // TODO: comma appears at the end of the line, could be a problem
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
                        foreach(String Header in Document.Headers)
                            await sw.WriteAsync(String.Format("\"{0}\",", Header));
                        
                    }
                    foreach (Row row in (Document != null? Document.Rows : Rows))
                    {
                        foreach (Value value in row.Values)
                        {
                            if (value.Type == typeof(String))
                                await sw.WriteAsync(String.Format("\"{0}\",", value));
                            else
                                await sw.WriteAsync(value.ToString() + ",");
                        }
                        await sw.WriteLineAsync(String.Empty);
                    }
                    sw.Close();
                }
                    return true;
            }
            catch { return false; }
        }
    }
}
