using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IdentityManager.Authorization
{
    public class AuthorizationAge : AuthorizationHandler<MinimumAge>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAge requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(ClaimTypes.DateOfBirth);

            if (dateOfBirthClaim is null)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);

            var age = DateTime.Today.Year - dateOfBirth.Year;

            if(dateOfBirth > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if (age >= requirement.Age)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}