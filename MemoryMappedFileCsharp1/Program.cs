using System.IO.MemoryMappedFiles;
using System.Text;

namespace MemoryMappedFileCsharp1
{
    internal class Program
    {
        static void Main( string[] args )
        {
            // メモリマップトファイルの作成
            using ( var mmf = MemoryMappedFile.CreateOrOpen( "Local\\testmap", 1024 ) )
            {
                using ( var accessor = mmf.CreateViewAccessor() )
                {
                    // 書き込むメッセージ
                    string message = "Hello from C#";
                    byte[] buffer = Encoding.UTF8.GetBytes(message);

                    // データの長さを最初の位置に書き込む
                    accessor.Write( 0, buffer.Length );

                    // メッセージを書き込む
                    accessor.WriteArray( 4, buffer, 0, buffer.Length );

                    Console.WriteLine( "Message written to memory-mapped file: " + message );
                }
            }
        }
    }
}
