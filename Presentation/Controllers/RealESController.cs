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
            var linq = db.RealES.Include(x => x.Room).Include(x => x.Address).ThenInclude(x => x.Country).ThenInclude(x => x.Cities).ThenInclude(x => x.hoods).Include(x => x.Images).Include(x => x.User).ToList();
            var data = new List<CardVM>();
            foreach (var item in linq)
            {

                CardVM card = new CardVM
                {
                    Country = item.Address.Country.Name,
                    City = item.Address.City.Name,
                    Hood = item.Address.Hood.Name,
                    Title = item.Name,
                    Price = item.Price,

                    N_BedRoom = item.Room.N_Bedroom,
                    TotalRooms = linq.Select(x => item.Room).Count().ToString(),
                    Area_Siza = item.Area_Size,
                    UserName = item.User.FirstName + " " + item.User.LastName,
                    UserPP = item.User.ImageName,
                    ImageName = item.Images.FirstOrDefault().ImageName




                };
                data.Add(card);
            }

            return View(data);
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
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/RealESImages", Guid.NewGuid().ToString() + item.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    item.CopyTo(stream);
                };
                images.Add(
                   new RealESImages
                   {
                       ID = Guid.NewGuid().ToString(),
                       ImageName = item.FileName,
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

    }




}
