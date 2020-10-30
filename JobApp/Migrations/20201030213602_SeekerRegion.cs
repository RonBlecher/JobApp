using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApp.Migrations
{
    public partial class SeekerRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Publisher_PublisherID",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "PublisherID",
                table: "Job",
                newName: "PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Job_PublisherID",
                table: "Job",
                newName: "IX_Job_PublisherId");

            migrationBuilder.AlterColumn<double>(
                name: "Lon",
                table: "Job",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Job",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.CreateTable(
                name: "SeekerRegion",
                columns: table => new
                {
                    SeekerID = table.Column<int>(nullable: false),
                    RegionName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeekerRegion", x => new { x.SeekerID, x.RegionName });
                    table.ForeignKey(
                        name: "FK_SeekerRegion_Region_RegionName",
                        column: x => x.RegionName,
                        principalTable: "Region",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeekerRegion_Seeker_SeekerID",
                        column: x => x.SeekerID,
                        principalTable: "Seeker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeekerRegion_RegionName",
                table: "SeekerRegion",
                column: "RegionName");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Publisher_PublisherId",
                table: "Job",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Publisher_PublisherId",
                table: "Job");

            migrationBuilder.DropTable(
                name: "SeekerRegion");

            migrationBuilder.RenameColumn(
                name: "PublisherId",
                table: "Job",
                newName: "PublisherID");

            migrationBuilder.RenameIndex(
                name: "IX_Job_PublisherId",
                table: "Job",
                newName: "IX_Job_PublisherID");

            migrationBuilder.AlterColumn<double>(
                name: "Lon",
                table: "Job",
                type: "double",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Job",
                type: "double",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Publisher_PublisherID",
                table: "Job",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
