using System.Collections.Generic;

namespace GuestBook.WebApi.Contracts
{
    /// <summary>
    /// Represents list of an items.
    /// </summary>
    public class ItemList<TItem>
    {
        /// <summary>
        /// Requested page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Requested page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of items
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Page items
        /// </summary>
        public List<TItem> Data { get; set; }
    }
}
