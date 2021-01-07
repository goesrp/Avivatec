using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RickLocalization_api.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using RickLocalization_api.EF;
using Microsoft.EntityFrameworkCore;

namespace RickLocalization_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
        services.AddCors ()
                .AddMvc ()
                .SetCompatibilityVersion (CompatibilityVersion.Version_3_0);

            services.AddDbContext<RickLocalizationContext> (options => options.UseSqlServer (Configuration["SQLConfig:Connection"]));

            services.AddScoped<IRickService, RickService> ();
            services.AddScoped<IMotyService, MotyService> ();
            services.AddScoped<ITravelService, TravelService> ();
            services.AddScoped<IDimensionService, DimensionService> ();
    
            services.AddControllers();

            #region ApiVersioning
                services.AddApiVersioning (options => {
                    options.UseApiBehavior = false;
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion (1, 0);
                    options.ApiVersionReader = ApiVersionReader.Combine (
                        new HeaderApiVersionReader ("x-api-version"),
                        new QueryStringApiVersionReader (),
                        new UrlSegmentApiVersionReader ()
                    );
                });
                services.AddVersionedApiExplorer (
                    options => {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        options.SubstituteApiVersionInUrl = true;
                     });
                    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions> ();
                    services.AddSwaggerGen (options => { // integrate xml comments
                    

                    options.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement (new OpenApiSecurityRequirement () {
                        {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                }
                            }, new List<string> ()
                        }
                    });
                });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,  IApiVersionDescriptionProvider provider ,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger ();
            app.UseSwaggerUI (  options => {
                                    foreach (var description in provider.ApiVersionDescriptions) {
                                        options.SwaggerEndpoint (
                                            $"/swagger/{description.GroupName}/swagger.json",
                                            description.GroupName.ToUpperInvariant ());
                                    }
                                });

            //app.UseHttpsRedirection();
            app.UseRouting();
          
            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseCors ("InternalPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string XmlCommentsFilePath {
            get {
                var basePath = Environment.CurrentDirectory; // PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof (Startup).GetTypeInfo ().Assembly.GetName ().Name + ".xml";
                return Path.Combine (basePath, fileName);
            }
        }
    }
}
