using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Application;

public static class ServiceRegistration
{
	public static void AddApplication(this IServiceCollection service)
	{
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}

