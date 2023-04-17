using System;

namespace CookbookCommon
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
