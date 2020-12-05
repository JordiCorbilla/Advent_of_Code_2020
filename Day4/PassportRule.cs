namespace Day4
{
    public class PassportRule: IRule
    {
        public bool Valid(string value)
        {
            if (int.TryParse(value, out var id))
            {
                return value.Length == 9;
            }

            return false;
        }
    }
}