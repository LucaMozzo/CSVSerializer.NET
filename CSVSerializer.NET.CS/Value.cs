using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSerializer
{
    public class Value
    {
        internal Type Type { get; }
        private object value;

        public Value(int v)
        {
            value = v;
            Type = typeof(int);
        }
        public Value(double v)
        {
            value = v;
            Type = typeof(double);
        }
        public Value(String v)
        {
            value = v;
            Type = typeof(String);
        }
        public Type GetValue()
        {
            return (Type) value;
        }
        public void UpdateValue(Value NewValue)
        {
            value = NewValue;
        }

        public override string ToString()
        {
            if (Type == typeof(String))
                return (String) value;
            else
                return base.ToString();
        }
    }
}
