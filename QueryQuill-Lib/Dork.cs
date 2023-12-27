using System;

namespace QueryQuill_Lib
{
    /// <summary>
    /// Class representing a generated dork, by its link or query.
    /// </summary>
    /// <param name="query">String representing the query.</param>
    public class Dork(string? query)
    {
        public Uri Link { get { return new($"https://google.com/search?q={Uri.EscapeDataString(Query)}"); } }

        public string Query { get; private init; } = query ?? "";
    }
}
