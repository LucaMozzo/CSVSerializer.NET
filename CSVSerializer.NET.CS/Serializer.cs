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
        private List<List<Value>> Values;
        private Document Document;
        private String FilePath;
        public Serializer(List<List<Value>> Values, String FilePath)
        {
            this.Values = Values;
        }

        public Serializer(Document Document, String FilePath)
        {
            this.Document = Document;
        }
        // TODO: the comma appears at the end of the line, could be a problem
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
                    foreach (List<Value> lv in (Document != null? Document.Values : Values))
                    {
                        foreach (Value v in lv)
                        {
                            if (v.Type == typeof(String))
                                await sw.WriteAsync(String.Format("\"{0}\",", v));
                            else
                                await sw.WriteAsync(v.ToString() + ",");
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
