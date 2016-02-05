using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSerializer
{
    public class CSVFileExcepion : Exception
    {
        public CSVFileExcepion(string message) : base(message)
        {
            message = "The input file is not a valid CSV file!\n" + message;
        }
    }
}
