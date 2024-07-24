using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCode
{
    internal class DelegateAllocation
    {
        static void Run( List<int> list, string format, bool cond )
        {
            // does not use lambda, but...
            if ( cond )
            {
                Console.WriteLine( "Do Nothing" );
                return;
            }

            // キャプチャが存在するメソッドを分けることで回避
            RunCore( list, format );
        }

        private static void RunCore( List<int> list, string format )
        {
            list.ForEach( x =>
            {
                Console.WriteLine( string.Format( format, x.ToString() ) );
            } );
        }

        //static readonly WaitCallback CallBack = RunInThreadPool;
        //public int X;

        //public void Bar()
        //{
        //    // Action<object> の state はそのためにある、this を入れるのが頻出パターン。
        //    // これはキャッシュされたデリゲートを使うためゼロアロケーション
        //    ThreadPool.QueueUserWorkItem( CallBack, this );
        //}

        //static void RunInThreadPool(object state )
        //{
        //    // state から this を引っ張ることでインスタンスメソッドを呼び出せる
        //    var self = (DelegateAllocation)state;
        //    self.Nanikasuru();
        //}

        //void Nanikasuru()
        //{
        //    Console.WriteLine("Nanika Suru:" + X);
        //}
    }
}
