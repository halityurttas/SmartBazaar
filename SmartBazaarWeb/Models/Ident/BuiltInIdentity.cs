namespace SmartBazaar.Web.Models.Ident
{
    public class BuiltInRoles
    {
        public const string Admins = "78bfe7bb-68fe-4c23-925b-c5586c070377";
    }

    public class BuiltInUsers
    {
        public static ApplicationUser Admin
        {
            get
            {
                return new ApplicationUser
                {
                    AccessFailedCount = 5,
                    Email = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"],
                    EmailConfirmed = true,
                    Id = "3c974a64-27f0-47b6-bf3b-2613c59aaba0",
                    LockoutEnabled = true,
                    PhoneNumber = "",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = "hrSW9JwXpp47Im4PPJmk7xtnFtHpTkv5",
                    UserName = "Admin"
                };
            }
        }
    }
}