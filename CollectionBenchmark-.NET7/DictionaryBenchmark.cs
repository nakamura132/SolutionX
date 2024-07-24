using BenchmarkDotNet.Attributes;
using System.Collections;
using System.Collections.Concurrent;
// ベンチマーク
[Config( typeof( BenchmarkConfig ) )]
public class DictionaryBenchmark
{
    ConcurrentDictionary<Type, Action> concurrentDict;
    Dictionary<Type, Action> dict;
    Hashtable hashtable;

    Type key;

    [GlobalSetup]
    public void Setup()
    {
        concurrentDict = new ConcurrentDictionary<Type, Action>();
        dict = new Dictionary<Type, Action>();
        hashtable = new Hashtable();

        // 3000 件くらいを突っ込む
        foreach( var item in typeof( int ).Assembly.GetTypes()){
            concurrentDict.TryAdd( item, () => { } );
            dict[item] = () => { };
            hashtable[item] = new Action(() => { });
        }

        // int の lookup 速度を競う
        key = typeof( int );
    }

    // 戻り値を返すようにしているのは最適化で消滅しないようにするため
    [Benchmark]
    public Action Dictionary()
    {
        // 今回はマルチスレッド環境下をイメージするので lock を入れる
        lock( dict )
        {
            Action __;
            dict.TryGetValue( key, out __ );
            return __;
        }
    }

    [Benchmark]
    public Action HashTable()
    {
        var __ = hashtable[key];
        return ( Action )__;
    }

    // ConcurrentDictionary を基準にする
    [Benchmark(Baseline = true)]
    public Action ConcurrentDictionary()
    {
        Action __;
        concurrentDict.TryGetValue( key, out __ );
        return __;
    }
}