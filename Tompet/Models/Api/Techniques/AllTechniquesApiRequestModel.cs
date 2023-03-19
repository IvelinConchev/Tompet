namespace Tompet.Models.Api.Techniques
{
    public class AllTechniquesApiRequestModel
    {
        public string Name { get; init; }

        public string SearchTerm { get; init; }

        public TechniqueSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TechniquesPerPage { get; init; } = 10;
    }
}
