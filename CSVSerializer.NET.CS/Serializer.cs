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
        private String FilePath;
        public Serializer(List<List<Value>> Values, String FilePath)
        {
            this.Values = Values;
        }

        public async Task<bool> Serialize()
        {
            try
            {
                if (!File.Exists(FilePath))
                    File.Create(FilePath);
                using (StreamWriter sw = new StreamWriter(FilePath, false))
                {
                    foreach (List<Value> lv in Values)
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
