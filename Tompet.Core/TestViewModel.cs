namespace Tompet.Core
{
    using System;
    using Tompet.Core.CustomAttributes;

    public class TestViewModel
    {
        public DateTime FirstDate { get; set; }

        [IsBefore(nameof(FirstDate), errorMessage: "Boom")]
        public DateTime SecondDate { get; set; }
    }
}
