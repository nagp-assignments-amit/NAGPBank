using System;
using System.Collections.Generic;
using System.Text;

namespace NAGPBank.CrossCutting.Types
{
    public static class CacheKey
    {
        public static string Customer = "Customer";

        public static string Account = "Account";

        public static string Transactions = "Transactions";
    }

    public static class ConfigKey
    {
        public static string DbName = "DbName";
    }
}
