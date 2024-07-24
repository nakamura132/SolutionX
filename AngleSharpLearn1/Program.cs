using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Io;
using System;
using System.Linq;
using System.Text.RegularExpressions;


// コンフィグレーションを作成
var config = Configuration.Default.WithDefaultLoader();

// コンテキストを作成
var context = BrowsingContext.New(config);

// Edge の User-Agent をセット
var requester = context.GetService<DefaultHttpRequester>();
requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 Edg/118.0.2088.61";

// HTMLをパースしてドキュメントオブジェクトを取得
IDocument doc = await context.OpenAsync("https://neue.cc/");

// 基本、QuerySelectorかQuerySelectorAllでDOMを絞り込む
//var anchors = doc.QuerySelectorAll(".side_body:last-child a")
//    .Cast<IHtmlAnchorElement>() // AngleSharp.Html.Dom
//    .Select(x => x.Href)
//    .ToArray();

var anchors = doc.QuerySelectorAll<IHtmlAnchorElement>(".side_body:last-child a")
    .Select(x => x.Href)
    .ToList();

var yearGrouped = anchors
    .Select( x =>
    {
        var match = Regex.Match(x, """(\d+)/(\d+)""");
        return new
        {
            Url = x,
            Year = int.Parse(match.Groups[1].Value),
            Month = int.Parse(match?.Groups[2].Value),
        };
    })
    .GroupBy( x => x.Year);

var source = """
    <head>
    <style>
        .styled-table{
            /* セルの罫線の間にスペースがないことを確認する */
            border-collapse: collapse;
            margin: 25px 0;
            font-size: 0.9em;
            font-family: sans-serif;
            min-width: 400px;
            /* テーブルの周囲に微妙な透明な影を追加します */
            box-shadow: 0 0 20px rgba(0,0,0,0.15);
        }
        .styled-table thead tr{
            background-color: #009879;
            color: #ffffff;
            text-align: left;
        }
        .styled-table th,
        .styled-table td {
            padding: 12px 15px;
        }
        .styled-table tbody tr {
            /* 各行に下罫線を追加して区切る */
            border-bottom: 1px solid #dddddd;
        }
        .styled-table tbody tr:nth-of-type(even){
            /* 読みやすくするために、2 行ごとに明るい背景を追加します */
            background-color: #f3f3f3;
        }
        .styled-table tbody tr:last-of-type{
            /* 最後の行に暗い境界線を追加して、テーブルの終わりを示します */
            border-bottom: 2px solid #009879;
        }
    </style>
    </head>
    <body>
        <table class="styled-table">
            <caption>neue cc</caption>
            <thead>
                <tr>
                    <th>Month</th>
                    <th>Title</th>
                    <th>Link</th>
                </tr>
            </thead>
            <tbody>
            </tbody>

        </table>
    </body>
    """;
var parser = context.GetService<IHtmlParser>();
var newDoc = await parser.ParseDocumentAsync(source);
var tbody = newDoc.QuerySelector<IHtmlTableSectionElement>("tbody");

// <tbody> タグ配下に全ての行を詰め込む
foreach(var year in yearGrouped.OrderBy(x => x.Key))
{
    //Console.WriteLine( $"[{year.Key}] Completed." );
    foreach(var month in year.OrderBy(x => x.Month) )
    {
        Console.WriteLine( $"[{month.Year}/{month.Month:00}] Completed." );

        var html = await context.OpenAsync(month.Url);
        var titles = html.QuerySelectorAll<IHtmlAnchorElement>("h1 a")
            .Select(x => new { x.Text, x.Href});

        // yyyy/MM 単位で行をグルーピングする
        var td1 = newDoc.CreateElement<IHtmlTableDataCellElement>();
        var articleCount = titles.Count();
        td1.SetAttribute( "rowspan", articleCount.ToString() );
        td1.TextContent = $"{month.Year}/{month.Month:00}";

        bool first = true;
        foreach( var title in titles )
        {
            // タイトルごとに1行作る
            var tr = newDoc.CreateElement<IHtmlTableRowElement>();
            if( first )
            {
                first = false;
                tr.AppendChild( td1 );
            }
            // タイトル
            var td2 = newDoc.CreateElement<IHtmlTableDataCellElement>();
            td2.TextContent = title.Text;
            // リンク
            var td3 = newDoc.CreateElement<IHtmlTableDataCellElement>() ;
            // <a>タグ
            var anchor = newDoc.CreateElement<IHtmlAnchorElement>();
            anchor.SetAttribute( "href", title.Href );
            anchor.TextContent = title.Href;
            td3.AppendChild( anchor );
            
            tr.AppendChild( td2 );
            tr.AppendChild( td3 );
            tbody.AppendChild( tr );
        }
    }
}
Console.WriteLine( newDoc.DocumentElement.OuterHtml );
return;