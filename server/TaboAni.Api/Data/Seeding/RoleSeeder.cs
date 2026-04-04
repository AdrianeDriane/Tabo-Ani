using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Seeding;

internal static class RoleSeeder
{
    private static readonly RoleSeedDefinition[] PlatformRoles =
    [
        new(
            SeedConstants.AdminRoleId,
            "ADMIN",
            "Admin",
            "Platform administrator role."),
        new(
            SeedConstants.FarmerRoleId,
            "FARMER",
            "Farmer",
            "Farmer account role."),
        new(
            SeedConstants.BuyerRoleId,
            "BUYER",
            "Buyer",
            "Buyer account role."),
        new(
            SeedConstants.DistributorRoleId,
            "DISTRIBUTOR",
            "Distributor",
            "Distributor account role.")
    ];

    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var roleSeed in PlatformRoles)
        {
            var role = await context.Roles.SingleOrDefaultAsync(
                existingRole => existingRole.RoleCode == roleSeed.RoleCode,
                cancellationToken);

            if (role is null)
            {
                role = new Role
                {
                    RoleId = roleSeed.RoleId,
                    CreatedAt = now
                };

                context.Roles.Add(role);
            }

            role.RoleCode = roleSeed.RoleCode;
            role.RoleName = roleSeed.RoleName;
            role.Description = roleSeed.Description;

            await context.SaveChangesAsync(cancellationToken);
        }

        PrintSeedSummary();
    }

    private static void PrintSeedSummary()
    {
        Console.WriteLine("Platform role seed completed.");
        Console.WriteLine("Command: dotnet run --project .\\TaboAni.Api\\TaboAni.Api.csproj -- --seed-platform-roles");
        Console.WriteLine();
        Console.WriteLine("Roles:");

        foreach (var role in PlatformRoles)
        {
            Console.WriteLine($"  {role.RoleCode}: {role.RoleId}");
        }
    }

    private sealed record RoleSeedDefinition(
        Guid RoleId,
        string RoleCode,
        string RoleName,
        string Description);
}
