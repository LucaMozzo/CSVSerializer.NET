using System;

namespace CsvSerializer
{
    public class CsvDeserializationExcepion : Exception
    {
        /// <summary>
        /// Constructor for the message containing details of the exception
        /// </summary>
        /// <param name="message">Details of the exception</param>
        public CsvDeserializationExcepion(string message) : base(message) { }
    }
}
