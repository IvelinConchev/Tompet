namespace Tompet.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Core.Services.Techniques;
    using Tompet.Infrastructure.Data;
    using Tompet.Models;
    using Tompet.Models.Api.Techniques;

    [ApiController]
    [Route("api/techniques")]
    public class TechniquesApiController : ControllerBase
    {
        private readonly ITechniqueService techniques;

        public TechniquesApiController(ITechniqueService techniques)
            => this.techniques = techniques;

        [HttpGet]
        public TechniquesQueryServiceModel All([FromQuery] AllTechniquesApiRequestModel query) 
            => this.techniques.All(
                query.Name,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.TechniquesPerPage);
    }
}
