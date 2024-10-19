using System.Text;
using WorkWithFilesApi.Models;

namespace WorkWithFilesApi.Helpers
{
    public static class HtmlGenerator
    {
        public static string GetHtmlGames(List<Game> games)
        {
            var sb = new StringBuilder();
            sb.Append(
                @"
                <html>
                    <head></head>
                    <body>
                        <div>
                            <h1>Pdf Document<h1>
                            <h2>Games<h2>
                        </div>
                        <table>
                            <tr>
                                <th>Game</th>
                                <th>Genre</th>
                                <th>Platform</th>
                                <th>Release</th>
                            </tr>"
                );

            foreach (var game in games)
            {
                sb.AppendFormat(@"
                    <tr>
                        <td>{0}</td>
                        <td>{1}</td>
                        <td>{2}</td>
                        <td>{3}</td>
                    </tr>
                    ", game.Title, game.Genre, game.Platform, game.Release);
            }

            sb.Append(
                    @"
                        </table>
                    </body>
                </html>");

            return sb.ToString();
        }
    }
}
