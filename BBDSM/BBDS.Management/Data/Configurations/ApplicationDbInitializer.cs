using System;

namespace BBDS.Management.Data.Configurations
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<>();
                context.Database.EnsureCreated();

                if (!context..Any())
                {
                    context.Cinemas.AddRange(new List<>()
                    {
                        new()
                        {
                            Name = "Iskra",
                            Logo="https://upload.wikimedia.org/wikipedia/commons/f/f8/Iskra-logo.jpg",
                            Description="A cinema in Veliko Tarnovo"
                        },

                        new ()
                        {
                            Name = "Palace",
                            Logo="https://meetolerance.eu/wp-content/uploads/2018/11/logo-cinema-palace.png",
                            Description="A cinema in Europe"
                        },

                        new ()
                        {
                            Name = "Arena",
                            Logo="https://searchlogovector.com/wp-content/uploads/2018/07/arena-cinemas-logo-vector.png",
                            Description="A cinema in Bulgaria"
                        },

                    });
                }
            }
        }
    }
}
