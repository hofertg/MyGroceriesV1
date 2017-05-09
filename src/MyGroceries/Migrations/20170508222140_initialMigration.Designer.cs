using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyGroceries.Data;

namespace MyGroceries.Migrations
{
    [DbContext(typeof(MyGroceriesDbContext))]
    [Migration("20170508222140_initialMigration")]
    partial class initialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyGroceries.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name");

                    b.Property<int>("UnitID");

                    b.HasKey("ID");

                    b.HasIndex("UnitID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MyGroceries.Models.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Directions");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("MyGroceries.Models.RecipeItem", b =>
                {
                    b.Property<int>("RecipeID");

                    b.Property<int>("ItemID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<double>("Quantity");

                    b.HasKey("RecipeID", "ItemID");

                    b.HasIndex("ItemID");

                    b.HasIndex("RecipeID");

                    b.ToTable("RecipeItems");
                });

            modelBuilder.Entity("MyGroceries.Models.ShoppingList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("MyGroceries.Models.ShoppingListItem", b =>
                {
                    b.Property<int>("ShoppingListID");

                    b.Property<int>("ItemID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<double>("Quantity");

                    b.Property<bool>("StrikeThrough");

                    b.HasKey("ShoppingListID", "ItemID");

                    b.HasIndex("ItemID");

                    b.HasIndex("ShoppingListID");

                    b.ToTable("ShoppingListItems");
                });

            modelBuilder.Entity("MyGroceries.Models.Unit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaseUnit");

                    b.Property<double>("ConversionToBaseFactor");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("MyGroceries.Models.Item", b =>
                {
                    b.HasOne("MyGroceries.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyGroceries.Models.RecipeItem", b =>
                {
                    b.HasOne("MyGroceries.Models.Item", "Item")
                        .WithMany("RecipeItems")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyGroceries.Models.Recipe", "Recipe")
                        .WithMany("RecipeItems")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyGroceries.Models.ShoppingListItem", b =>
                {
                    b.HasOne("MyGroceries.Models.Item", "Item")
                        .WithMany("ShoppingListItems")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyGroceries.Models.ShoppingList", "ShoppingList")
                        .WithMany("ShoppingListItems")
                        .HasForeignKey("ShoppingListID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
