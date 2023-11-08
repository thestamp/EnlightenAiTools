using Microsoft.AspNetCore.Authorization;

namespace Enlighten.Admin.Web
{
    public class UserEmailRequirement : IAuthorizationRequirement { }

    public class UserEmailHandler : AuthorizationHandler<UserEmailRequirement>
    {
        private readonly List<string> _allowedUsers;

        public UserEmailHandler(List<string> allowedUsers)
        {
            _allowedUsers = allowedUsers;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserEmailRequirement requirement)
        {
            var userEmailClaim = context.User?.FindFirst(c => c.Type == "emails");
            if (userEmailClaim != null && _allowedUsers.Contains(userEmailClaim.Value))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
