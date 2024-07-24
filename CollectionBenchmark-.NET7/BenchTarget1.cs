using BenchmarkDotNet.Attributes;

namespace CollectionBenchmark_.NET7
{
    public class BenchTarget1
    {
        const int Size = 100;

        static IEnumerable<int> _source = Enumerable.Range(0, Size);

        static int[] _array = _source.ToArray();

        [Benchmark]
        public int ArrayForBench()
        {
            int sum = 0;
            var target = _array;
            for ( int i = 0; i < target.Length; i++ ) { sum += target[i]; }
            return sum;
        }

        [Benchmark]
        public int ArrayForEachBench()
        {
            int sum = 0;
            var target = _array;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }

        [Benchmark]
        public int ArrayAsSpanForEachBench()
        {
            int sum = 0;
            var target = _array;
            foreach ( var item in target.AsSpan() ) { sum += item; }
            return sum;
        }

        static List<int> _list = _source.ToList();

        [Benchmark]
        public int ListForBench()
        {
            int sum = 0;
            var target = _list;
            for ( int i = 0; i < target.Count; i++ ) { sum += target[i]; }
            return sum;
        }
        [Benchmark]
        public int ListForEachBench()
        {
            int sum = 0;
            var target = _list;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }

        [Benchmark]
        public int ListAsSpanForEachBench()
        {
            int sum = 0;
            var target = _list;
            foreach ( var item in System.Runtime.InteropServices.CollectionsMarshal.AsSpan( target ) ) { sum += item; }
            return sum;
        }
        static LinkedList<int> _linkedList = new LinkedList<int>(_source);


        [Benchmark]
        public int LinkedListForEachBench()
        {
            int sum = 0;
            var target = _linkedList;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }

        static SortedSet<int> _sortedSet = new SortedSet<int>(_source);
        [Benchmark]
        public int SortedSetForEachBench()
        {
            int sum = 0;
            var target = _sortedSet;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }

        static HashSet<int> _hashSet = new HashSet<int>(_source);

        [Benchmark]
        public int HashSetForEachBench()
        {
            int sum = 0;
            var target = _hashSet;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }
        static Stack<int> _stack = new Stack<int>(_source);

        [Benchmark]
        public int StackForEachBench()
        {
            int sum = 0;
            var target = _stack;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }

        static Queue<int> _queue = new Queue<int>(_source);

        [Benchmark]
        public int QueueForEachBench()
        {
            int sum = 0;
            var target = _queue;
            foreach ( var item in target ) { sum += item; }
            return sum;
        }
    }
}
