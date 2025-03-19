using System.Text.RegularExpressions;

namespace LibraryManagement.Utility
{
    public static class StringHelper
    {
        public static string CleanInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            input = input.Trim();

            input = Regex.Replace(input, @"\s+", " ");

            return input;
        }
    }
}
