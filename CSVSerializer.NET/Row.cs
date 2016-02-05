using System.Collections.Generic;

namespace CSVSerializer
{
    /// <summary>
    /// A row is a set of values
    /// </summary>
    public class Row
    {
        /// <summary>
        /// The content of the row
        /// </summary>
        public List<Value> Values { get; }
        /// <summary>
        /// Constructor that requires a list of values
        /// </summary>
        /// <param name="Values">Values to be added</param>
        public Row(List<Value> Values)
        {
            this.Values = Values;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Row()
        {
            Values = new List<Value>();
        }
        /// <summary>
        /// Adds a value to the row
        /// </summary>
        /// <param name="value">Value to be added</param>
        public void AddValue(Value value)
        {
            Values.Add(value);
        }
        /// <summary>
        /// Update the value at the specified index
        /// </summary>
        /// <param name="Index">Index of the value to be updated</param>
        /// <param name="NewValue">Updated values</param>
        public void UpdateValue(int Index, Value NewValue)
        {
            try
            {
                Values[Index].UpdateValue(NewValue); //delegates the Value class to update the value
            }
            catch { }
        }
    }
}
