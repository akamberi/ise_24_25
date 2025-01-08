using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ISEPay.Config
{
    public class SecurityConfig
    {
        public static void ConfigureSecurity(IServiceCollection services)
        {
            // Add Authorization policies here
            services.AddAuthorization(options =>
            {
                // Allow all users (public endpoint)
                options.AddPolicy("Public", policy => policy.RequireAssertion(context => true));

                // Only Admin users (admin-only endpoint)
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("ADMIN")
                          .RequireAuthenticatedUser());

                // Only authenticated users (user-only endpoint)
                options.AddPolicy("Authenticated", policy =>
                    policy.RequireAuthenticatedUser());

                // Specific policies based on roles or other claims can be added similarly
            });
        }
    }
}
