using System;

namespace Matrix.Utils
{
    class GenId
    {
        public static string GetId (int lengthOfTheId)
        {
            var x = new Random();
            var idOut = string.Empty;

            for (var i = 0; i < lengthOfTheId; i++)
            {                
                idOut = idOut + x.Next(1,9);
            }
            return idOut;
        }

    }

}
