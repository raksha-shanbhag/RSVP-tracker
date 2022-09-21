using RSVPtracker.Core.Interfaces;
using RSVPtracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSVPtracker.Infrastructure
{
    public class DatabaseInitialiser : IDatabaseInitialiser
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseInitialiser(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedSampleData()
        {
            await _dbContext.Database.EnsureCreatedAsync().ConfigureAwait(false);

            if (!_dbContext.Users.Any())
            {
                var testUser1= new User
                {
                    FullName = "Test User 1",
                    PhoneNumber = 3333333,
                    Email = "test@Gmail.com",
                    UserName = "test2@123",
                    CreatedDate = DateTime.Now,
                    CreatedBy = 2
                };

                var testUser2 = new User
                {
                    FullName = "Test Use 2",
                    PhoneNumber = 3333333,
                    Email = "test@Gmail.com",
                    UserName = "test1@123",
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1
                };

                _dbContext.Users.Add(testUser2);
                _dbContext.Users.Add(testUser1);
                
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
