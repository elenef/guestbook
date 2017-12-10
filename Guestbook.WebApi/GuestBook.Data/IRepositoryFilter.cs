using System.Linq;

namespace GuestBook.Data
{
    public interface IRepositoryFilter
    {
        /// <summary>
        /// Filter all items by some criteria
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryable Apply(IQueryable query);

        /// <summary>
        /// Check single item and returns true if item meets with some critera
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        CheckResult CheckItem(object item);
    }

    public class CheckResult
    {
        public bool IsSuccess { get; }

        public string ErrorMessage { get; }

        public static CheckResult Success => new CheckResult();

        public CheckResult()
        {
            IsSuccess = true;
        }

        public CheckResult(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }
    }
}
