using System;
using System.IO;

namespace CSVSerializer
{
    /// <summary>
    ///  This class allows you to deserialize a CSV file or an array of bytes
    /// </summary>
    public class Deserializer
    {
        private StreamReader File;
        /// <summary>
        /// Constructor that requires the path of the file. It may throw a FileNotFoundException, deal with it
        /// </summary>
        /// <param name="FilePath">The path of the csv file</param>
        public Deserializer(String FilePath)
        {
                File = new StreamReader(FilePath); //may throw an exception if the file is not found, to be handled by the final user
        }
        /// <summary>
        /// Constructor that requires the content of the file
        /// </summary>
        /// <param name="buffer">An array of bytes that represent the csv file</param>
        public Deserializer(byte[] buffer)
        {
            this.File = new StreamReader(new MemoryStream(buffer));
        }
        /// <summary>
        /// Deserialize the file defined in the constructor
        /// </summary>
        /// <returns>A Document containing the deserialized values</returns>
        public Document Deserialize()
        {
            Document doc = new Document();
            int index = 0;
            while (!File.EndOfStream)
            {
                Row row = new Row();
                char[] line = File.ReadLine().ToCharArray();
                char[] buffer = new char[line.Length]; //temporary buffer for composing values
                int bufferIndex = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\"')
                    {
                        int closingQuoteIndex = IndexOf(line, '\"', i + 1); //copies all the stuff between the double quotes in the buffer
                        if (closingQuoteIndex == -1)
                            throw new CSVFileExcepion("Double quote mismatched");
                        for (int a = i + 1; a < closingQuoteIndex; a++)
                        {
                            buffer[bufferIndex] = line[a];
                            ++bufferIndex;
                        }
                        i = closingQuoteIndex; //the index is now the closing double quote, next loop it will be the ,
                    }           
                    else if (line[i] == ',') //when it sees a comma, it dumps the content of the buffer in the row
                    {
                        DumpBuffer(row, buffer);
                        buffer = new char[line.Length]; //clears the array after dumping values
                        bufferIndex = 0;
                    }
                    else
                    {
                        buffer[bufferIndex] = line[i];
                        ++bufferIndex;
                    }
                }
                DumpBuffer(row, buffer); //the last column must be added
                if (index == 0) //the first row is the header
                    doc.SetHeader(row);
                else
                    doc.AddRow(row);
                ++index;
            }
            return doc;
        }
        //for a better reusability I created this method to avoid redundancy
        private static void DumpBuffer(Row row, char[] buffer)
        {
            String tmp = "";
            foreach (char c in buffer)
                if (c != '\0')
                    tmp += c;
                else break; //otherwise keeps looping through the empty slot of the buffer
            Value value = new Value(tmp);
            row.AddValue(value);
        }

        //returns the index of the first occurrence of the specified element
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
