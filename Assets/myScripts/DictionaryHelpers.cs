 using System.Collections.Generic;
 using System.Reflection;
 using System.Linq;

public static class DictionaryHelpers
{
    public static Dictionary<string, dynamic> ToDictionary(object obj)
    {
        BindingFlags publicAttributes = BindingFlags.Public | BindingFlags.Instance;
        Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();

        foreach (PropertyInfo property in obj.GetType().GetProperties(publicAttributes))
        {
            if (property.CanRead)
            {
                dictionary.Add(property.Name, property.GetValue(obj, null));
            }
        }

        return dictionary;
    }

    public static U GetOrDefault<T, U>(this Dictionary<T, U> dict, T key)
    {
        if (dict.ContainsKey(key)) return dict[key];
        return default(U);
    }

    public static bool Contains<T, U>(this Dictionary<T, U> dict, System.Func<KeyValuePair<T,U>, bool> test)
    {
        foreach (var pair in dict) {
            if (test(pair)) {
                return true;
            }
        }
        return false;
    }

    // Enables 'foreach' using tuples on a Dictionary
    //  e.g.  foreach (var (key, val) in dict.EnumerableTuples()) { ... }
    public static IEnumerable<(T,U)> EnumerableTuples<T,U>(this Dictionary<T,U> dict)
    {
        return dict.Select(x => (x.Key, x.Value));
    }
}