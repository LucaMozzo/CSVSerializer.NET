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
        private StreamReader File;

        public Deserializer(String FilePath)
        {
            File = new StreamReader(FilePath);

        }
        public Deserializer(byte[] buffer)
        {
            this.File = new StreamReader(new MemoryStream(buffer));
        }

        public Document Deserialize()
        {

            return new Document(Headers, Rows);
        }

        public List<Row> GetValues()
        {

            return new List<Row>(); //temporary
        }
    }
}
