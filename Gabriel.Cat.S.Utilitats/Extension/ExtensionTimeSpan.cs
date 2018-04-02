using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionTimeSpan
    {
        public static string ToHoursMinutesSeconds(this TimeSpan time,bool addMiliseconds=false)
        {
            StringBuilder str = new StringBuilder();
            str.Append(time.Hours);
            str.Append(":");
            str.Append(time.Minutes);
            str.Append(":");
            str.Append(time.Seconds);
            if(addMiliseconds)
            {
                str.Append(".");
                str.Append(time.Milliseconds);
            }
            return str.ToString();
        }
    }
}
