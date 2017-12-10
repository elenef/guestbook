using System.Collections.Generic;

namespace GuestBook.Models.Contracts
{
    public class RestaurantContract : EditRestaurantContract
    {
        public string Id { get; set; }

        public List<string> ReviewIds { get; set; }
    }
}
