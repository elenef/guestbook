﻿namespace GuestBook.WebApi.Contracts
{
    public class EditReviewContract
    {
        public string Comment { get; set; }

        public string RestaurantId { get; set; }

        public string UserName { get; set; }

        public int ReviewRating { get; set; }

        public int Like { get; set; }
    }
}
