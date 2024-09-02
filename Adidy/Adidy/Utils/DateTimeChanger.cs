namespace Adidy.Utils
{
    public class DateTimeChanger
    {
        public static DateTime StringToDateTime(string value)
        {
            string[] separation = value.Split('/');

            DateTime retour = new(Int32.Parse(separation[2]), Int32.Parse(separation[1]), Int32.Parse(separation[0]));

            return retour;
        }
    }
}
