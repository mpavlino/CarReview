using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Review.DAL;
using Review.Model;

namespace Review {
    public class Startup {
        public Startup( IConfiguration configuration ) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            services.Configure<CookiePolicyOptions>( options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            } );

            services.AddDbContext<CarManagerDbContext>( options => options.UseInMemoryDatabase( "CarManager" ) );

            services.AddDbContext<CarManagerDbContext>( options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString( "CarManagerDbContext" ) ) );

            services.AddDefaultIdentity<AppUser>()
                .AddEntityFrameworkStores<CarManagerDbContext>();


            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_3_0 );
            services.AddRazorPages();

            services.AddAuthentication();
                //.AddGoogle( options => {
                //    IConfigurationSection googleAuthNSection =
                //    Configuration.GetSection( "Authentication:Google" );
                //    options.ClientId = googleAuthNSection["ClientId"];
                //    options.ClientSecret = googleAuthNSection["ClientSecret"];

                //} );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env , CarManagerDbContext context) {
            if( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
                context.Database.EnsureCreated();
            }
            else {
                app.UseExceptionHandler( "/Home/Error" );
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            var supportedCultures = new[]
{
                new CultureInfo("hr"),
                new CultureInfo("en-US")
            };

            app.UseRequestLocalization( new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture( "hr" ),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            } );

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints( endpoints => {
                endpoints.MapControllerRoute( "default", "{controller=Home}/{action=Index}" );
                endpoints.MapRazorPages();
            } );
        }
    }
}
