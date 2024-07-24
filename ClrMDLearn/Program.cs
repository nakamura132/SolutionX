using Microsoft.Diagnostics.Runtime;

namespace ClrMDLearn
{
    internal class Program
    {
        static void Main( string[] args )
        {
            int pid = System.Diagnostics.Process.GetProcessesByName("MyTestApp")[0].Id;
            using ( DataTarget dataTarget = DataTarget.AttachToProcess( pid, true ) ) //プロセスにアタッチ
            //using (DataTarget dataTarget = DataTarget.LoadDump(@"")) //ダンプ読み込み
            {
                foreach(ClrInfo version in dataTarget.ClrVersions )
                {
                    Console.WriteLine( $"Found CLR Version: {version.Version}" );

                    // This is data needed to request the dac from the symbol server.
                    ModuleInfo dacInfo = version.ModuleInfo;
                    Console.WriteLine( $"Filesize:  {dacInfo.IndexFileSize:X}" );
                    Console.WriteLine( $"Timestamp: {dacInfo.IndexTimeStamp:X}" );
                    Console.WriteLine( $"Dac File:  {dacInfo.FileName}" );

                    ClrRuntime runtime = version.CreateRuntime();
                    foreach(ClrAppDomain domain in runtime.AppDomains )
                    {
                        Console.WriteLine( $"ID:     {domain.Id}");
                        Console.WriteLine($"Name:    {domain.Name}");
                        Console.WriteLine($"Address: {domain.Address}");
                    }

                    foreach ( ClrThread thread in runtime.Threads )
                    {
                        if ( !thread.IsAlive )
                            continue;

                        Console.WriteLine( "Thread {0:X}:", thread.OSThreadId );

                        foreach ( ClrStackFrame frame in thread.EnumerateStackTrace() )
                            Console.WriteLine( "{0,12:X} {1,12:X} {2}", frame.StackPointer, frame.InstructionPointer, frame.ToString() );

                        Console.WriteLine();
                    }

                    //foreach ( var region in ( from r in runtime.EnumerateMemoryRegions()
                    //                          where r.Type != ClrMemoryRegionType.ReservedGCSegment
                    //                          group r by r.Type into g
                    //                          let total = g.Sum( p => (uint)p.Size )
                    //                          orderby total descending
                    //                          select new
                    //                          {
                    //                              TotalSize = total,
                    //                              Count = g.Count(),
                    //                              Type = g.Key
                    //                          } ) )
                    //{
                    //    Console.WriteLine( "{0,6:n0} {1,12:n0} {2}", region.Count, region.TotalSize, region.Type.ToString() );
                    //}
                    var heap = runtime.Heap;

                    //foreach ( ulong obj in runtime.EnumerateFinalizerQueueObjectAddresses() )
                    //{
                    //    var type = heap.GetObjectType(obj);

                    //    // If heap corruption, continue past this object.
                    //    if ( type == null )
                    //        continue;

                    //    ulong size = type.GetSize(obj);
                    //    Console.WriteLine( "{0,12:X} {1,8:n0} {2}", obj, size, type.Name );
                    //}
                    foreach ( var handle in runtime.EnumerateHandles() )
                    {
                        string objectType = heap.GetObjectType(handle.Object).Name;
                        Console.WriteLine( "{0,12:X} {1,12:X} {2,12} {3}", handle.Address, handle.Object, handle.GetType().ToString(), objectType );
                    }

                    Console.WriteLine( "{0,12} {1,12} {2,12} {3,12} {4,4} {5}", "Start", "End", "Committed", "Reserved", "Heap", "Type" );
                    //foreach ( var segment in heap.Segments )
                    //{
                    //    string type;
                    //    if ( segment.IsEphemeral )
                    //        type = "Ephemeral";
                    //    else if ( segment.IsLarge )
                    //        type = "Large";
                    //    else
                    //        type = "Gen2";

                    //    Console.WriteLine( "{0,12:X} {1,12:X} {2,12:X} {3,12:X} {4,4} {5}", segment.Start, segment.End, segment.CommittedEnd, segment.ReservedEnd, segment.ProcessorAffinity, type );
                    //}

                    //foreach ( var item in ( from seg in heap.Segments
                    //                        group seg by seg.ProcessorAffinity into g
                    //                        orderby g.Key
                    //                        select new
                    //                        {
                    //                            Heap = g.Key,
                    //                            Size = g.Sum( p => (uint)p.Length )
                    //                        } ) )
                    //{
                    //    Console.WriteLine( "Heap {0,2}: {1:n0} bytes", item.Heap, item.Size );
                    //}

                    //if ( !heap.CanWalkHeap )
                    //{
                    //    Console.WriteLine( "Cannot walk the heap!" );
                    //}
                    //else
                    //{
                    //    foreach ( var seg in heap.Segments )
                    //    {
                    //        for ( ulong obj = seg.FirstObject; obj != 0; obj = seg.NextObject( obj ) )
                    //        {
                    //            var type = heap.GetObjectType(obj);

                    //            // If heap corruption, continue past this object.
                    //            if ( type == null )
                    //                continue;

                    //            ulong size = type.GetSize(obj);
                    //            Console.WriteLine( "{0,12:X} {1,8:n0} {2,1:n0} {3}", obj, size, seg.GetGeneration( obj ), type.Name );
                    //        }
                    //    }
                    //}

                    //foreach ( var type in heap.EnumerateTypes() )
                    //{
                    //    foreach ( var appDomain in runtime.AppDomains )
                    //    {
                    //        foreach ( var thread in runtime.Threads )
                    //        {
                    //            foreach ( var field in type.ThreadStaticFields )
                    //            {
                    //                if ( field.HasSimpleValue )
                    //                    Console.WriteLine( "{0}.{1} ({2}, {3:X}) = {4}", type.Name, field.Name, appDomain.Id, thread.OSThreadId, field.GetValue( appDomain, thread ) );
                    //            }
                    //        }
                    //    }
                    //}
                }
                //string dacLocation = dataTarget.ClrVersions[0].DataTarget.(); //DAC の場所を取得
                //ClrRuntime runtime = dataTarget.CreateRuntime(dacLocation); //ランタイムを作成

                //ClrHeap heap = runtime.GetHeap(); //ヒープ情報を取得
                //foreach ( ulong obj in heap.EnumerateObjects() )
                //{
                //    ClrType type = heap.GetObjectType(obj); //ヒープにあるオブジェクトのタイプを取得
                //    ulong size = type.GetSize(obj); //ヒープにあるオブジェクトのサイズを取得
                //    System.Console.WriteLine( "{0,12:X} {1,8:n0} {2}", obj, size, type.Name ); //出力
                //}
            }
        }
    }
}