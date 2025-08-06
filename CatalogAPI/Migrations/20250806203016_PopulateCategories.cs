using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('drinks', 'drinks.png')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('snacks', 'snacks.png')");
            migrationBuilder.Sql("INSERT INTO Categories(Name, ImageUrl) VALUES('desserts', 'desserts.png')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE from Categories");
        }
    }
}
