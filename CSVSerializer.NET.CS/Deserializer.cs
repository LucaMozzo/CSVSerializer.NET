using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSVSerializer
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
            Document doc = new Document();
            int index = 0;
            while (!File.EndOfStream)
            {
                Row row = new Row();
                char[] line = File.ReadLine().ToCharArray();
                char[] buffer = new char[line.Length]; //temporary buffer for composing values
                for (int i = 0; i < line.Length; i++)
                {
                    int bufferIndex = 0;
                    if (line[i] == '\"')
                    {
                        int closingQuoteIndex = IndexOf(line, '\"', i + 1);
                        for (int a = i + 1; a < closingQuoteIndex; a++)
                        {
                            buffer[bufferIndex] = line[a];
                            ++bufferIndex;
                        }
                        i = closingQuoteIndex;
                    }           
                    else if (line[i] == ',')
                    {
                        String tmp = "";
                        foreach (char c in buffer)
                            tmp += c;
                        Value value = new Value(tmp);
                        row.AddValue(value);
                    }
                    else
                    {
                        buffer[bufferIndex] = line[i];
                        ++bufferIndex;
                    }
                }
                if (index == 0)
                    doc.SetHeader(row);
                else
                    doc.AddRow(row);
                ++index;
            }
            return doc;
        }

        //returns the index of the first occurrence of the spacified element
        private int IndexOf(char[] array, char element, int startingIndex = 0)
        {
            for(int i = startingIndex; i < array.Length; i++)
            {
                if (array[i] == element)
                    return i;
            }
            return -1;
        }
    }
}
