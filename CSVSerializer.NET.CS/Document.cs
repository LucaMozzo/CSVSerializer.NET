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
        public List<Row> Rows { get; }
        public Document(List<String> Headers, List<Row> Rows)
        {
            this.Rows = Rows;
            this.Headers = Headers;
        }
        public Document()
        {
            Headers = new List<String>();
            Rows = new List<Row>();
        }
        public void AddHeader(Value header)
        {
            Headers.Add(header.ToString());
        }
        public void AddRow(Row row)
        {
            Rows.Add(row);
        }
    }
}
