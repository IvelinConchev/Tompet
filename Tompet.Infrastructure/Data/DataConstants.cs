namespace Tompet.Infrastructure.Data
{
    public class DataConstants
    {
        public class Company
        {
            public const int CompanyNameMinLength = 5;
            public const int CompanyNameMaxLength = 20;
            public const int CompanyManagerMinLength = 2;
            public const int CompanyManagerMaxLength = 50;
            public const int CompanyPhoneNumberMinLength = 6;
            public const int CompanyPhoneNumberMaxLength = 15;
            public const int CompanyAddressMinLenghth = 3;
            public const int CompanyAddressMaxLenghth = 50;
        }

        public class Service
        {
            public const int ServiceNameMaxLength = 40;
            public const int ServiceNameMinLength = 9;
            public const int ServiceDescriptionMinLength = 2;
            public const int ServiceDescriptionMaxLength = 100;
        }

        public class Technique
        {
            public const int TechniqueNameMaxLength = 20;
            public const int TechniqueNameMinLength = 2;
            public const int TechniqueTypeMaxLength = 20;
            public const int TechniqueTypeMinLength = 2;
        }

        public class Manager
        {
            public const int ManagerNameMinLength = 2;
            public const int ManagerNameMaxLength = 25;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 10;

        }

        public class ApplicationUser
        {
            public const int ApplicationUserFirstNameMaxLength = 50;
            public const int ApplicationUserLastNameMaxLength = 50;
        }

        public static class UserConstant
        {
            public static class Roles
            {
                public const string Administrator = "Administrator";
                public const string Manager = "Administrator, Manager";
            }
        }
    }
}
