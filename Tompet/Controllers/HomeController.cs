//namespace Tompet.Controllers
//{
//    using AutoMapper;
//    using AutoMapper.QueryableExtensions;
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.Extensions.Caching.Distributed;
//    using Microsoft.Extensions.Caching.Memory;
//    using System.Diagnostics;
//    using Tompet.Core.Constants;
//    using Tompet.Core.Contracts;
//    using Tompet.Core.Services.Statistics;
//    using Tompet.Core.Services.Techniques;
//    using Tompet.Core.Services.Techniques.Models;
//    using Tompet.Infrastructure.Data;
//    using Tompet.Models;
//    using Tompet.Models.Home;

//    using static Tompet.Infrastructure.Data.DataConstants.Cache;

//    //using System.IO;

//    public class HomeController : BaseController
//    {
//        private readonly ITechniqueService techniques;
//        private readonly IStatisticsService statistics;
//        private readonly IConfigurationProvider mapper;
//        private readonly TompetDbContext data;
//        private readonly IMemoryCache cache;

//        public HomeController(
//            ITechniqueService techniques,
//            IStatisticsService statistics,
//            IMapper mapper,
//            TompetDbContext data, 
//            IMemoryCache cache)
//        {
//            this.techniques = techniques;
//            this.statistics = statistics;
//            this.mapper = mapper.ConfigurationProvider;
//            this.data = data;
//            this.cache = cache;
//        }

//        private readonly ILogger<HomeController> logger;

//        //private readonly IDistributedCache cache;

//        private readonly IFileService fileService;

//        //public HomeController(
//        //    ILogger<HomeController> _logger,
//        //    IDistributedCache _cache,
//        //    IFileService _fileService)
//        //{
//        //    logger = _logger;
//        //    cache = _cache;
//        //    fileService = _fileService;
//        //}

//        public IActionResult Index()
//        {
//            var latestTechniques = this.cache.Get<List<LatestTechniquesServiceModel>>(LatestTechniquesCacheKey);

//            if (latestTechniques == null)
//            {
//                latestTechniques = this.techniques
//                     .Latest()
//                     .ToList();

//                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

//                this.cache.Set(LatestTechniquesCacheKey, latestTechniques, cacheOptions);
//            }

//            var totalTechnique = this.data.Techniques.Count();
//            var totalUsers = this.data.Users.Count();

//            var tecniques = this.data
//                .Techniques
//                .OrderByDescending(t => t.Id)
//                .ProjectTo<TechniqueIndexViewModel>(this.mapper)
//                .Take(3)
//                .ToList();

//            var totalStatistics = this.statistics.Total();
//            // DateTime dateTime = DateTime.Now;
//            // var cachedData = await cache.GetStringAsync("cachedTime");

//            //if (cachedData == null)
//            //{
//            //    cachedData = dateTime.ToString();
//            //    DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
//            //    {
//            //        SlidingExpiration = TimeSpan.FromSeconds(20),
//            //        AbsoluteExpiration = DateTime.Now.AddSeconds(60)
//            //    };

//            //   await cache.SetStringAsync("cashedTime", cachedData, cacheOptions);
//            //}
//            ViewData[MessageConstant.SuccessMessage] = "Браво, успяхме да подкараме тостера!";


//            //return View(new IndexViewModel
//            //{
//            //    TotalTechniques = totalStatistics.TotalTechniques,
//            //    TotalUsers = totalStatistics.TotalUsers,
//            //    Techniques = latestTechniques.ToList(),
//            //});
//            return View(new IndexViewModel
//            {
//                TotalTechniques = totalStatistics.TotalTechniques,
//                TotalUsers = totalStatistics.TotalUsers,
//                Techniques = latestTechniques.ToList()
//            });
//        }

//        public IActionResult Privacy(ErrorViewModel error)
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult UploadFile()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> UploadFile(IFormFile file)
//        {
//            try
//            {
//                if (file != null && file.Length > 0)
//                {
//                    string path = Path.GetFullPath(@"./wwwroot/Image");

//                    using (var stream = new MemoryStream())
//                    {
//                        await file.CopyToAsync(stream);

//                        //var fileToSave = new ApplicationFile()
//                        //{
//                        //    FileName = file.FileName,
//                        //    Content = stream.ToArray()
//                        //};

//                        await System.IO.File.WriteAllBytesAsync(path, stream.ToArray());

//                        //await fileService.SaveFileAsync(fileToSave);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//                logger.LogError(ex, "HomeController/UploadFile");

//                TempData[MessageConstant.ErrorMessage] = "Възникна проблем по време на запис";
//            }

//            TempData[MessageConstant.SuccessMessage] = "Файла е качен успешно";

//            return RedirectToAction(nameof(Index));
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
namespace Tompet.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using System.Diagnostics;
    using Tompet.Core.Constants;
    using Tompet.Core.Contracts;
    using Tompet.Core.Services.Statistics;
    using Tompet.Core.Services.Techniques;
    using Tompet.Core.Services.Techniques.Models;
    using Tompet.Infrastructure.Data;
    using Tompet.Models;
    using Tompet.Models.Home;

    using static Tompet.Infrastructure.Data.DataConstants.Cache;

    //using System.IO;

    public class HomeController : BaseController
    {
        private readonly ITechniqueService techniques;
        private readonly IConfigurationProvider mapper;
        private readonly TompetDbContext data;
        private readonly IMemoryCache cache;

        public HomeController(
            ITechniqueService techniques,
            IMapper mapper,
            TompetDbContext data, 
            IMemoryCache cache)
        {
            this.techniques = techniques;
            this.mapper = mapper.ConfigurationProvider;
            this.data = data;
            this.cache = cache;
        }

        private readonly ILogger<HomeController> logger;

        //private readonly IDistributedCache cache;

        private readonly IFileService fileService;

        //public HomeController(
        //    ILogger<HomeController> _logger,
        //    IDistributedCache _cache,
        //    IFileService _fileService)
        //{
        //    logger = _logger;
        //    cache = _cache;
        //    fileService = _fileService;
        //}

        public async Task<IActionResult> Index()
        {
            var totalTechnique = this.data.Techniques.Count();
            var totalUsers = this.data.Users.Count();

            var techniques = this.data
                .Techniques
                .OrderByDescending(t => t.Id)
                .ProjectTo<TechniqueIndexViewModel>(this.mapper)
                .Take(3)
                .ToList();


            var latestTechniques = this.cache.Get<List<LatestTechniqueServiceModel>>(LatestTechniqueCacheKey);

            if (latestTechniques == null)
            {
                latestTechniques = this.techniques
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestTechniqueCacheKey, latestTechniques, cacheOptions);
            }
            // DateTime dateTime = DateTime.Now;
            // var cachedData = await cache.GetStringAsync("cachedTime");

            //if (cachedData == null)
            //{
            //    cachedData = dateTime.ToString();
            //    DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
            //    {
            //        SlidingExpiration = TimeSpan.FromSeconds(20),
            //        AbsoluteExpiration = DateTime.Now.AddSeconds(60)
            //    };

            //   await cache.SetStringAsync("cashedTime", cachedData, cacheOptions);
            //}
            ViewData[MessageConstant.SuccessMessage] = "Браво, успяхме да подкараме тостера!";


            return View(latestTechniques);
        }

        public IActionResult Privacy(ErrorViewModel error)
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string path = Path.GetFullPath(@"./wwwroot/Image");

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        //var fileToSave = new ApplicationFile()
                        //{
                        //    FileName = file.FileName,
                        //    Content = stream.ToArray()
                        //};

                        await System.IO.File.WriteAllBytesAsync(path, stream.ToArray());

                        //await fileService.SaveFileAsync(fileToSave);
                    }
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "HomeController/UploadFile");

                TempData[MessageConstant.ErrorMessage] = "Възникна проблем по време на запис";
            }

            TempData[MessageConstant.SuccessMessage] = "Файла е качен успешно";

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}