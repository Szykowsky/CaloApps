﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Feature.UserSettings.Helpers
{
    public static class ErrorMessages
    {
        public const string UserIdNotNull = "You have to add user id";
        public static string PrepareMessage(string argument, string value)
        {
            return string.Format("{0} must be grather than {1}", argument, value);
        }
    }
}