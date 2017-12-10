using System;

namespace GuestBook.Data
{
    public class IdentityGenerator
    {
        public static string NewId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
