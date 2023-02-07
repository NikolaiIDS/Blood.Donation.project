using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BBDS.Management.Data.Configurations

{
    public class BloodTypeConfiguration : IEntityTypeConfiguration<BloodType>
    {
        public void Configure(EntityTypeBuilder<BloodType> builder)
        {
            List<BloodType> bloodTypes = new List<BloodType>
            {
                new BloodType
                {
                    Id = 1,
                    TypeName = "A+"
                },new BloodType
                {
                     Id = 2,
                    TypeName = "A-"
                },new BloodType
                {
                     Id = 3,
                    TypeName = "B+"
                },new BloodType
                {
                     Id = 4,
                    TypeName = "B-"
                },new BloodType
                {
                     Id = 5,
                    TypeName = "AB+"
                },new BloodType
                {
                     Id = 6,
                    TypeName = "AB-"
                },new BloodType
                {
                     Id = 7,
                    TypeName = "0+"
                },new BloodType
                {
                     Id = 8,
                    TypeName = "0-"
                }
            };
            builder.HasData(bloodTypes);
        }
    }
}
