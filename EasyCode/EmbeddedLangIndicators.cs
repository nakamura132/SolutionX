using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCode
{
    /// <summary>
    /// 埋め込み言語
    /// </summary>
    internal class EmbeddedLangIndicators
    {
        public EmbeddedLangIndicators()
        {
            // https://github.com/dotnet/roslyn/tree/main/src/Features/Core/Portable/EmbeddedLanguages

            var y1 = """
                (?<name>\w+?\d{3})\.txt
                """;

            // lang=regex
            var y2 = """
                (?<name>\w+?\d{3})\.txt
                """;

            var y3 = """
                { value: 13,'name': 'abc',"data": [ true, 1.2, null, {}, new Date() ] }
                """;

            // lang=json
            var y4 = """
                { value: 13,'name': 'abc',"data": [ true, 1.2, null, {}, new Date() ] }
                """;

            var y5 = """
                yyyyMMdd
                """;

            // lang=DateAndTime
            var y6 = """
                yyyyMMdd
                """;
        }
    }
}
