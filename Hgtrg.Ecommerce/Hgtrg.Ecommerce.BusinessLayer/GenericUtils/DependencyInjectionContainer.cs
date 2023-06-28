using Hgtrg.Ecommerce.BusinessLayer.Services;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
//using Hgtrg.Ecommerce.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hgtrg.Ecommerce.BusinessLayer.GenericUtils
{
    public static class DependencyInjectionContainer
    {
        public static void InitializerDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, HgtrgEcommerceContext>();

            services.AddScoped<IUserServices, UserServices>();
        }
    }
}
