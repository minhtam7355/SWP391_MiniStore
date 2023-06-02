using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP391_MiniStore.Data;
using SWP391_MiniStore.Models.Domain;
using System.Security.Claims;

namespace SWP391_MiniStore.Controllers
{
    public static class ControllerExtensions
    {
        public static async Task<StoreStaff?> GetCurrentStoreStaffAsync(this Controller controller, MiniStoreDbContext _dbContext, HttpContext httpContext)
        {
            // Get the staff ID claim for the current user
            string? staffID = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the StoreStaff data based on the user ID
            StoreStaff? staff = await _dbContext.StoreStaff.FirstOrDefaultAsync(s => s.StaffID.ToString() == staffID);

            return staff;
        }

        // NEED MODIFY LIKE IN THE LOGIN POST ACTION
        public static async Task UpdateClaims(this Controller controller, MiniStoreDbContext dbContext, HttpContext httpContext, StoreStaff updatedStaff)
        {
            // Get the existing claims
            List<Claim> existingClaims = new List<Claim>(httpContext.User.Claims);

            // Find the claim to be updated by its type and remove it
            Claim? staffIDClaim = existingClaims.Find(c => c.Type == ClaimTypes.NameIdentifier);
            if (staffIDClaim != null)
            {
                existingClaims.Remove(staffIDClaim);
            }
            Claim? usernameClaim = existingClaims.Find(c => c.Type == ClaimTypes.Name);
            if (usernameClaim != null)
            {
                existingClaims.Remove(usernameClaim);
            }
            Claim? emailClaim = existingClaims.Find(c => c.Type == ClaimTypes.Email);
            if (emailClaim != null)
            {
                existingClaims.Remove(emailClaim);
            }
            Claim? phoneNumberClaim = existingClaims.Find(c => c.Type == ClaimTypes.MobilePhone);
            if (phoneNumberClaim != null)
            {
                existingClaims.Remove(phoneNumberClaim);
            }
            Claim? passwordClaim = existingClaims.Find(c => c.Type == "Password");
            if (passwordClaim != null)
            {
                existingClaims.Remove(passwordClaim);
            }
            Claim? dobClaim = existingClaims.Find(c => c.Type == ClaimTypes.DateOfBirth);
            if (dobClaim != null)
            {
                existingClaims.Remove(dobClaim);
            }
            Claim? addressClaim = existingClaims.Find(c => c.Type == ClaimTypes.StreetAddress);
            if (addressClaim != null)
            {
                existingClaims.Remove(addressClaim);
            }
            Claim? roleClaim = existingClaims.Find(c => c.Type == ClaimTypes.Role);
            if (roleClaim != null)
            {
                existingClaims.Remove(roleClaim);
            }
            Claim? hourlyRateClaim = existingClaims.Find(c => c.Type == "HourlyRate");
            if (hourlyRateClaim != null)
            {
                existingClaims.Remove(hourlyRateClaim);
            }
            Claim? staffStatusClaim = existingClaims.Find(c => c.Type == "StaffStatus");
            if (staffStatusClaim != null)
            {
                existingClaims.Remove(staffStatusClaim);
            }

            // Add the updated claims only if the corresponding properties are not null
            existingClaims.Add(new Claim(ClaimTypes.NameIdentifier, updatedStaff.StaffID.ToString()));

            if (!string.IsNullOrEmpty(updatedStaff.Username))
            {
                existingClaims.Add(new Claim(ClaimTypes.Name, updatedStaff.Username));
            }

            if (!string.IsNullOrEmpty(updatedStaff.Email))
            {
                existingClaims.Add(new Claim(ClaimTypes.Email, updatedStaff.Email));
            }

            if (!string.IsNullOrEmpty(updatedStaff.PhoneNumber))
            {
                existingClaims.Add(new Claim(ClaimTypes.MobilePhone, updatedStaff.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(updatedStaff.Password))
            {
                existingClaims.Add(new Claim("Password", updatedStaff.Password));
            }

            if (updatedStaff.Dob != null)
            {
                existingClaims.Add(new Claim(ClaimTypes.DateOfBirth, updatedStaff.Dob.Value.ToString()));
            }

            if (!string.IsNullOrEmpty(updatedStaff.Address))
            {
                existingClaims.Add(new Claim(ClaimTypes.StreetAddress, updatedStaff.Address));
            }

            if (!string.IsNullOrEmpty(updatedStaff.StaffRole))
            {
                existingClaims.Add(new Claim(ClaimTypes.Role, updatedStaff.StaffRole));
            }

            if (updatedStaff.HourlyRate != null)
            {
                existingClaims.Add(new Claim("HourlyRate", updatedStaff.HourlyRate.Value.ToString()));
            }

            if (updatedStaff.StaffStatus != null)
            {
                existingClaims.Add(new Claim("StaffStatus", updatedStaff.StaffStatus.Value.ToString()));
            }

            // Get the existing authentication properties
            var authenticationInfo = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var existingProperties = authenticationInfo?.Properties;

            // Create new authentication properties with the updated properties
            var newProperties = new AuthenticationProperties
            {
                AllowRefresh = existingProperties?.AllowRefresh ?? true,
                IsPersistent = existingProperties?.IsPersistent ?? false,
            };

            // Update the user claims
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(existingClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), newProperties);
        }
    }
}
