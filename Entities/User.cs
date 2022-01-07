using System;

namespace Entities
{
    public class User
    {
        public int userId { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public DateTime createdOn { get; set; }
        public bool isActive { get; set; }
        public string picURL { get; set; }
        public bool isAdmin { get; set; }
    }
}
