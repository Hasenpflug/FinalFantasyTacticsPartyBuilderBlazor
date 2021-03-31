using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalFantasyTacticsPartyBuilderBlazor.Models
{
    public partial class FFTContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public FFTContext()
        {
        }

        public FFTContext(DbContextOptions<FFTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ability> Abilities { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<JobItem> JobItems { get; set; }
        public virtual DbSet<JobPrerequisites> Jobprerequisites { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<UnitName> UnitNames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseMySql(ConnectionString, x => x.ServerVersion("5.6.26-mysql"));                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ability>(entity =>
            {
                entity.HasKey(e => e.AbilityId)
                    .HasName("PRIMARY");

                entity.ToTable("abilities");

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_JobID");

                entity.Property(e => e.AbilityId)
                    .HasColumnName("AbilityID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AbilityType).HasColumnType("int(11)");

                entity.Property(e => e.AddStatusEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AmountBoost)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AmountDamage)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DamageEquation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Element)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EnergizeEquation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HealingEquation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HealthDrainedEquation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Height).HasColumnType("int(11)");

                entity.Property(e => e.HpPercentBoost).HasColumnType("int(11)");

                entity.Property(e => e.Hprestored)
                    .HasColumnName("HPRestored")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemEquipIds)
                    .HasColumnName("ItemEquipIDs")
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.JobId)
                    .HasColumnName("JobID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JpNeededToLearnPsp).HasColumnType("int(11)");

                entity.Property(e => e.JpNeededToLearnPsx).HasColumnType("int(11)");

                entity.Property(e => e.JumpBoost).HasColumnType("int(11)");

                entity.Property(e => e.MoveBoost).HasColumnType("int(11)");

                entity.Property(e => e.MpDrainedEquation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mpcost)
                    .HasColumnName("MPCost")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mprestored)
                    .HasColumnName("MPRestored")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PspName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PsxName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Quote)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Radius).HasColumnType("int(11)");

                entity.Property(e => e.Range).HasColumnType("int(11)");

                entity.Property(e => e.RemoveStatusEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Speed).HasColumnType("int(11)");

                entity.Property(e => e.SuccessRateEquation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Terrain)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Trigger)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Abilities)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_dbo.Abilities_dbo.Jobs_JobID");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.ItemCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("itemcategories");

                entity.Property(e => e.ItemCategoryId)
                    .HasColumnName("ItemCategoryID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EquipmentCategoryId)
                    .HasColumnName("EquipmentCategoryID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EquipmentCategoryName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ItemCategoryName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StartingItemImagePath)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PRIMARY");

                entity.ToTable("items");

                entity.HasIndex(e => e.ItemCategoryId)
                    .HasName("IX_ItemCategoryID");

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AcquiredViaCatch)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AcquiredViaInitialEquip)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AcquiredViaLocation)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AcquiredViaMode)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AcquiredViaPoach)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AcquiredViaSteal)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AcquiredViaTreasureHunt)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AttackPower).HasColumnType("int(11)");

                entity.Property(e => e.Cost).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Element)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ElementAbsorbed)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ElementBoosted)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ElementHalved)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ElementNegated)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ElementWeakness)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EquipStatusEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HitPercentage).HasColumnType("int(11)");

                entity.Property(e => e.HitStatusEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HPBonus)
                    .HasColumnName("HPBonus")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IconFileName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ImmuneStatusEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ItemCategoryId)
                    .HasColumnName("ItemCategoryID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JumpBoost).HasColumnType("int(11)");

                entity.Property(e => e.MagicAttackBoost).HasColumnType("int(11)");

                entity.Property(e => e.MagicalEvade).HasColumnType("int(11)");

                entity.Property(e => e.MoveBoost).HasColumnType("int(11)");

                entity.Property(e => e.MPBonus)
                    .HasColumnName("MPBonus")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Note)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhysicalAttackBoost).HasColumnType("int(11)");

                entity.Property(e => e.PhysicalEvade).HasColumnType("int(11)");

                entity.Property(e => e.PspName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PsxName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RemoveStatusEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpeedBoost).HasColumnType("int(11)");

                entity.Property(e => e.SpellEffect)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.ItemCategory)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemCategoryId)
                    .HasConstraintName("FK_dbo.Items_dbo.ItemCategories_ItemCategoryID");
            });

            modelBuilder.Entity<JobItem>(entity =>
            {
                entity.HasKey(e => e.JobItemCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("jobitems");

                entity.HasIndex(e => e.ItemCategoryId)
                    .HasName("IX_ItemCategoryID");

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_JobID");

                entity.Property(e => e.JobItemCategoryId)
                    .HasColumnName("JobItemCategoryID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ItemCategoryId)
                    .HasColumnName("ItemCategoryID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JobId)
                    .HasColumnName("JobID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ItemCategory)
                    .WithMany(p => p.Jobitems)
                    .HasForeignKey(d => d.ItemCategoryId)
                    .HasConstraintName("FK_dbo.JobItems_dbo.ItemCategories_ItemCategoryID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Jobitems)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_dbo.JobItems_dbo.Jobs_JobID");
            });

            modelBuilder.Entity<JobPrerequisites>(entity =>
            {
                entity.ToTable("jobprerequisites");

                entity.HasIndex(e => e.JobId)
                    .HasName("IX_JobID");

                entity.Property(e => e.JobPrerequisitesId)
                    .HasColumnName("JobPrerequisitesID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JobId)
                    .HasColumnName("JobID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JobLevelRequiredPsp).HasColumnType("int(11)");

                entity.Property(e => e.JobLevelRequiredPsx).HasColumnType("int(11)");

                entity.Property(e => e.JobRequiredId)
                    .HasColumnName("JobRequiredID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Jobprerequisites)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_dbo.JobPrerequisites_dbo.Jobs_JobID");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobId)
                    .HasName("PRIMARY");

                entity.ToTable("jobs");

                entity.Property(e => e.JobId)
                    .HasColumnName("JobID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AbilitySetPspName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AbilitySetPsxName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BaseCombatEvasion).HasColumnType("int(11)");

                entity.Property(e => e.BaseJumpHeight).HasColumnType("int(11)");

                entity.Property(e => e.BaseMoveLength).HasColumnType("int(11)");

                entity.Property(e => e.HPGrowthConstant)
                    .HasColumnName("HPGrowthConstant")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HPMultiplier)
                    .HasColumnName("HPMultiplier")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MagicalAttackGrowthConstant).HasColumnType("int(11)");

                entity.Property(e => e.MagicalAttackMultiplier).HasColumnType("int(11)");

                entity.Property(e => e.MPGrowthConstant)
                    .HasColumnName("MPGrowthConstant")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MPMultiplier)
                    .HasColumnName("MPMultiplier")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PhysicalAttackGrowthConstant).HasColumnType("int(11)");

                entity.Property(e => e.PhysicalAttackMultiplier).HasColumnType("int(11)");

                entity.Property(e => e.PspName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PsxName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SpeedGrowthConstant).HasColumnType("int(11)");

                entity.Property(e => e.SpeedMulitplier).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.HasKey(e => e.QuoteId)
                    .HasName("PRIMARY");

                entity.ToTable("quotes");

                entity.Property(e => e.QuoteId)
                    .HasColumnName("QuoteID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Gender).HasColumnType("int(11)");

                entity.Property(e => e.Text)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<UnitName>(entity =>
            {
                entity.HasKey(e => e.UnitNameId)
                    .HasName("PRIMARY");

                entity.ToTable("unitnames");

                entity.Property(e => e.UnitNameId)
                    .HasColumnName("UnitNameID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Gender).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
