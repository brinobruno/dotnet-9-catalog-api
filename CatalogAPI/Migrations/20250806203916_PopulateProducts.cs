using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Coca-Cola', 'cool soda', 1.99, 'coca-cola.png', 100, now(), 1)");
            
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Tuna sandwich', 'cool sandwich', 2.59, 'tuna-sandwich.png', 15, now(), 2)");
            
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pudding', 'cool pudding', 1.99, 'pudding.png', 20, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
