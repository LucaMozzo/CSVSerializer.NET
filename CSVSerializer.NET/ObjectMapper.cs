using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CSVSerializer
{
    public class ObjectMapper<T>
    {
        private Dictionary<int, Tuple<string, string>> propertyMap = new Dictionary<int, Tuple<string, string>>();
        // mapped property name => transform
        private Dictionary<string, Delegate> transformFunctions = new Dictionary<string, Delegate>();

        public ObjectMapper<T> AddMap<TMember>(Expression<Func<T, TMember>> expression, int? index = null, string header = null)
        {
            return AddMap<TMember, TMember>(expression, null, index, header);
        }

        public ObjectMapper<T> AddMap<TMember, TOut>(Expression<Func<T, TMember>> expression, Func<TMember, TOut> transformFunction = null, int? index = null, string header = null)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            MemberInfo memberInfo = memberExpression.Member;
            if (memberInfo.MemberType == MemberTypes.Property)
            {
                var propertyNameAlias = new Tuple<string, string>(memberInfo.Name, header == null ? memberInfo.Name : header);
                propertyMap.Add(ResolveKey(index), propertyNameAlias);

                if (transformFunction != null)
                {
                    transformFunctions.Add(propertyNameAlias.Item2, transformFunction);
                }
            }

            return this;
        }

        internal Dictionary<int, Tuple<string, string>> GetPropertyMap() => propertyMap;

        internal Dictionary<string, Delegate> GetTransformFunctions() => transformFunctions;

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
