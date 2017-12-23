using System;
using System.Collections.Generic;
using System.Linq;

namespace GuestBook.Domain.Helpers
{
    public static class ListExtensions
    {
        private const char Separator = ',';

        public static string SerializeToString(this List<string> items)
        {
            return string.Join(Separator.ToString(), items);
        }

        public static List<string> DeserializeToList(this string item)
        {
            if (item == null)
            {
                return new List<string>();
            }
            return item
                .Split(new[] {Separator}, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }
    }
}
