namespace Day4
{
    public class EyeRule : IRule
    {
        public bool Valid(string text)
        {
            string[] colours = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
            foreach (var colour in colours)
                if (text == colour)
                    return true;
            return false;
        }
    }
}