using System;

namespace RandomUser.Application.Extensions
{
    public static class ByteExtensions
    {
        public static string ToBase64String(this byte[] byteArray)
        {
            if (byteArray == null)
            {
                return null;
            }
            else
            {
                var base64string = null as string;

                try
                {
                    base64string = Convert.ToBase64String(byteArray);
                }
                catch (Exception ex)
                {
                    //Log error
                }

                return base64string;
            }
        }
    }
}
