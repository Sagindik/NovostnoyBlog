using Microsoft.Extensions.DependencyInjection;
using TestArtur.Services.Blogs;
using TestArtur.Services.Categorys;
using TestArtur.Services.Novosts;
using TestArtur.Services.Tegs;

namespace TestArtur
{
    public static class DependencyInjector
    {
        public static IServiceCollection RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(INovostService), typeof(NovostService));
            services.AddScoped(typeof(ITegService), typeof(TegService));
            services.AddScoped(typeof(IBlogService), typeof(BlogService));
            return services;
        }
    }
}
