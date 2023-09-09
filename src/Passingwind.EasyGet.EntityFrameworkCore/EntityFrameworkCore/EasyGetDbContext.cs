using Microsoft.EntityFrameworkCore;
using Passingwind.EasyGet.Feeds;
using Passingwind.EasyGet.Packages;
using Passingwind.EasyGet.Packages.NuGets;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Passingwind.EasyGet.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class EasyGetDbContext :
    AbpDbContext<EasyGetDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    /// <summary>
    /// Identity
    /// </summary>
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    /// <summary>
    /// Tenant Management
    /// </summary>
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; }

    #endregion Entities from the modules

    public DbSet<Feed> Feeds { get; set; }

    public DbSet<Package> Packages { get; set; }

    public DbSet<NuGetPackage> NuGetPackages { get; set; }

    public EasyGetDbContext(DbContextOptions<EasyGetDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EasyGetConsts.DbTablePrefix + "YourEntities", EasyGetConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder
            .Entity<Feed>(b =>
            {
                b.ToTable(EasyGetConsts.DbTablePrefix + "Feeds", EasyGetConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(128);

                b.HasOne(x => x.Mirror).WithOne().HasForeignKey<FeedMirror>(x => x.FeedId);

                b.HasIndex(x => x.Name);
            })
            .Entity<FeedMirror>(b =>
            {
                b.ToTable(EasyGetConsts.DbTablePrefix + "FeedMirrors", EasyGetConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.MirrorUrl).IsRequired().HasMaxLength(256);

                b.HasKey(x => x.FeedId);
            })
            .Entity<Package>(b =>
            {
                b.ToTable(EasyGetConsts.DbTablePrefix + "Packages", EasyGetConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.Property(x => x.LatestVersion).IsRequired().HasMaxLength(32);

                b.HasIndex(x => new { x.FeedId, x.Name });
            })
            .Entity<NuGetPackage>(b =>
            {
                b.ToTable(EasyGetConsts.DbTablePrefix + "NuGetPackages", EasyGetConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                b.Property(x => x.Version).IsRequired().HasMaxLength(32);

                b.HasIndex(x => new { x.FeedId, x.Name });

                b.HasOne(x => x.Package).WithMany().HasForeignKey(x => x.PackageId);

                b.HasMany(x => x.PackageTypes).WithOne().HasForeignKey(x => x.NuGetPackageId);
            })
            .Entity<NuGetPackageType>(b =>
            {
                b.ToTable(EasyGetConsts.DbTablePrefix + "NuGetPackageTypes", EasyGetConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(x => x.PackageType).IsRequired().HasMaxLength(64);
                b.Property(x => x.Version).IsRequired().HasMaxLength(32);

                b.HasKey(x => new { x.NuGetPackageId, x.PackageType, x.Version });
            })
            ;
    }
}
