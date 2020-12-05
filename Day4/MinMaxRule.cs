namespace Day4
{
    public class MinMaxRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public bool Valid(int number)
        {
            return number >= Min && number <= Max;
        }
    }
}