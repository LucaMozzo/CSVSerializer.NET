using System;

namespace CSVSerializer
{
    /// <summary>
    /// Exception thrown whenever the file is not regognized to be a valid csv file
    /// </summary>
    public class CSVFileExcepion : Exception
    {
        /// <summary>
        /// Constructor for the message containing details of the exception
        /// </summary>
        /// <param name="message">Details of the exception</param>
        public CSVFileExcepion(string message) : base(message)
        {
            message = "The input file is not a valid CSV file!\n" + message;
        }
    }
}
