using System.Text.RegularExpressions;

namespace GuestBook.WebApi.Identity
{
    public class RoleEndpointPermission
    {
        public string Endpoint { get; private set; }

        public EndpointMethod Method { get; private set; }

        public string Role { get; private set; }

        private readonly Regex _regex;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint">Regex of endpoint url</param>
        /// <param name="method">Endpoint method: GET, POST and etc.</param>
        /// <param name="role">Role of the user</param>
        public RoleEndpointPermission(string endpoint, EndpointMethod method, string role)
        {
            Endpoint = endpoint;
            Method = method;
            Role = role;
            _regex = new Regex(endpoint, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public RoleEndpointPermission(string endpoint, EndpointMethod method)
            : this(endpoint, method, null)
        {
        }

        /// <summary>
        /// Check if incoming url math to endpoint url using regex
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsMatch(string url, EndpointMethod method)
        {
            return Method.HasFlag(method) && _regex.IsMatch(url);
        }

        public bool IsMatch(string url, EndpointMethod method, string role)
        {
            return Method.HasFlag(method) && _regex.IsMatch(url) && Role == role;
        }
    }
}
