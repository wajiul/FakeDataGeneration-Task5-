using Bogus;
using FakeDataGeneration.Models;

namespace FakeDataGeneration.Services
{
    public class DataGeneratorService
    {
        private string GetAddress(Faker f)
        {
            string fullAddress = f.Address.FullAddress();
            string streetAddress = f.Address.StreetAddress();
            var shortAddress = $"{f.Address.StreetName()}, {f.Address.ZipCode()}";

            var randomChoice = f.Random.Int(1, 3);

            return randomChoice switch
            {
                1 => fullAddress,
                2 => streetAddress,
                3 => shortAddress,
                _ => streetAddress
            };
        }

        private string GetPhone(Faker f)
        {
            var internationalPhone = f.Phone.PhoneNumber("+## (#) ###-####");
            var nationalPhone = f.Phone.PhoneNumber("(###) ###-####");
            var localPhone = f.Phone.PhoneNumber("###-####");
            var dashedPhone = f.Phone.PhoneNumber("###-###-####");
            var spacedPhone = f.Phone.PhoneNumber("### ### ####");

            int randomChoice = f.Random.Int(1, 6);

            return randomChoice switch
            {
                1 => localPhone,
                2 => nationalPhone,
                3 => internationalPhone,
                4 => spacedPhone,
                5 => dashedPhone,
                _ => internationalPhone
            };
        }

        private Faker<UserModel> GetFaker(string locale)
        {
            var userGenerator = new Faker<UserModel>(locale)
                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Address, f => GetAddress(f))
                .RuleFor(u => u.Phone, f => GetPhone(f));
            return userGenerator;
        }
        
        

        public IEnumerable<UserModel> GetFakeData(int seed, string locale, int row)
        {

            Randomizer.Seed = new Random(seed);
            var faker = GetFaker(locale);
            var data = new List<UserModel>();
            for(int i =  0; i < row; i++)
            {
                data.Add(faker.Generate());
            }
            return data;
        }

    }
}
