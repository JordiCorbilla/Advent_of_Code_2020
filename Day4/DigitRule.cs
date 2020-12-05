namespace Day4
{
    public class DigitRule: MinMaxRule
    {
        public int NumDigits { get; set; }

        public bool Valid(string number)
        {
            if (int.TryParse(number, out var parsed))
            {
                return number.Length == NumDigits && base.Valid(parsed);
            }

            return false;
        }
    }
}