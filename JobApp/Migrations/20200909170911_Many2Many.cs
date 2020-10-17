using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApp.Migrations
{
    public partial class Many2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CV",
                table: "Seeker",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "CityJob",
                columns: table => new
                {
                    CityName = table.Column<string>(nullable: false),
                    JobID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityJob", x => new { x.CityName, x.JobID });
                    table.ForeignKey(
                        name: "FK_CityJob_City_CityName",
                        column: x => x.CityName,
                        principalTable: "City",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityJob_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    JobID = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => new { x.JobID, x.SkillName });
                    table.ForeignKey(
                        name: "FK_JobSkill_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkill_Skill_SkillName",
                        column: x => x.SkillName,
                        principalTable: "Skill",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeekerJob",
                columns: table => new
                {
                    SeekerID = table.Column<int>(nullable: false),
                    JobID = table.Column<int>(nullable: false),
                    SubmitDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeekerJob", x => new { x.SeekerID, x.JobID });
                    table.ForeignKey(
                        name: "FK_SeekerJob_Job_JobID",
                        column: x => x.JobID,
                        principalTable: "Job",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeekerJob_Seeker_SeekerID",
                        column: x => x.SeekerID,
                        principalTable: "Seeker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeekerSkill",
                columns: table => new
                {
                    SeekerID = table.Column<int>(nullable: false),
                    SkillName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeekerSkill", x => new { x.SeekerID, x.SkillName });
                    table.ForeignKey(
                        name: "FK_SeekerSkill_Seeker_SeekerID",
                        column: x => x.SeekerID,
                        principalTable: "Seeker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeekerSkill_Skill_SkillName",
                        column: x => x.SkillName,
                        principalTable: "Skill",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityJob_JobID",
                table: "CityJob",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_SkillName",
                table: "JobSkill",
                column: "SkillName");

            migrationBuilder.CreateIndex(
                name: "IX_SeekerJob_JobID",
                table: "SeekerJob",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "IX_SeekerSkill_SkillName",
                table: "SeekerSkill",
                column: "SkillName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityJob");

            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropTable(
                name: "SeekerJob");

            migrationBuilder.DropTable(
                name: "SeekerSkill");

            migrationBuilder.AlterColumn<byte[]>(
                name: "CV",
                table: "Seeker",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
