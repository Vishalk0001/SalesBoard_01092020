using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesBoard.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalesBoard.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class RoleController : Controller
    {     
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationUser userData;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            userData = new ApplicationUser();
        }

        
        public IActionResult Index()
        {      
            var roledt = _roleManager.Roles.ToList(); 
            return View(roledt);
        }

        
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
       
        public IActionResult UserList()
        {
            //MS: List of Users 
            var users = _userManager.Users.ToList();
            return View(users);
        }
      
        public IActionResult EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }  
            ViewData["roles"] = _roleManager.Roles.ToList();
            var user = _userManager.FindByIdAsync(id); 
             
            TempData["UserData"] = id.ToString();
            if (user == null)
            {
                return NotFound();
            }
            
          return View(user.Result); 
          //return RedirectToAction("AssignRole"); //MS: for testing 
        }
        [HttpGet] 
        public string SelectedRole(string Name)
        {
            TempData["SelecedRole"] = Name;
            return "OK";
        }
      
        public async Task<IActionResult> AssignRole()
        {
            string RoleName = TempData["SelecedRole"].ToString();
            string userID = TempData["UserData"].ToString();

            ApplicationUser user = await _userManager.FindByIdAsync(userID);
            
            await _userManager.AddToRoleAsync(user, RoleName);  
            return RedirectToAction("UserList", "Role");
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, NormalizedName")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(role); 
                ViewData["roles"] = _roleManager.Roles.ToList();
                return RedirectToAction(nameof(Index));
            }
            ViewData["roles"] = _roleManager.Roles.ToList();
            return View(role);
        } 
    }
}
