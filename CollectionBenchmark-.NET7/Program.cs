using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

internal class Program
{
    private static void Main( string[] args )
    {
        //var summary = BenchmarkRunner.Run<BenchTarget1>();

        // Swither は複数ベンチマークを作りたい場合に便利
        var switcher = new BenchmarkSwitcher(
            new []
            {
                typeof(DictionaryBenchmark),
                typeof(StaticTypeCacheBenchmark),
                typeof(DictionaryKeyBenchmark),
                typeof(CultureInfoBenchmark)
            });

        args = new string[] { "0", "1", "2", "3" };
        switcher.Run( args );
    }
}

class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddDiagnoser( MemoryDiagnoser.Default );
        // ShortRunを使うとサクッと終わらせられる、デフォルトだと本気で長いので短めにしとく。
        // ShortRunは LaunchCount=1  TargetCount=3 WarmupCount = 3 のショートカット
        AddJob( Job.ShortRun
            .WithLaunchCount(1)
            // InProcessNoEmitToolChain を指定しないと Windows Defender に引っかかる
            //.WithToolchain(InProcessNoEmitToolchain.Instance)
            );
    }
    
}
