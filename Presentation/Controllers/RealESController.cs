using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Presentation.Data;
using Presentation.Models;
using Presentation.ViewModels.RealESVM;
using System.Drawing;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;
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
        [HttpGet]
        public IActionResult GetAllCountry()
        {
            var data = db.Countries.ToList();
            return Ok(data);
        }
        public async Task<IActionResult> Index(FilterVM fill)
        {
           
          var  data = db.RealES
              .Include(x => x.Address)
              .ThenInclude(x => x.Country)
              .ThenInclude(x => x.Cities)
              .ThenInclude(x => x.hoods)
              .Include(x => x.Images)
              .Include(x => x.Room)
              .Include(x => x.User)
              .Include(x => x.RealESFeatures)
              .ThenInclude(x => x.Feature)
              .Include(x => x.Category)
              .AsQueryable();
            if(!string.IsNullOrEmpty(fill.Estate))
            {
               data =  data.Where(x => x.Name.Contains(fill.Estate));

            } 
            if(fill.Country != null)
            {
               data =  data.Where(x => x.Address.Country.Name.Contains(fill.Country));

            }
            if (fill.CategoryId != null)
            {
                data = data.Where(x=>x.CategoryID==fill.CategoryId);
            }
            if (fill.Feature != null)
            {
                var selected = fill.Feature.Where(x => x.isSelected == true).Select(x => x.Id).ToList();
                if (selected.Count != 0)
                {
                    data = data.Where(x => x.RealESFeatures.Any(x => selected.Contains(x.FeatureID)));
                }
            }
            if (fill.Categories != null)
            {
                var selected = fill.Categories.Where(x => x.isSelected == true).Select(x => x.Id).ToList();
                if (selected.Count != 0)
                {

                    data = data.Where(x => selected.Contains(x.Category.ID));
                }
            }
            if (fill.MinPrice > 0 && fill.MaxPrice < 10e5)
            {
                data = data.Where(x => x.Price <= fill.MaxPrice && x.Price >= fill.MinPrice);
            }
            if (fill.CountryFilter != null)
            {
                data = data.Where(x => x.Address.Country.ID == fill.CountryFilter);

                if (fill.CityFilter != null)
                {
                    data = data.Where(x => x.Address.City.ID == fill.CityFilter);
                    if (fill.HoodFilter != null)
                    {
                        data = data.Where(x => x.Address.Hood.ID == fill.HoodFilter);
                    }
                }
            }


            List<CardVM> cardVM = new List<CardVM>();
            foreach (var card in data.ToList())
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
                    Categories = db.Categories.Select(x => new SelectionFeatures { Id = x.ID, Name = x.Name, isSelected = false }).ToList(),
                    Features = db.Features.Select(x => new SelectionFeatures { Id = x.ID, Name = x.Name, isSelected = false }).ToList(),

                });

            }
            if (data.ToList().Count == 0)
            {
                cardVM.Add(new CardVM
                {
                    Categories = db.Categories.Select(x => new SelectionFeatures { Id = x.ID, Name = x.Name, isSelected = false }).ToList(),
                    Features = db.Features.Select(x => new SelectionFeatures { Id = x.ID, Name = x.Name, isSelected = false }).ToList()
                });
            }




            return View(cardVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {


            var types = db.Categories.ToList();
            var data = new CreateVM();



            data.CategoryListItems = types.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.ID }).ToList();
            data.CountriesListItems = db.Countries.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = x.Name, Value = x.ID }).ToList();
            data.Features = db.Features.Select(x => new SelectionFeatures { Name = x.Name, Id = x.ID }).ToList();
            data.flag = false;
            if (id == null)
            {
                return View(data);

            }
            else
            {
                var realES = await
                    db.RealES
             .Include(x => x.Address)
             .ThenInclude(x => x.Country)
             .ThenInclude(x => x.Cities)
             .ThenInclude(x => x.hoods)
             .Include(x => x.Images)
             .Include(x => x.Room)
             .Include(x => x.User)
             .Include(x => x.RealESFeatures.Where(x => x.RealESID == id))
             .ThenInclude(x => x.Feature)
             .Include(x => x.Category)
             .FirstOrDefaultAsync(x => x.ID == id);


                List<SelectionFeatures> r = new List<SelectionFeatures>();
                foreach (var fg in data.Features)
                {
                    bool f = true;
                    foreach (var i in realES.RealESFeatures)
                    {
                        if (fg.Id == i.FeatureID)
                        {
                            r.Add(new SelectionFeatures { Id = fg.Id, Name = fg.Name, isSelected = true });
                            f = false;
                            break;
                        }
                    }

                    if (f)
                    { r.Add(new SelectionFeatures { Id = fg.Id, Name = fg.Name, isSelected = false }); }

                }
                data.IDRealES = realES.ID;
                data.Name = realES.Name;
                data.Price = realES.Price;
                data.Description = realES.Description;
                data.IDAddress = realES.AddressID;
                data.IDRoom = realES.RoomID;
                data.Area_Size = realES.Area_Size;
                data.Email = realES.Email;
                data.PhoneNumber = realES.PhoneNumber;
                data.CategoryId = realES.CategoryID;
                data.Features = r;
                data.CountryId = realES.Address.CountryID;
                data.CityId = realES.Address.CityID;
                data.HoodId = realES.Address.HoodID;
                data.NRooms = realES.Room.N_Rooms;
                data.N_Bedroom = realES.Room.N_Bedroom;
                data.N_Bathroom = realES.Room.N_Bathroom;
                data.Carage = realES.Room.N_Garage;
                data.UserID = _userManager.GetUserId(User);

                data.flag = true;
                return View(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM prop)
        {
            if (!prop.flag)
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
                    Name = prop.Name.Trim(),
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
            }
            else
            {
                var realES = await
                    db.RealES
             .Include(x => x.Address)
             .ThenInclude(x => x.Country)
             .ThenInclude(x => x.Cities)
             .ThenInclude(x => x.hoods)
             .Include(x => x.Images)
             .Include(x => x.Room)
             .Include(x => x.User)
             .Include(x => x.RealESFeatures)
             .ThenInclude(x => x.Feature)
             .Include(x => x.Category)
             .FirstOrDefaultAsync(x => x.ID == prop.IDRealES);


                realES.Name = prop.Name.Trim();
                realES.Price = prop.Price;
                realES.Description = prop.Description;
                realES.AddressID = prop.IDAddress;
                realES.RoomID = prop.IDRoom;
                realES.Area_Size = prop.Area_Size;
                realES.Email = prop.Email;
                realES.PhoneNumber = prop.PhoneNumber;
                realES.CategoryID = prop.CategoryId;

                var features = prop.Features
                .Where(x => x.isSelected == true)
                .Select(x => x.Id)
                .ToList();
                List<RealESFeature> r = new List<RealESFeature>();
                foreach (var fg in features)
                {
                    r.Add(new RealESFeature { FeatureID = fg, RealESID = prop.IDRealES });

                }
                realES.RealESFeatures = r;




                realES.Room.N_Bathroom = prop.N_Bathroom;
                realES.Room.N_Bedroom = prop.N_Bedroom;
                realES.Room.N_Garage = prop.Carage;
                realES.Room.N_Floors = "1";
                realES.Room.N_Kitchen = "1";
                realES.Room.N_LivingRoom = "1";
                realES.Room.RealESId = prop.IDRealES;
                realES.Room.RoomID = prop.IDRoom;
                realES.Room.N_Rooms = prop.NRooms;

                realES.Address.HoodID = prop.HoodId;
                realES.Address.CityID = prop.CityId;
                realES.Address.CountryID = prop.CountryId;
                realES.Address.RealESID = prop.IDRealES;
                realES.Address.AddressID = prop.IDAddress;

                db.RealES.Update(realES);

            }















            await db.SaveChangesAsync();

            var types = db.Categories.ToList();
            var data = new CreateVM
            {

                CategoryListItems = types.Select(x => new SelectListItem { Text = x.Name, Value = x.ID }).ToList(),
                CountriesListItems = db.Countries.Select(x => new SelectListItem { Text = x.Name, Value = x.ID }).ToList(),
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
            var RealId = db.RealES.Include(x => x.Comments).ThenInclude(x => x.User).Include(x => x.Images).Include(x => x.RealESFeatures).ThenInclude(x => x.Feature).FirstOrDefault(x => x.ID == id);
            RealId.Views++;
            db.Update(RealId);
            db.SaveChanges();
            var data = new DetailsVM();
            if (RealId != null)
            {
                var address = db.Addresses.Include(x => x.Country).Include(x => x.City).Include(x => x.Hood).FirstOrDefault(x => x.AddressID == RealId.AddressID);
                var rooms = db.Rooms.FirstOrDefault(x => x.RoomID == RealId.RoomID);
                var user = (User)db.Users.FirstOrDefault(x => x.Id == RealId.UserID);

                data = new DetailsVM
                {
                    ImageName = RealId.Images.Select(x => x.ImageName).ToList(),
                    Title = RealId.Name,
                    Price = RealId.Price,
                    Country = address.Country.Name,
                    City = address.City.Name,
                    Hood = address.Hood.Name,
                    N_BedRoom = rooms.N_Bedroom,
                    TotalRooms = rooms.N_Rooms,
                    N_Bathrooms = rooms.N_Bathroom,
                    Area_Siza = RealId.Area_Size,
                    UserName = user.FirstName + " " + user.LastName,
                    Date = RealId.CreatedAt.Year.ToString(),
                    Description = RealId.Description,
                    Email = user.UserName,
                    Garage = rooms.N_Garage,
                    UserPP = user.ImageName,
                    PhoneNumber = user.PhoneNumber,
                    Features = RealId.RealESFeatures.Select(x => x.Feature.Name).ToList(),
                    userID = _userManager.GetUserId(User).ToString(),
                    RealID = id,
                    Commentslist = RealId.Comments.Select(x => new Comments { CreatedAT = x.CreatedAT, Description = x.Description, User = x.User, UserID = x.UserID, RealESID = x.RealESID }).OrderByDescending(x => x.CreatedAT).ToList(),
                Views = RealId.Views,
                
                };
            }

            return View(data);
        }

        public IActionResult PostComment(DetailsVM comm)
        {
            Comments commnt = new Comments
            {
                Id = Guid.NewGuid().ToString(),
                Description = comm.Comment,
                RealESID = comm.RealID,
                UserID = comm.userID,
                CreatedAT = DateTime.Now

            };
            db.Comments.Add(commnt);
            db.SaveChanges();
            return RedirectToAction("Details", "RealES", new { id = comm.RealID });
        }

        public IActionResult NotFound()
        {

            return View();
        }

        public async Task<IActionResult> Favorites()
        {
            var userID = _userManager.GetUserId(User);

            var favs = db.Favorites
                 .Where(x => x.UserID == userID)
                 .Select(x => x.RealESID)
                  .ToList();

            var realEs = db.RealES
                .Include(x => x.Favorites)
                .Include(x => x.Images)
                .Include(x => x.Address)
                .ThenInclude(x => x.Country)
                .ThenInclude(x => x.Cities)
                .ThenInclude(x => x.hoods)
                 .Where(x => favs.Contains(x.ID))
                .ToList();




            var data = new List<FavVM>();

            foreach (var fav in realEs)
            {
                data.Add
                (
                    new FavVM
                    {
                        RealId = fav.ID,
                        ImageName = fav.Images.FirstOrDefault().ImageName,
                        Title = fav.Name,
                        Price = fav.Price,
                        Country = fav.Address.Country.Name,
                        City = fav.Address.City.Name,
                        Hood = fav.Address.Hood.Name

                    }
                );
            }

            return View(data);

        }

        public IActionResult MakeitFavorite(string id)
        {
            var userID = _userManager.GetUserId(User);
            var check = db.Favorites.Where(x => x.UserID == userID && x.RealESID == id).ToList();
            if (check.Count > 0)
            {
                return RedirectToAction("RemoveitFavorite", new { id = id });
            }
            var fav = new Favorite
            {
                RealESID = id,
                UserID = userID
            }
            ;
            db.Favorites.Add(fav);
            db.SaveChanges();

            return Ok(1);

        }

        public IActionResult RemoveitFavorite(string id)
        {

            var userID = _userManager.GetUserId(User);
            var fav = new Favorite
            {
                RealESID = id,
                UserID = userID
            }
            ;
            db.Favorites.Remove(fav);
            db.SaveChanges();
            return Ok();

        }

        public IActionResult RemoveitFavoriteDashboard(string id)
        {

            var userID = _userManager.GetUserId(User);
            var fav = new Favorite
            {
                RealESID = id,
                UserID = userID
            }
            ;
            db.Favorites.Remove(fav);
            db.SaveChanges();
            return RedirectToAction("Favorites");

        }

        public IActionResult MyProperties()
        {
            var userID = _userManager.GetUserId(User);
            var data = db.RealES.Where(x => x.UserID == userID)
                .Include(x => x.Images)
                .Include(x => x.Address)
                .ThenInclude(x => x.Country)
                .ThenInclude(x => x.Cities)
                .ThenInclude(x => x.hoods)
                .ToList();
            var MyProp = new List<FavVM>();

            foreach (var prop in data)
            {
                MyProp.Add
                (
                    new FavVM
                    {
                        RealId = prop.ID,
                        ImageName = prop.Images.FirstOrDefault().ImageName,
                        Title = prop.Name,
                        Price = prop.Price,
                        Country = prop.Address.Country.Name,
                        City = prop.Address.City.Name,
                        Hood = prop.Address.Hood.Name

                    }
                );
            }
            return View(MyProp);
        }
        [HttpGet]
        public IActionResult RemoveMyProperties(string id)
        {
            var reales = db.RealES.Include(x => x.Comments).Include(x => x.Address).Include(x => x.Favorites).Include(x => x.RealESFeatures).Include(x => x.Images).FirstOrDefault(x => x.ID == id);
            var address = db.Addresses.Where(x => x.RealESID == id).FirstOrDefault();
            var room = db.Rooms.Where(x => x.RealESId == id).FirstOrDefault();
            db.RealES.Remove(reales);
            db.Addresses.Remove(address);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
