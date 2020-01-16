using Microsoft.EntityFrameworkCore.Migrations;

namespace PCShop.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 511, nullable: false),
                    Unit = table.Column<string>(maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 511, nullable: false),
                    ParentProductComponentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComponent_ProductComponent_ParentProductComponentId",
                        column: x => x.ParentProductComponentId,
                        principalTable: "ProductComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 511, nullable: false),
                    ProductComponentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductComponent_ProductComponentId",
                        column: x => x.ProductComponentId,
                        principalTable: "ProductComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductComponentAttributeMap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductComponentId = table.Column<int>(nullable: false),
                    ProductAttributeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComponentAttributeMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComponentAttributeMap_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComponentAttributeMap_ProductComponent_ProductComponentId",
                        column: x => x.ProductComponentId,
                        principalTable: "ProductComponent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductComponentId",
                table: "Product",
                column: "ProductComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponent_ParentProductComponentId",
                table: "ProductComponent",
                column: "ParentProductComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponentAttributeMap_ProductAttributeId",
                table: "ProductComponentAttributeMap",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponentAttributeMap_ProductComponentId",
                table: "ProductComponentAttributeMap",
                column: "ProductComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductComponentAttributeMap");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "ProductComponent");
        }
    }
}
