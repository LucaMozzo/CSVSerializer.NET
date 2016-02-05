using System;

namespace CSVSerializer
{
    /// <summary>
    /// Represents a generic value of an implicit type
    /// </summary>
    public class Value<T>
    {
        private T type;
        private object value;
        /// <summary>
        /// Constructor that accepts any object
        /// </summary>
        /// <param name="value">The value to assign to the object</param>
        public Value(object value)
        {
            this.value = value;
        }
        /// <summary>
        /// Getter method for the value
        /// </summary>
        /// <returns>The actual value</returns>
        public T GetValue()
        {
            return (T) value;
        }
        /// <summary>
        /// Method for updating the actual value of this Value
        /// </summary>
        /// <param name="NewValue">New value that will replace the old one</param>
        public void UpdateValue(Value<object> NewValue)
        {
            value = NewValue;
        }
        /// <summary>
        /// Override method for ToString
        /// </summary>
        /// <returns>The string representation of the object</returns>
        public override string ToString()
        {
            if (typeof(T) == typeof(String))
                return (string)value;
            else
                return Convert.ToString(value);
        }
    }
}
