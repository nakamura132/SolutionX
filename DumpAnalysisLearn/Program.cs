namespace DumpAnalysisLearn
{
    internal class Program
    {
        //private static List<A> _values = new List<A>();

        //static void Main( string[] args )
        //{
        //    for ( var i = 0; i < 100000; i++ )
        //    {
        //        _values.Add( new A() { ValueB = new B() } );
        //    }
        //    Console.ReadLine(); // ← ここで止まっているときにダンプをとる
        //}

        /// <summary>
        /// スタックオーバーフローをわざと発生させる
        /// </summary>
        /// <param name="args"></param>
        static void Main( string[] args )
        {
            HogeHoge( 1 );
        }
        static void HogeHoge( int s )
        {
            HogeHoge2( s );
        }
        static void HogeHoge2( int s )
        {
            HogeHoge( s );
        }
    }
    class A
    {
        public B ValueB { get; set; }
    }
    class B
    {
        public int Value { get; set; }
    }
}