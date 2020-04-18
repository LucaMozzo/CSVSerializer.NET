using System;
using System.Collections.Generic;

namespace CSVSerializer
{
    /// <summary>
    /// Structured document for a better organization of the data
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Headers of the document (usually the first row)
        /// </summary>
        public List<String> Headers { get; }
        /// <summary>
        /// List of rows, which are the body of the document
        /// </summary>
        public List<Row> Rows { get; }
        /// <summary>
        /// Constructor that requires headers and rows
        /// </summary>
        /// <param name="Headers">Name of the columns in the document</param>
        /// <param name="Rows">Body of the document</param>
        public Document(List<String> Headers, List<Row> Rows)
        {
            this.Rows = Rows;
            this.Headers = Headers;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Document()
        {
            Headers = new List<String>();
            Rows = new List<Row>();
        }
        /// <summary>
        /// Set the header for the document
        /// </summary>
        /// <param name="header">Header of the document</param>
        public void SetHeader(Row header)
        {
            foreach (Value<object> v in header.Values)
                Headers.Add(v.ToString());
        }
        /// <summary>
        /// Add a row to the document
        /// </summary>
        /// <param name="row">The row to be added</param>
        public void AddRow(Row row)
        {
            Rows.Add(row);
        }

        /// <summary>
        /// Add a collection of rows to the document
        /// </summary>
        /// <param name="rows">The collection of rows</param>
        public void AddRows(IEnumerable<Row> rows)
        {
            Rows.AddRange(rows);
        }
        //TODO: Add indexed and allow more manipulation of the document, including removing rows and setting header from a List<String>
    }
}
