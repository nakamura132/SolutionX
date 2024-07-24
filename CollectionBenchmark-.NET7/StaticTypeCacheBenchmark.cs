using BenchmarkDotNet.Attributes;

[Config( typeof( BenchmarkConfig ) )]
public class StaticTypeCacheBenchmark
{
    private IDictionary<Type, int> _dictionary => new Dictionary<Type, int>
    {
        { typeof(int), 0 },
        { typeof(uint), 1 },
        { typeof(short), 2 },
        { typeof(ushort), 3 },
        { typeof(long), 4 },
        { typeof(ulong), 5 },
        { typeof(float), 6 },
        { typeof(double), 7 },
        { typeof(decimal), 8 },
        { typeof(string), 9 },
        { typeof(bool), 10 },
        { typeof(List<int>), 11 },
        { typeof(List<uint>), 12 },
        { typeof(List<short>), 13 },
        { typeof(List<ushort>), 14 },
        { typeof(List<long>), 15 },
        { typeof(List<ulong>), 16 },
        { typeof(List<float>), 17 },
        { typeof(List<double>), 18 },
        { typeof(List<decimal>), 19 },
        { typeof(List<string>), 20 },
        { typeof(List<bool>), 21 },
    };

    public StaticTypeCacheBenchmark()
    {
        
        int i = 0;
        StaticTypeCache<int>.Value = i++;
        StaticTypeCache<uint>.Value = i++;
        StaticTypeCache<short>.Value = i++;
        StaticTypeCache<ushort>.Value = i++;
        StaticTypeCache<float>.Value = i++;
        StaticTypeCache<double>.Value = i++;
        StaticTypeCache<decimal>.Value = i++;
        StaticTypeCache<long>.Value = i++;
        StaticTypeCache<ulong>.Value = i++;
        StaticTypeCache<string>.Value = i++;
        StaticTypeCache<bool>.Value = i++;
        StaticTypeCache<List<int>>.Value = i++;
        StaticTypeCache<List<uint>>.Value = i++;
        StaticTypeCache<List<short>>.Value = i++;
        StaticTypeCache<List<ushort>>.Value = i++;
        StaticTypeCache<List<float>>.Value = i++;
        StaticTypeCache<List<double>>.Value = i++;
        StaticTypeCache<List<decimal>>.Value = i++;
        StaticTypeCache<List<long>>.Value = i++;
        StaticTypeCache<List<ulong>>.Value = i++;
        StaticTypeCache<List<string>>.Value = i++;
        StaticTypeCache<List<bool>>.Value = i++;
    }

    [Benchmark]
    public int GetFromDictionary()
    {
        var ret = 0;
        for ( int i = 0; i < 100; i++ )
        {
            ret += _dictionary[typeof( int )]
            + _dictionary[typeof( uint )]
            + _dictionary[typeof( short )]
            + _dictionary[typeof( ushort )]
            + _dictionary[typeof( float )]
            + _dictionary[typeof( double )]
            + _dictionary[typeof( decimal )]
            + _dictionary[typeof( long )]
            + _dictionary[typeof( ulong )]
            + _dictionary[typeof( string )]
            + _dictionary[typeof( bool )]
            + _dictionary[typeof( List<int> )]
            + _dictionary[typeof( List<uint> )]
            + _dictionary[typeof( List<short> )]
            + _dictionary[typeof( List<ushort> )]
            + _dictionary[typeof( List<float> )]
            + _dictionary[typeof( List<double> )]
            + _dictionary[typeof( List<decimal> )]
            + _dictionary[typeof( List<long> )]
            + _dictionary[typeof( List<ulong> )]
            + _dictionary[typeof( List<string> )]
            + _dictionary[typeof( List<bool> )];
        }
        return ret;
    }

    [Benchmark]
    public int GetFromCache()
    {
        var ret = 0;
        for ( int i = 0; i < 100; i++ )
        {
            ret += StaticTypeCache<int>.Value
                + StaticTypeCache<uint>.Value
                + StaticTypeCache<short>.Value
                + StaticTypeCache<ushort>.Value
                + StaticTypeCache<float>.Value
                + StaticTypeCache<double>.Value
                + StaticTypeCache<decimal>.Value
                + StaticTypeCache<long>.Value
                + StaticTypeCache<ulong>.Value
                + StaticTypeCache<string>.Value
                + StaticTypeCache<bool>.Value
                + StaticTypeCache<List<int>>.Value
                + StaticTypeCache<List<uint>>.Value
                + StaticTypeCache<List<short>>.Value
                + StaticTypeCache<List<ushort>>.Value
                + StaticTypeCache<List<float>>.Value
                + StaticTypeCache<List<double>>.Value
                + StaticTypeCache<List<decimal>>.Value
                + StaticTypeCache<List<long>>.Value
                + StaticTypeCache<List<ulong>>.Value
                + StaticTypeCache<List<string>>.Value
                + StaticTypeCache<List<bool>>.Value;
        }
        return ret;
    }
}