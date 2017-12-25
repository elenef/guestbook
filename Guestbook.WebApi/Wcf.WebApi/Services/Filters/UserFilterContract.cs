using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services.Filters
{
    public class UserFilterContract : BaseFilterContract
    {
        /// <summary>
        /// поиск пользователей по имени или логину
        /// </summary>
        public string Search { get; set; }
    }
}
