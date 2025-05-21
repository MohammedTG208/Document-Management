using System.Reflection;
using System.Runtime.CompilerServices;
using DocumentManagement.Data.Models;
using __Document_Management_API.IService;
using Asp.Versioning.ApiExplorer;
using DocumentManagementAPI.Service;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DocumentManagementAPI.Repo;

namespace DocumentManagementAPI.Extensions.Service
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddSingleton<FileExtensionContentTypeProvider>();
            //CallConvThiscall for Sqlite connections 
            //services.AddDbContext<DocumentDbContext>(DbContextOptions => DbContextOptions.UseSqlite(configuration["ConnectionStrings:DocumentDbContext"]));
            services.AddDbContext<DocumentDbContext>(DbContextOptions => DbContextOptions.UseSqlServer(configuration["ConnectionStrings:DocumentDbContext"], sqlOp=> sqlOp.EnableRetryOnFailure()));
            services.AddScoped<Repo.User.IUser, UserService>();
            services.AddScoped<IDocument, DocumentService>();
            services.AddScoped<IUserRepo,UserRepo>();
            services.AddScoped<FolderRepo>();
            services.AddScoped<DocumentRepo>();
            services.AddScoped<MessagesRepo>();
            services.AddScoped<IFolder,FolderService>();
            services.AddScoped<IMessage,MessagesService>();
            services.AddScoped<IAuth,AuthService>();
            services.AddScoped<ProfileRepo>();
            services.AddScoped<ProfileService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication().AddJwtBearer(
                option =>
                {
                    option.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = configuration["Authntcation:Audince"],
                        ValidIssuer = configuration["Authntcation:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(configuration["Authntcation:SecretKey"]))
                    };
                }
                );
            services.AddAuthorization(option =>
            {
                option.AddPolicy("SystemAdmin", op =>
                {
                    op.RequireAuthenticatedUser();
                    op.RequireClaim(claimType: "Admin");
                });
            });

            services.AddApiVersioning(build =>
            {
                build.ReportApiVersions = true; // Adds supported version info in response headers
                build.AssumeDefaultVersionWhenUnspecified = true; // Defaults when version is not provided
                build.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0); // Sets default version to 1.0

            }).AddMvc() // Required for MVC API support
            .AddApiExplorer(build =>
            {
                build.SubstituteApiVersionInUrl = true;
            });

            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(build =>
            {
                foreach(var description in provider.ApiVersionDescriptions)
                {
                    build.SwaggerDoc($"{description.GroupName}", new()
                    {
                        Title = "Document Management",
                        Version = description.ApiVersion.ToString(),
                        Description = "From this api you can manage your doc from your folder"

                    });
                }
                var xmlCommand = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommandFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommand);

                build.IncludeXmlComments(xmlCommandFullPath);

                build.AddSecurityDefinition("DocumentManagementBearer", new()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Input fr valid Token to access this Api"
                });
                build.AddSecurityRequirement(new()
                {
                    {
                    new()
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="DocumentManagementBearer"
                        }
                    },
                    new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
