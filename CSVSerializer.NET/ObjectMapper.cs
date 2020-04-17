using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CSVSerializer
{
    public class ObjectMapper<T>
    {
        private Dictionary<int, Tuple<string, string>> propertyMap = new Dictionary<int, Tuple<string, string>>();

        public ObjectMapper<T> AddMap<TMember>(Expression<Func<T, TMember>> expression, int? index = null, string header = null)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            MemberInfo memberInfo = memberExpression.Member;
            if (memberInfo.MemberType == MemberTypes.Property)
            {
                var propertyNameAlias = new Tuple<string, string>(memberInfo.Name, header == null ? memberInfo.Name : header);
                propertyMap.Add(ResolveKey(index), propertyNameAlias);
            }

            return this;
        }

        internal Dictionary<int, Tuple<string, string>> GetPropertyMap()
        {
            return propertyMap;
        }

        protected int ResolveKey(int? index)
        {
            int currentIndex;
            if (index.HasValue)
            {
                currentIndex = index.Value;
            }
            else
            {
                currentIndex = 0;
                while (propertyMap.ContainsKey(currentIndex))
                {
                    ++currentIndex;
                }
            }

            return currentIndex;
        }
    }
}
