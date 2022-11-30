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
            public const int ServiceDescriptionMaxLength = 100;
        }

        public class Technique
        {
            public const int TechniqueNameMaxLength = 20;
            public const int TechniqueTypeMaxLength = 20;
        }
    }
}
