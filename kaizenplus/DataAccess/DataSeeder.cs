using System;
using Microsoft.EntityFrameworkCore;
using kaizenplus.Domain;
using kaizenplus.Domain.Roles;
using kaizenplus.Domain.UserRoles;
using kaizenplus.Domain.Users;
using kaizenplus.Enums;
using kaizenplus.Domain.Countries;

namespace kaizenplus.DataAccess
{
    public static class DataSeeder
    {
        /// <summary>
        /// Insert System roles (if not already there) into the database
        /// </summary>
        public static ModelBuilder SeedRoles(this ModelBuilder modelBuilder)
        {
            string[] roleNames = Enum.GetNames(typeof(Roles));
            object[] roles = new object[roleNames.Length];

            for (int i = 0; i < roleNames.Length; i++)
            {
                roles[i] = new { Id = i + 1, Name = roleNames[i] };
            }

            modelBuilder.Entity<Role>().HasData(roles);

            return modelBuilder;
        }

        /// <summary>
        /// Insert System Configurations (if not already there) into the database
        /// </summary>
        public static ModelBuilder SeedSystemConfig(this ModelBuilder modelBuilder)
        {
            object[] items = new object[1];
            items[0] = new
            {
                Id = 1
            };

       

            return modelBuilder;
        }

        /// <summary>
        /// Insert System Admin (if not already there) into the database
        /// </summary>
        public static ModelBuilder SeedAdminUser(this ModelBuilder modelBuilder)
        {
            CreatePasswordHash("P@ssw0rd", out byte[] salt, out byte[] hash);

            var userId = new Guid("8240573D-BECC-4AAE-B2AB-974979DE96A1");

            object user = new
            {
                Id = userId,
                Username = "admin@happywarehouse.com",
                Email = "admin@happywarehouse.com",
                PhoneNumber = "07950430205",
                FirstName = "system",
                LastName = "admin",
                CreatedDate = new DateTime(1989, 8, 18),
                PasswordHash = hash,
                PasswordSalt = salt,
                Active = true,
                IsVerified = true,
               
                AllowNotifications = true
            };

            object userRole = new
            {
                UserId = userId,
                RoleId = 1
            };

            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<UserRole>().HasData(userRole);

            return modelBuilder;
        }
        public static ModelBuilder SeedCountry(this ModelBuilder modelBuilder)
        {


            object Country1 = new
            {
                Id = (long)1,
                Name = "Jordan"
            };

            object Country2 = new
            {
                Id = (long)2,
                Name = "Germany"
            };


            modelBuilder.Entity<Country>().HasData(Country1);
            modelBuilder.Entity<Country>().HasData(Country2);


            return modelBuilder;
        }
        private static void CreatePasswordHash(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}