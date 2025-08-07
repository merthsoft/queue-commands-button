using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merthsoft.QueueCommandsButton;

static class Extensions
{
    public static TValue GetOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default)
    {
        if (dictionary.TryGetValue(key, out TValue result))
            return result;

        dictionary[key] = defaultValue;
        return defaultValue;
    }
}
