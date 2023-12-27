using System.Collections.Generic;

namespace QueryQuill_Lib
{
    /// <summary>
    /// Class representing the parameters of a dork.
    /// </summary>
    public class DorkParams
    {
        public Dictionary<int, string[]> Params { get; private set; } = [];

        /// <summary>
        /// Method that adds a parameter to a dork.
        /// </summary>
        /// <param name="dorkParamDescription">Array representing the dork parameter in the form [dorkParamName, param1, param2 || null].</param>
        /// <param name="dorkKey">Integer representing the unique key of this parameter.</param>
        public void AddParam(string[] dorkParamDescription, int dorkKey) { Params.Add(dorkKey, dorkParamDescription); }

        /// <summary>
        /// Method that removes a specific parameter from a dork.
        /// </summary>
        /// <param name="dorkKey">Integer representing the unique key of the parameter to be removed.</param>
        public void RemoveParam(int dorkKey) { Params.Remove(dorkKey); }
    }
}
