using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BBDS.Management.Data;

namespace BBDS.Management.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            List<City> cities = new List<City>
            {
                new City
                {
                    Id = Guid.Parse("3a4dd904-d3b8-4adb-b6a6-4e42267c9683"),
                    CityName = "София"
                },new City
                {
                     Id = Guid.Parse("65078407-8ae3-4872-a807-ce3484306a99"),
                    CityName = "Варна"
                },new City
                {
                     Id = Guid.Parse("72316b2d-31a5-4ba8-8609-13719f7cce98"),
                    CityName = "Велиоко Търново"
                },new City
                {
                     Id = Guid.Parse("f40854ce-7d63-46a4-bb60-07dde1a4705e"),
                    CityName = "Пловдив"
                },new City
                {
                     Id = Guid.Parse("d0e60734-f384-4ccb-b472-61e8e9629e48"),
                    CityName = "Враца"
                },new City
                {
                     Id = Guid.Parse("56393f6b-d016-4583-b536-726ad925ed8a"),
                    CityName = "Левски"
                },new City
                {
                     Id = Guid.Parse("00182e26-fd34-436a-988a-69a8ce6ceb16"),
                    CityName = "Горна Оряховица"
                },new City
                {
                     Id = Guid.Parse("7aac1f3b-ef3e-461e-8aab-857846f67fa4"),
                    CityName = "Дряново"
                }
            };
            builder.HasData(cities);
        }
    }
}

