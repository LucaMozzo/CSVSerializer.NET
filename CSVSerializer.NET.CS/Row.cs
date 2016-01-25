using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSerializer.NET.CS
{
    public class Row
    {
        public List<Value> Values { get; }
        public Row(List<Value> Values)
        {
            this.Values = Values;
        }

        public void UpdateValue(int Index, Value NewValue)
        {
            //delegates the Value class to update the value
            Values[Index].UpdateValue(NewValue);
        }
    }
}
