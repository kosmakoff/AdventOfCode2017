using System;
using System.Collections.Generic;

namespace Day08
{
    internal class Memory
    {
        public IDictionary<string, int> Data { get; } = new SortedDictionary<string, int>();

        public event EventHandler<KeyValuePair<string, int>> DataSet;

        public int GetValue(string name)
        {
            return Data.TryGetValue(name, out var value) ? value : 0;
        }

        public void SetValue(string name, int value)
        {
            Data[name] = value;
            OnDataSet(new KeyValuePair<string, int>(name, value));
        }

        public void DebugOutput()
        {
            foreach (var kvp in Data)
            {
                Console.WriteLine($"{kvp.Key} => {kvp.Value}");
            }
        }

        protected virtual void OnDataSet(KeyValuePair<string, int> e)
        {
            DataSet?.Invoke(this, e);
        }
    }
}
