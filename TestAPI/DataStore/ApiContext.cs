using System;
using Microsoft.EntityFrameworkCore;
using TestAPI.Entities;

namespace TestAPI.DataStore
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
			                                            : base(options)
		{
		  
        }
        public DbSet<PhoneNumberRecord> customerPhoneNumber { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PhoneNumberRecord>()
                        .HasKey(c => new { c.CustomerId, c.PhoneNumber });
		}
    }
}
