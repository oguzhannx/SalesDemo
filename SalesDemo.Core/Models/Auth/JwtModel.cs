﻿namespace SalesDemo.Core.Models.Auth
{
    public class JwtModel
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
