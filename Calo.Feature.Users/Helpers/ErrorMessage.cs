using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.Users.Helpers
{
    public static class ErrorMessage
    {
        public const string MinLengthLogin = "Login: Min length = 2";
        public const string MinLengthPassword = "Passwords: Min length = 6";
        public const string NotNullLogin = "You have to add login";
        public const string NotNullPassword = "You have to add Password";
        public const string PasswordsMatch = "Passwords not match";
    }
}
