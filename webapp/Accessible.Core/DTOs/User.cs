using System;
using System.Collections.Generic;

namespace Accessible.Core.DTOs
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OAuthToken { get; set; }
        public UserType UserType { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HomeLocation { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Feature> Requirements { get; } = new List<Feature>();
    }

    public enum UserType
    {
        Guest,
        Registered,
        Owner,
        Moderator,
        Admin
    }
}
