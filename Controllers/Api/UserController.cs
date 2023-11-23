using KeyGem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Web;
using Microsoft.Owin.Security;



namespace KeyGem.Controllers.Api
{
    public class UserController : ApiController
    {
        private ApplicationDbContext _context;


        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetUser()
        {
            var userData = _context.Users.Include(u => u.Roles).ToList();
            var roleData = _context.Roles.ToList();
            foreach (var user in userData)
            {
                if (user.Roles.Count != 0)
                {
                    var roleName = roleData.SingleOrDefault(r => r.Id == user.Roles.First().RoleId).Name;
                    user.RoleName = roleName;
                }
            }
            return Ok(userData);
        }

        [HttpDelete]
        [Route("api/user/{userId}")]
        public IHttpActionResult DeleteUser(string userId)
        {
            ApplicationUser userToDelete;

            using (var context = new ApplicationDbContext())
            {
                userToDelete = context.Users.Find(userId);
            }

            if (userToDelete == null)
            {
                return NotFound();
            }
            List<string> userRoles;

            using (var context = new ApplicationDbContext())
            {
                userRoles = context.Roles
                    .Where(r => r.Users.Any(u => u.UserId == userId))
                    .Select(r => r.Name)
                    .ToList();
            }
            using (var context = new ApplicationDbContext())
            {
                foreach (var roleName in userRoles)
                {
                    var role = context.Roles.SingleOrDefault(r => r.Name == roleName);

                    if (role != null)
                    {
                        var userRole = role.Users.SingleOrDefault(u => u.UserId == userId);

                        if (userRole != null)
                        {
                            context.Entry(userRole).State = EntityState.Deleted;
                        }
                    }
                }

                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Find(userId);

                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to find user for deletion.");
                }
            }
        }
    }
}
