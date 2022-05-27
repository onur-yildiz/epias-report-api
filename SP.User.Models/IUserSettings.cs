﻿namespace SP.User.Models
{
    public interface IUserSettings
    {
        bool IsActive { get; set; }
        string LanguageCode { get; set; }
        HashSet<string> Roles { get; set; }
    }
}