using System.Web.Mvc;
using Gift.Data.Entities;
using Gift.Framework.Repository;

namespace Gift.Web.Controllers {
    [AllowAnonymous]
    public class HomeController : Controller {
        private readonly IRepository<City> _cityRepository;

        public HomeController(IRepository<City> cityRepository) {
            _cityRepository = cityRepository;
        }

        // GET: Home
        public ActionResult Index() {
            //var cityEntity = new City();
            //cityEntity.Name = "ASDdasd4";
            //_cityRepository.Create(cityEntity);
            //_cityRepository.SaveChanges();
            return View();
        }
    }
}