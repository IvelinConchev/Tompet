namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using System.Diagnostics;
    using Tompet.Core.Constants;
    using Tompet.Core.Contracts;
    using Tompet.Infrastructure.Data;
    using Tompet.Models;
    //using System.IO;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;

        private readonly IDistributedCache cache;

        private readonly IFileService fileService;

        public HomeController(
            ILogger<HomeController> _logger,
            IDistributedCache _cache,
            IFileService _fileService)
        {
            logger = _logger;
            cache = _cache;
            fileService = _fileService;
        }

        public async Task<IActionResult> Index()
        {
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


            return View();
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