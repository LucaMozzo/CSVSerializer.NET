using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSerializer.NET.CS
{
    internal class Value
    {
        private Type Type;
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
        public Type getValue()
        {
            return (Type) value;
        }
    }
}
