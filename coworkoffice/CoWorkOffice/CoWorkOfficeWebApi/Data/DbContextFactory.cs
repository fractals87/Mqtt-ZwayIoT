using CoWorkOffice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorkOfficeWebApi.Data
{
    public class DbContextFactory
    {
        public ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=3P-NB001\\SQLEXPRESS;Database=CoWorkOffice;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
