using Microsoft.AspNetCore.Mvc;
using Presentation.Data;
using Presentation.Models;
using Presentation.ViewModels.RealESVM;
using System.Drawing;

namespace Presentation.Controllers
{
    public class RealESController : Controller
    {

        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly UserManager<IdentityUser> _userManager;
        private readonly MyDbContext db;

        public RealESController(MyDbContext myDbContext, IWebHostEnvironment web, UserManager<IdentityUser> user)
        {
            _webHostEnvironment = web;
            _userManager = user;
            this.db = myDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var data = db.RealES
                .Include(x => x.Address)
                .ThenInclude(x => x.Country)
                .ThenInclude(x => x.Cities)
                .ThenInclude(x => x.hoods)
                .Include(x => x.Images)
                .Include(x => x.Room)
                .Include(x => x.User)
                .ToList();
            List<CardVM> cardVM = new List<CardVM>();
            foreach (var card in data)
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
                    RealId = card.ID

                });
            }
            return View(cardVM);
        }

        public async Task<IActionResult> Create()
        {


            var types = db.Categories.ToList();
            var data = new CreateVM
            {

                CategoryListItems = types.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.ID }).ToList(),
                CountriesListItems = db.Countries.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.ID }).ToList(),
                Features = db.Features.Select(x => new SelectionFeatures { Name = x.Name, Id = x.ID }).ToList(),
            };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM prop)
        {
            var RealESid = Guid.NewGuid().ToString();
            var AddressId = Guid.NewGuid().ToString();
            var RoomId = Guid.NewGuid().ToString();
            var ImageId = Guid.NewGuid().ToString();

            Address address = new Address
            {
                HoodID = prop.HoodId,
                CityID = prop.CityId,
                CountryID = prop.CountryId,
                RealESID = RealESid,
                AddressID = AddressId
            };

            Room room = new Room
            {
                N_Bathroom = prop.N_Bathroom,
                N_Bedroom = prop.N_Bedroom,
                N_Garage = prop.Carage,
                N_Floors = "1",
                N_Kitchen = "1",
                N_LivingRoom = "1",
                RealESId = RealESid,
                RoomID = RoomId,
                N_Rooms = prop.NRooms

            };
            List<RealESImages> images = new List<RealESImages>
           ();
            foreach (var item in prop.ImageFiles)
            {
                var guid = Guid.NewGuid().ToString();

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/RealESImages", guid + item.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    item.CopyTo(stream);
                };
                images.Add(
                   new RealESImages
                   {
                       ID = Guid.NewGuid().ToString(),
                       ImageName = guid + item.FileName,
                       ImagePath = path,
                       RealESId = RealESid
                   }
                );
            }
            var features = prop.Features
                   .Where(x => x.isSelected == true)
                   .Select(x => x.Id)
                   .ToList();

            List<RealESFeature> r = new List<RealESFeature>();
            foreach (var fg in features)
            {
                r.Add(new RealESFeature { FeatureID = fg, RealESID = RealESid });

            }


            RealES realES = new RealES
            {
                ID = RealESid,
                Name = prop.Name,
                Price = prop.Price,
                Description = prop.Description,
                AddressID = AddressId,
                RoomID = RoomId,
                Area_Size = prop.Area_Size,
                Email = prop.Email,
                PhoneNumber = prop.PhoneNumber,
                CategoryID = prop.CategoryId,


                RealESFeatures = r,

                Images = images,
                UserID = _userManager.GetUserId(User)

            };




            await db.RealES.AddAsync(realES);
            await db.RealESFeatures.AddRangeAsync(r);
            await db.Addresses.AddAsync(address);
            await db.Rooms.AddAsync(room);
            await db.RealESImages.AddRangeAsync(images);

            await db.SaveChangesAsync();

            var types = db.Categories.ToList();
            var data = new CreateVM
            {

                CategoryListItems = types.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.ID }).ToList(),
                CountriesListItems = db.Countries.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.ID }).ToList(),
                Features = db.Features.Select(x => new SelectionFeatures { Name = x.Name, Id = x.ID }).ToList(),
            };
            return View(data);
        }

        public IActionResult GetCities(string id)
        {
            var data = db.Cities.Where(x => x.CountryId == id).ToList();
            return Ok(data);
        }
        public IActionResult GetHoods(string id)
        {
            var data = db.Hoods.Where(x => x.CityId == id).ToList();
            return Ok(data);
        }
        
        public async Task<IActionResult> Details(string id)
        {
            var RealId = db.RealES .Include(x=>x.Images).Include(x=>x.RealESFeatures).ThenInclude(x=>x.Feature).FirstOrDefault(x=>x.ID==id);
            
            var data = new DetailsVM();
            if (RealId != null)
            {
                var address = db.Addresses.Include(x=>x.Country).Include(x=>x.City).Include(x=>x.Hood).FirstOrDefault(x => x.AddressID == RealId.AddressID);
                var rooms = db.Rooms.FirstOrDefault(x => x.RoomID == RealId.RoomID);
                var user = (User)db.Users.FirstOrDefault(x => x.Id == RealId.UserID);
                data = new DetailsVM
                {
                    ImageName =RealId.Images.Select(x=>x.ImageName).ToList() ,
                    Title = RealId.Name,
                    Price = RealId.Price,
                    Country = address.Country.Name,
                    City = address.City.Name,
                    Hood = address.Hood.Name,
                    N_BedRoom = rooms.N_Bedroom,
                    TotalRooms = rooms.N_Rooms,
                    N_Bathrooms = rooms.N_Bathroom,
                    Area_Siza = RealId.Area_Size,
                    UserName = user.FirstName + " "+ user.LastName,
                    Date=  RealId.CreatedAt.Year.ToString(),
                    Description =   RealId.Description,
                    Email = user.UserName,
                    Garage= rooms.N_Garage,
                    UserPP = user.ImageName,
                    PhoneNumber = user.PhoneNumber,
                    Features = RealId.RealESFeatures.Select(x=>x.Feature.Name).ToList()
                };
            }
            
            return View(data);
        }

    } 
}
