using System;

namespace GuestBook
{
    public class IdentityGenerator
    {
        public static string NewId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
