using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.ChuckNorris;
using Shop.Core.Dto.OpenWeatherDtos;
using Shop.Core.ServiceInterface;
using Shop.Models.ChuckNorris;
using Shop.Models.OpenWeathers;

namespace Shop.Controllers
{
    public class ChuckNorrisController : Controller
    {


        private readonly IChuckNorrisServices _chuckNorrisServices;

        public ChuckNorrisController(IChuckNorrisServices chuckNorrisServices)
        {
            _chuckNorrisServices = chuckNorrisServices;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Chuck()
        {

            return RedirectToAction(nameof(Search));
        }

        [HttpGet]

        public IActionResult Search()
        {
            ChuckNorrisResultDto dto = new ChuckNorrisResultDto();

            _chuckNorrisServices.ChuckNorrisResult(dto);
            ChuckNorrisViewModel vm = new();

            vm.Categories = dto.Categories;
            vm.UpdatedAt = dto.UpdatedAt;
            vm.CreatedAt = dto.CreatedAt;
            vm.IconUrl = dto.IconUrl;
            vm.Id = dto.Id;
            vm.Url = dto.Url;
            vm.Value = dto.Value;



            return View(vm);

        }

    }
}
