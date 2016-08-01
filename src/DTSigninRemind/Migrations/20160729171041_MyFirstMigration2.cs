using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DTSigninRemind.Migrations
{
    public partial class MyFirstMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyOnes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Conent = table.Column<string>(maxLength: 20, nullable: false),
                    CreateBy = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DOtype = table.Column<string>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOnes", x => x.Id);
                });

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "SigninReminds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsLeaderInDepts",
                table: "SigninReminds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "SigninReminds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "SigninReminds");

            migrationBuilder.DropColumn(
                name: "IsLeaderInDepts",
                table: "SigninReminds");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "SigninReminds");

            migrationBuilder.DropTable(
                name: "DailyOnes");
        }
    }
}
