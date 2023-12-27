using System;

namespace QueryQuill_Lib
{
    /// <summary>
    /// Static class containing the dork generator.
    /// </summary>
    public static class DorkGenerator
    {
        /// <summary>
        /// Static method for generating the dork.
        /// </summary>
        /// <param name="dorkParams">Instance of <see cref="DorkParams"/> used for generation.</param>
        /// <returns>Returns an instance of a generated <see cref="Dork"/>.</returns>
        public static Dork GenerateDork(DorkParams dorkParams)
        {
            string? dorkQuery = null;
            string? dorkNewQueryParams;

            foreach (string dork in Enum.GetNames(typeof(DorksList)))
            {
                dorkNewQueryParams = null;
                foreach (string[] param in dorkParams.Params.Values)
                {
                    if (param[0] == dork)
                    {
                        if (dorkNewQueryParams is not null) { dorkNewQueryParams += " OR "; }

                        dorkNewQueryParams += $"{dork}:{param[1]}";

                        if (dork == "daterange") { dorkNewQueryParams += ".." + param[2]; }
                    }
                }

                if (dorkNewQueryParams is null) { continue; }
                if (dorkQuery is not null) { dorkQuery += " AND "; }

                dorkQuery += $"({dorkNewQueryParams})";
            }

            return new(dorkQuery);
        }
    }
}
