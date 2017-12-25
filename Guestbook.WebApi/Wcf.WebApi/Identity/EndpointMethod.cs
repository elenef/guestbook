using System;

namespace GuestBook.WebApi.Identity
{
    [Flags]
    public enum EndpointMethod
    {
        Post = 1,
        Put = 2,
        Get = 4,
        Delete = 8,
        All = Post | Put | Get | Delete
    }
}
