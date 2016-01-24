using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSerializer.NET.CS
{
    public class Document
    {
        public List<String> Headers { get; }
        public List<List<Value>> Values { get; }
        public Document(List<String> Headers, List<List<Value>> Values)
        {
            this.Values = Values;
            this.Headers = Headers;
        }


    }
}
