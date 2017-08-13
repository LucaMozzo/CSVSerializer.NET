using System;

namespace CSVSerializer
{
    /// <summary>
    /// Exception thrown whenever the file is not regognized to be a valid csv file
    /// </summary>
    public class Exception : System.Exception
    {
        /// <summary>
        /// Constructor for the message containing details of the exception
        /// </summary>
        /// <param name="message">Details of the exception</param>
        public Exception(string message) : base(message)
        {
            message = "The input file is not a valid CSV file!\n" + message;
        }
    }

    public class CSVDeserializationExcepion : System.Exception
    {
        /// <summary>
        /// Constructor for the message containing details of the exception
        /// </summary>
        /// <param name="message">Details of the exception</param>
        public CSVDeserializationExcepion(string message) : base(message) { }
    }
}
