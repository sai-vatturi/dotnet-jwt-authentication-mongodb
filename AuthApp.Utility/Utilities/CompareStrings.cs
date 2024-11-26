namespace AuthApp.Utility.Utilities
{
    public class CompareStrings
    {

        public CompareStrings()
        {

        }
        public (bool, string, string) IsStringEqual(string a, string b)
        {
            string A = a.ToUpper().Trim();
            string B = b.ToUpper().Trim();

            if(A.Contains(B) || B.Contains(A))
            {
                return (true,A,B);
            }
            return (false,A,B);
        }
    }
}
