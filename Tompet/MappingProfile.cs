namespace Tompet.Infrastructure
{
    using AutoMapper;
    using Tompet.Core.Services.Techniques.Models;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models.Home;
    using Tompet.Models.Techniques;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Technique, TechniqueIndexViewModel>();
            this.CreateMap<TechniqueDetailsServiceModel, TechniqueFormModel>();

            this.CreateMap<Technique, TechniqueDetailsServiceModel>()
                .ForMember(t => t.UserId, cfg => cfg.MapFrom(t => t.Manager.UserId));
        }
    }
}
