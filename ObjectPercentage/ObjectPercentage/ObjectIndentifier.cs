using System.Collections;
using System.Reflection;

namespace ObjectPercentage;

public class ObjectIndentifier
{
    public int Count<T>(T item, int count)
    {
        var total = count;
        Type type = item.GetType();
        IEnumerable<PropertyInfo> propertyInfos = type.GetProperties();
        
        foreach (var prop in propertyInfos)
        {
            if (type.GetProperty(prop.Name).PropertyType.IsGenericType &&
                typeof(IEnumerable).IsAssignableFrom(type.GetProperty(prop.Name).PropertyType) && prop.GetValue(item) is not null)
            {
                var typeName = prop.GetValue(item).GetType().GenericTypeArguments.First();
                Console.WriteLine(typeName);
            }
        }

        return total;
    }
    
}