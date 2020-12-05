using System.Collections.Generic;

namespace Day4
{
    public class HeightRule: IRule
    {
        public bool Valid(string height)
        {
            var heights = new Dictionary<string, MinMaxRule>
            {
                {"cm", new MinMaxRule() {Min = 150, Max = 193}}, {"in", new MinMaxRule() {Min = 59, Max = 76}}
            };

            if (height.Contains("cm"))
            {
                var totalHeight = height.Replace("cm", "");
                var rule = heights["cm"];
                return rule.Valid(int.Parse(totalHeight));
            }
            if (height.Contains("in"))
            {
                var totalHeight = height.Replace("in", "");
                var rule = heights["in"];
                return rule.Valid(int.Parse(totalHeight));
            }

            return false;
        }
    }
}