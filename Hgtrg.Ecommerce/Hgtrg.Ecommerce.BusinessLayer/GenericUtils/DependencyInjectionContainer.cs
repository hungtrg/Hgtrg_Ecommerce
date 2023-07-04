using Hgtrg.Ecommerce.BusinessLayer.Services;
using Hgtrg.Ecommerce.DataLayer.DataAccess;
using Hgtrg.Ecommerce.DataLayer.Models;
using Hgtrg.Ecommerce.DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hgtrg.Ecommerce.BusinessLayer.GenericUtils
{
    public static class DependencyInjectionContainer
    {
        public static void InitializerDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<HgtrgEcommerceContext>, UnitOfWork<HgtrgEcommerceContext>>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<ISellerRepository , SellerRepository>();
            services.AddScoped<ISellerServices, SellerServices>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryServices, CategorySevices>();

            services.AddScoped<IProductRepositoy , ProductRepositry>();
            services.AddScoped<IProductServices, ProductServices>();
        }
    }
}
