using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Specify the exact method overload to resolve ambiguity by using the generic version
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // If using FluentValidation  

            // Add MediatR behaviors (e.g., validation, logging)  
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Example validation behavior  

            return services;
        }
    }
}
