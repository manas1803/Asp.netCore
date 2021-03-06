using Microsoft.EntityFrameworkCore;
using ParkyApi.Model;
using ParkyApi.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<NationalPark> NationalParks { get; set; }
    }
}
