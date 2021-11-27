using saloonAPI.Models;
using saloonAPI.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlService : IDataAccessRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public List<CustomerTelNumber> GetTelNumbers(string userId)
        {
            List<CustomerTelNumber> customerTelNumbers = new List<CustomerTelNumber>();
            var userCollection = _context.Users as IQueryable<ApplicationUser>;

            var user = userCollection.FirstOrDefault(u => u.Id == userId);

            if (user is not null)
            {
                customerTelNumbers = user.TelNumbers.ToList();
            }

            return customerTelNumbers;
        }
    }
}
