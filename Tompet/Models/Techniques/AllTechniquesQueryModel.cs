namespace Tompet.Models.Techniques
{
    using System.ComponentModel.DataAnnotations;

    public class AllTechniquesQueryModel
    {
        public string Name { get; init; }

        [Display(Name = "Търсене")]
        public string SearchTerm { get; init; }

        public TechniqueSorting Sorting { get; init; }

        public IEnumerable<string> Names { get; set; }

       public IEnumerable<TechniqueListingViewModel> Techniques { get; set; }
    }
}
