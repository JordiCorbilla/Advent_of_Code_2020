using System.Text.RegularExpressions;

namespace Day4
{
    public class HairRule {
        public bool Valid(string hair)
        {
            string pattern = @"#[0-9a-f]{6}";
            return Regex.Matches(hair, pattern).Count == 1;
        }
    }
}