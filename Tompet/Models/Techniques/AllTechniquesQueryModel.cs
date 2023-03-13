namespace Tompet.Models.Techniques
{
    using System.ComponentModel.DataAnnotations;

    public class AllTechniquesQueryModel
    {
        public const int TechniquesPerPage = 3;

        public string Name { get; init; }

        [Display(Name = "Търсене")]
        public string SearchTerm { get; init; }

        public TechniqueSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;


        public int TotalTechnique { get; set; }

        public IEnumerable<string> Names { get; set; }

       public IEnumerable<TechniqueListingViewModel> Techniques { get; set; }
    }
}
