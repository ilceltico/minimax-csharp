using System;
using System.Collections.Generic;

namespace minimax.core.framework
{
    /// <summary>
    /// From aima-csharp
    /// Stores key-value pairs for efficiency analysis.
    ///
    /// @author Ravi Mohan
    /// @author Ruediger Lunde
    /// </summary>
    public class Metrics
    {
        private Dictionary<String, String> hash;

        public Metrics()
        {
            this.hash = new Dictionary<String, String>(); //Faster than Hashtable
        }

        public void Set(String name, int i)
        {
            hash[name] = i.ToString();
        }

        public void Set(String name, double d)
        {
            hash[name] = d.ToString();
        }

        public void IncrementInt(String name)
        {
            Set(name, GetInt(name) + 1);
        }

        public void Set(String name, long l)
        {
            hash[name] = l.ToString();
        }

        public int GetInt(String name)
        {
            return int.Parse(hash[name]);
        }

        public double GetDouble(String name)
        {
            return double.Parse(hash[name]);
        }

        public long GetLong(String name)
        {
            return long.Parse(hash[name]);
        }

        public String Get(String name)
        {
            return hash[name];
        }

        public HashSet<String> KeySet()
        {
            return new HashSet<string>(hash.Keys);
        }

        override public String ToString()
        {
            SortedDictionary<String, String> map = new SortedDictionary<String, String>(hash);
            return map.ToString();
        }
    }
}
