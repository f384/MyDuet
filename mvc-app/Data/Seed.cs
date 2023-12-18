using Microsoft.AspNetCore.Identity;
using mvc_app.Data;
using mvc_app.Data.Enum;
using mvc_app.Models;
using mvc_app.Data.Enum;
using mvc_app.Models;
using System.Diagnostics;

namespace mvc_app.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Studios.Any())
                {
                    context.Studios.AddRange(new List<Studio>()
                    {
                        new Studio()
                        {
                            Title = "Band Studio",
                            Image = "https://images.unsplash.com/photo-1598653222000-6b7b7a552625?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8bXVzaWMlMjBzdHVkaW98ZW58MHx8MHx8fDA%3D",
                            Description = "To jest opis studia stworzonego dla zespołów",
                            StudioCategory = StudioCategory.Band,
                            Address = new Address()
                            {
                                Street = "Chopina 3",
                                City = "Katowice",
                                State = "Śląsk"
                            }
                         },
                        new Studio()
                        {
                            Title = "Producers Studio",
                            Image = "https://images.unsplash.com/photo-1516223725307-6f76b9ec8742?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fG11c2ljJTIwcHJvZHVjZXJ8ZW58MHx8MHx8fDA%3D",
                            Description = "To jest opis studia przeznaczonego dla collabów producenckich",
                            StudioCategory = StudioCategory.Producers,
                            Address = new Address()
                            {
                                Street = "Bacha 13",
                                City = "Mysłowice",
                                State = "Śląsk"
                            }
                        },
                        new Studio()
                        {
                            Title = "Solists Studio",
                            Image = "https://c0.wallpaperflare.com/preview/603/98/477/black-folding-chair-on-canvas.jpg",
                            Description = "To jest opis studia przeznaczonego dla Solistów",
                            StudioCategory = StudioCategory.Solists,
                            Address = new Address()
                            {
                                Street = "Vivaldiego 55",
                                City = "Chorzów",
                                State = "Śląsk"
                            }
                        },
                        new Studio()
                        {
                            Title = "Plug and Play Studio",
                            Image = "https://png.pngtree.com/background/20230522/original/pngtree-studio-with-guitars-in-it-picture-image_2685778.jpg",
                            Description = "To jest opis studia przeznaczonego dla muzyków z własnymi instrumentami",
                            StudioCategory = StudioCategory.PlugAndPlay,
                            Address = new Address()
                            {
                                Street = "Boba-Marley'a 44",
                                City = "Bytom",
                                State = "Śląsk"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Session
                if (!context.Sessions.Any())
                {
                    context.Sessions.AddRange(new List<Session>()
                    {
                        new Session()
                        {
                            Title = "Trap producers Session",
                            Image = "https://articles.roland.com/wp-content/uploads/2020/08/Roland-Cloud-Studio-102084-Medium.jpg",
                            Description = "To jest opis sesji nagraniowej przeznaczonej dla producentów muzyki trap",
                            SessionCategory = SessionCategory.HipHop,
                            Address = new Address()
                            {
                                Street = "Bacha 13",
                                City = "Mysłowice",
                                State = "Śląsk"
                            }
                        },
                 new Session()
                        {
                            Title = "Jazz Session",
                            Image = "https://articles.roland.com/wp-content/uploads/2020/08/Roland-Cloud-Studio-102084-Medium.jpg",
                            Description = "To jest opis sesji nagraniowej przeznaczonej dla miłośników jazzu",
                            SessionCategory = SessionCategory.Jazz,
                             Address = new Address()
                            {
                                Street = "Chopina 3",
                                City = "Katowice",
                                State = "Śląsk"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "filipkocima@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "filipkocima",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "krakowska 4",
                            City = "Mysłowice",
                            State = "Śląsk"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "krakowska 5",
                            City = "Mysłowice",
                            State = "Śląsk"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}