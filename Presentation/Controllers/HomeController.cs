using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Models;
using Presentation.ViewModels.RealESVM;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _db;

        public HomeController(ILogger<HomeController> logger , MyDbContext db)
        {
            _logger = logger;
            this._db = db;
        }

        public IActionResult Index()
        {
            var Real  = _db.RealES.Take(5).Include(x => x.Address)
              .ThenInclude(x => x.Country)
              .ThenInclude(x => x.Cities)
              .ThenInclude(x => x.hoods)
              .Include(x => x.Images)
              .Include(x => x.Room)
              .Include(x => x.User)
              .Include(x => x.RealESFeatures)
              .ThenInclude(x => x.Feature)
              .Include(x => x.Category)
              .ToList();
            List<CardVM> cardVM = new List<CardVM>();
            foreach (var card in Real)
            {
                cardVM.Add(new CardVM
                {
                    ImageName = card.Images.Select(x => x.ImageName).ToList(),
                    UserName = card.User.FirstName + " " + card.User.LastName,
                    Title = card.Name,
                    Country = card.Address.Country.Name,
                    City = card.Address.City.Name,
                    Hood = card.Address.Hood.Name,
                    Area_Siza = card.Area_Size,
                    UserPP = card.User.ImageName,
                    TotalRooms = card.Room.N_Bathroom,
                    N_BedRoom = card.Room.N_Bedroom,
                    Price = card.Price,
                    Date = card.CreatedAt.ToShortDateString(),
                    UserID = card.UserID,
                    RealId = card.ID,
                    Categories = _db.Categories.Select(x => new SelectionFeatures { Id = x.ID, Name = x.Name, isSelected = false }).ToList(),
                    Features = _db.Features.Select(x => new SelectionFeatures { Id = x.ID, Name = x.Name, isSelected = false }).ToList(),

                });

            }
            var d = _db.RealES.Include(x => x.Category).ToList();
            var groupedData = d.GroupBy(x => x.CategoryID);

            var data = new HomeVM
            {
                Cards = cardVM,
                CountsCategory = groupedData.Select(g => new CountCategoty
                {
                    CategoryName = g.First().Category.Name,
                    CategotyId = g.Key,
                    Count = g.Count().ToString()
                }).ToList(),
            };

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

    }
}
