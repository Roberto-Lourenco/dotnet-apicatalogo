using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class RefactorProductRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                schema: "core",
                table: "products");

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                schema: "core",
                table: "products",
                column: "category_id",
                principalSchema: "core",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                schema: "core",
                table: "products");

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                schema: "core",
                table: "products",
                column: "category_id",
                principalSchema: "core",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
