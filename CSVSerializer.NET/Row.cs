using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVSerializer
{
    public class Row
    {
        public List<Value> Values { get; }
        public Row(List<Value> Values)
        {
            this.Values = Values;
        }
        public Row()
        {
            Values = new List<Value>();
        }
        public void AddValue(Value v)
        {
            Values.Add(v);
        }
        public void UpdateValue(int Index, Value NewValue)
        {
            //delegates the Value class to update the value
            try
            {
                Values[Index].UpdateValue(NewValue);
            }
            catch { }
        }
    }
}
