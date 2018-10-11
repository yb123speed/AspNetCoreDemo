using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRSSample.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneRecord",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(nullable: false),
                    AreaCode = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    CustomerRecordId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneRecord_Customers_CustomerRecordId",
                        column: x => x.CustomerRecordId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneRecord_CustomerRecordId",
                table: "PhoneRecord",
                column: "CustomerRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneRecord");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
