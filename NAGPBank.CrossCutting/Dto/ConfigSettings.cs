using System;
using System.Collections.Generic;
using System.Text;

namespace NAGPBank.CrossCutting.Dto
{
    public class ConfigSettings
    {
        public string RedisCacheConnStr { get; set; }
        public string DbName { get; set; }
    }
}
