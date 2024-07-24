using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Config(typeof(BenchmarkConfig))]
public class DictionaryKeyBenchmark
{
    Dictionary<int, Action> intDictionary;
    Dictionary<string, Action> stringDictionary;
    Dictionary<string, Action> longStringDictionary;

    int key;
    string guidKey;

    [GlobalSetup]
    public void Setup()
    {
        intDictionary = new Dictionary<int, Action>();
        stringDictionary = new Dictionary<string, Action>();
        longStringDictionary = new Dictionary<string, Action>();

        key = Random.Shared.Next( 0, 10000 );

        foreach(int i in Enumerable.Range(0, 10000))
        {
            intDictionary[i] = () => { };
            stringDictionary[i.ToString()] = () => { };
            var guid = Guid.NewGuid().ToString("N");
            longStringDictionary[guid] = () => { };

            if( i == key )
            {
                guidKey = guid;
            }
        }
    }

    [Benchmark]
    public Action IntDictionary()
    {
        Action action;
        intDictionary.TryGetValue(key, out action);
        return action;
    }
    [Benchmark]
    public Action StringDictionary()
    {
        Action action;
        stringDictionary.TryGetValue(key.ToString(), out action);
        return action;
    }
    [Benchmark]
    public Action LongStringDictionary()
    {
        Action action;
        longStringDictionary.TryGetValue(guidKey, out action);
        return action;
    }
}
