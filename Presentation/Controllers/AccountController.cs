using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Models;

using Presentation.ViewModels.UserVms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Presentation.Controllers
{

    public class AccountController : Controller
    {

        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> user, RoleManager<IdentityRole> role, SignInManager<IdentityUser> signIn, IWebHostEnvironment webHostEnvironment)
        {
            _roleManager = role;
            _userManager = user;
            _signInManager = signIn;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM data)
        {
            if (data == null)
            {
                return View();
            }
            if (data.RememberMe == true)
            {

            var res = await _signInManager.PasswordSignInAsync(data.Email, data.Password, true, false);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                var res = await _signInManager.PasswordSignInAsync(data.Email, data.Password, false, false);

                return RedirectToAction("Index", "Home");
            }
           
            
       


        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM data)
        {
            if (data == null)
            {
                return View();
            }
            User User = new User
            {
                UserName = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                PhoneNumber = data.PhoneNumber,
                ImageName = "Default.png",
                ImagePath = _webHostEnvironment + "/Images/Default.png"

            };
            var res = await _userManager.CreateAsync(User, data.Password);
            if (res.Succeeded)
            {

                var resp = await _signInManager.PasswordSignInAsync(User, data.Password, true, false);
                if (resp.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {

            User resp = (User)await _userManager.GetUserAsync(User);
           

            var data = new ManageProfileVM
            {
                FirstName = resp.FirstName,
                LastName = resp.LastName,
                Email = resp.UserName,
                Country = resp.Country,
                City = resp.City,
                PhoneNumber = resp.PhoneNumber,
                Bio = resp.Bio
              
               
                
                
            };
            if (TempData["mess"] != null)
            {
                data.ValidationMessage = TempData["mess"]?.ToString();
                TempData["mess"] = null;
            }
            if (resp != null)
            {
                return View(data);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Manage(ManageProfileVM data)
        {

            var getUser = (User?)await _userManager.FindByIdAsync(_userManager.GetUserId(User));

            //if (data.ImageFile != null)
            //{
            //    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", data.ImageFile.FileName);

            //    using (var stream = new FileStream(path, FileMode.Create))
            //    {
            //        data.ImageFile.CopyTo(stream);
            //    }
            //getUser.ImagePath = path;
            //getUser.ImageName = data.ImageFile.FileName;
            //}
            getUser.FirstName = data.FirstName;
            getUser.LastName = data.LastName;
            getUser.UserName = data.Email;
            getUser.Country = data.Country;
            getUser.City = data.City;
            getUser.PhoneNumber = data.PhoneNumber;
            getUser.Bio = data.Bio;



            var resp = await _userManager.UpdateAsync(getUser);

            if (resp.Succeeded)
            {
                return RedirectToAction("Manage", "Account");
            }
            return View(data);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(ManageProfileVM pw)

        {
            var getUser = (User)await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var check = await _userManager.CheckPasswordAsync(getUser, pw.Password);

            if (check)
            {

                if (getUser != null)
                {
                    _signInManager.SignOutAsync();
                    _userManager.DeleteAsync(getUser);

                }
                return RedirectToAction("Register", "Account");
            }
            else
            {
                TempData["mess"] = "Couldn't disable your account ,Your password is wrong...!!";
            }

            return RedirectToAction("Manage", "Account");

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(CheckPwVM pw)
        {
            var getUser = (User)await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            if (getUser != null)
            {

                var check = await _userManager.CheckPasswordAsync(getUser, pw.CurrentPassword);

                if (check)
                {
                    
                    var change = await _userManager.ChangePasswordAsync(getUser, pw.CurrentPassword, pw.NewPassword);

                    if (change.Succeeded)
                    { 
                        
                        return RedirectToAction("Logout");

                    }



                }
                else//if the current password is not correct
                {

                    TempData["mess"] = " couldn't change password ,Current password or new password is incorrect...!";


                    return RedirectToAction("Manage","Account");
                }
            }
                return RedirectToAction("Manage");

        }


        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = (User) await _userManager.GetUserAsync(User);
            var data = new MyProfileVM();

            if (user != null)
            {
                data = new MyProfileVM
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.UserName,
                    Username = user.FirstName + " " + user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    BDay = user.BDay,
                    City = "Ka3ba",
                    Country = "KSA",
                    Bio= user.Bio,
                    ImageName = user.ImageName, 
                    CoverName = user.CoverName, 
                };
            }

            return View(data);
        }
        public  async Task<IActionResult> UploadProfile(Image file)
        {
            if(file != null)
            {
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", guid + file.formFile.FileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    file.formFile.CopyTo(stream);
                }
                var currUser = (User) await _userManager.GetUserAsync(User);
                if (currUser != null) {
                    currUser.ImageName = guid + file.formFile.FileName;
                    currUser.ImagePath = path;  
                }
                await _userManager.UpdateAsync(currUser);
            }
            return RedirectToAction("MyProfile");
        }
        public  async Task<IActionResult> UploadCover(Image file)
        {
            if(file != null)
            {
                var guid = Guid.NewGuid().ToString();
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", guid + file.formFile.FileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    file.formFile.CopyTo(stream);
                }
                var currUser = (User) await _userManager.GetUserAsync(User);
                if (currUser != null) {
                    currUser.CoverName = guid + file.formFile.FileName;
                    currUser.CoverPath = path;  
                await _userManager.UpdateAsync(currUser);
                }
            }
            return RedirectToAction("MyProfile");
        }
    }
}
