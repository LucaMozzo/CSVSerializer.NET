using System;

namespace CSVSerializer
{
    /// <summary>
    /// Represents a value of an implicit type
    /// </summary>
    public class Value
    {
        internal Type Type { get; }
        private object value;
        /// <summary>
        /// Constructor for integer values
        /// </summary>
        /// <param name="v">Integer value</param>
        public Value(int v)
        {
            value = v;
            Type = typeof(int);
        }
        /// <summary>
        /// Constructor for double value
        /// </summary>
        /// <param name="v">Double value</param>
        public Value(double v)
        {
            value = v;
            Type = typeof(double);
        }
        /// <summary>
        /// Constructor for string values
        /// </summary>
        /// <param name="v">String value</param>
        public Value(String v)
        {
            value = v;
            Type = typeof(String);
        }
        /// <summary>
        /// Constructor for specified types
        /// </summary>
        /// <param name="v">Value casted to any type</param>
        /// <param name="t">Explicit type of the value</param>
        public Value(object v, Type t)
        {
            Type = t;
            value = v;
        }
        /// <summary>
        /// Getter method for the value
        /// </summary>
        /// <returns>The actual value</returns>
        public Type GetValue()
        {
            return (Type) value;
        }
        /// <summary>
        /// Method for updating the actual value of this Value
        /// </summary>
        /// <param name="NewValue">New value that will replace the old one</param>
        public void UpdateValue(Value NewValue)
        {
            value = NewValue;
        }
        /// <summary>
        /// Override method for ToString
        /// </summary>
        /// <returns>The string representation of the object</returns>
        public override string ToString()
        {
            if (Type == typeof(String))
                return (String) value;
            else
                return base.ToString();
        }
    }
}
