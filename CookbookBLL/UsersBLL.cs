using CookbookCommon;
using CookbookData;

namespace CookbookBLL
{
    public static class UsersBLL
    {
        public static int CreateUser(int roleID, string email, string firstName, string lastName, string password)
        {
            string hashedPassword, salt;
            Hashing.GenerateSaltedHash(password, out hashedPassword, out salt);
            return UsersData.CreateUser(roleID, email, firstName, lastName, hashedPassword, salt);
        }

        public static void DeleteUser(int userID)
        {
            UsersData.DeleteUser(userID);
        }

        public static List<User> GetUsers()
        {
            return UsersData.GetAllUsers();
        }

        public static User GetUserByEmail(string email)
        {
            return UsersData.GetUserByEmail(email);
        }

        public static void UpdateUserPassword(string email, string newPassword)
        {
            string newSalt, newHash;
            Hashing.GenerateSaltedHash(newPassword, out newHash, out newSalt);
            UsersData.UpdateUserPassword(email, newHash, newSalt);
        }

        // Used by the register method to check for duplicate emails
        // Returns true if the email is valid
        public static bool VerifyEmail(string email)
        {
            User u = UsersData.GetUserByEmail(email);
            if (u == null)
            {
                return true;
            }
            else if (u.UserID == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method used for checking entered password when logging in
        // Returns true if the password is valid
        public static bool VerifyPassword(string email, string enteredPassword, out int userID, out int roleID)
        {
            userID = 0;
            roleID = 1;
            User u = UsersData.GetUserByEmail(email);
            if (u == null)
            {
                return false;
            }
            else if (u.UserID == 0)
            {
                return false;
            }
            else if (Hashing.VerifyPassword(enteredPassword, u.HashedPassword, u.Salt))
            {
                userID = u.UserID;
                roleID = u.RoleID;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void EditUser(int userID, int roleID, string email, string firstName, string lastName)
        {
            UsersData.EditUser(userID, roleID, email, firstName, lastName);
        }
    }
}