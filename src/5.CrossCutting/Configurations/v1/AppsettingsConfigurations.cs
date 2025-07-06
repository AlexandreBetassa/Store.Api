﻿using Store.CrossCutting.Configurations.v1.Models;

namespace Store.CrossCutting.Configurations.v1
{
    public class AppsettingsConfigurations
    {
        public JwtConfiguration JwtConfiguration { get; set; }

        public RedisConfiguration RedisConfiguration { get; set; }

        public EmailConfiguration EmailConfiguration { get; set; }

        public string Database { get; set; }
    }
}