using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVSerializer.NET.CS
{
    public class Deserializer
    {
        private List<String> Headers;
        private List<Row> Rows;
        private String FilePath;

        public Deserializer(String FilePath)
        {

        }
        public Deserializer(Stream File)
        {

        }

        public Document Deserialize()
        {

            return new Document(Headers, Rows);
        }

        public List<List<Value>> GetValues()
        {

            return new List<List<Value>>(); //temporary
        }
    }
}
