using System;

namespace GuestBook.Data
{
    public static class IdentityGenerator
    {
        public static string NewId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
