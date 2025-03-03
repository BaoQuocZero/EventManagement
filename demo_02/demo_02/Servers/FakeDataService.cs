using Bogus;
using demo_02.Models;
using System;
using System.Collections.Generic;

namespace demo_02.Services
{
    public class FakeDataService
    {
        public List<User> GenerateFakeUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.UserId, f => f.IndexFaker + 1)
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password(10))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Classname, f => f.Random.Word())
                .RuleFor(u => u.CreateAt, f => f.Date.Past(1));

            return faker.Generate(count);
        }
    }
}
