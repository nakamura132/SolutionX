namespace TimeProviderTry1
{
    internal class Program
    {
        static void Main()
        {
            //var timeProvider = TimeProvider.System;
            //var now = timeProvider.GetUtcNow();
            //Console.WriteLine( $"現在の時刻: {now}" );

            var customTime = new DateTimeOffset(2023, 7, 31, 12, 0, 0, TimeSpan.Zero);
            var timeProvider = new CustomDateTimeProvider(customTime);

            var now = timeProvider.GetUtcNow();
            Console.WriteLine($"カスタム時刻: {now}");
        }
    }

    // カスタム DateTimeProvider の作成と使用
    class CustomDateTimeProvider : TimeProvider
    {
        private readonly DateTimeOffset _customTime;
        public CustomDateTimeProvider(DateTimeOffset customTime)
        {
            _customTime = customTime;
        }

        public override DateTimeOffset GetUtcNow() => _customTime;
    }
}
