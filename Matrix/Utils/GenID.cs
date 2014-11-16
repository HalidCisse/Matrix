using System;

namespace Matrix.Utils
{
    class GenID
    {
        public static string GetID (int lengthOfTheID)
        {
            var x = new Random();
            var IdOut = string.Empty;

            for (var i = 0; i < lengthOfTheID; i++)
            {                
                IdOut = IdOut + x.Next(1,9);
            }
            return IdOut;
        }

    }

}
