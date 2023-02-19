using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var superAdminRoleId = "38a78A9f-0f23-4e36-92d8-7bA9e0571974";
			var adminRoleId = "5541afff-5907-4576-8a5f-a6bf7342ef4a";
			var userRoleId = "0714bc9d-9a4c-4b8d-9774-470f4cf60f07";

			// seed roles (user, admin, super_admin)
			var roles = new List<IdentityRole>
			{
				new IdentityRole()
				{
					Name = "SuperAdmin",
					NormalizedName = "SuperAdmin",
					Id = superAdminRoleId,
					ConcurrencyStamp = superAdminRoleId
				},
				new IdentityRole()
				{
					Name = "Admin",
					NormalizedName = "Admin",
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId
				},
				new IdentityRole()
				{
					Name = "User",
					NormalizedName = "User",
					Id = userRoleId,
					ConcurrencyStamp = userRoleId
				},

			};
			builder.Entity<IdentityRole>().HasData(roles);

			// seed super_admin_user

			var superAdminId = "7c77bfef-ab90-4139-b35b-d3f9dc50695c";
			var superAdminUser = new IdentityUser()
			{
				Id = superAdminId,
				UserName = "superadmin@blog.com",
				Email = "superadmin@blog.com",
				NormalizedEmail = "superadmin@blog.com".ToUpper(),
				NormalizedUserName = "superadmin@blog.com".ToUpper()
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "superadmin2023");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			// add all roles to super_admin_user
			var superAdminRoles = new List<IdentityUserRole<string>>()
			{
				new IdentityUserRole<string>
				{
					RoleId = superAdminRoleId,
					UserId = superAdminId,
				},
				new IdentityUserRole<string>
				{
					RoleId = adminRoleId,
					UserId = superAdminId,
				},
				new IdentityUserRole<string>
				{
					RoleId = userRoleId,
					UserId = superAdminId,
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

		}
	}
}
