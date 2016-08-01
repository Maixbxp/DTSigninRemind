using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DTSigninRemind.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SendLogTbs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    CorpID = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Msgtype = table.Column<string>(nullable: false),
                    PushType = table.Column<string>(nullable: false),
                    SendState = table.Column<bool>(nullable: true),
                    SendTime = table.Column<DateTime>(nullable: true),
                    Userid = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendLogTbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SigninReminds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Avatar = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsBoss = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Is_sys = table.Column<bool>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    OffRemind = table.Column<int>(nullable: false),
                    OffTime = table.Column<TimeSpan>(nullable: false),
                    RemindMode = table.Column<string>(nullable: false),
                    Sys_level = table.Column<string>(nullable: true),
                    Userid = table.Column<string>(nullable: false),
                    WorkRemind = table.Column<int>(nullable: false),
                    WorkTime = table.Column<TimeSpan>(nullable: false),
                    _OffRemind = table.Column<string>(nullable: false),
                    _WorkRemind = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SigninReminds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendLogTbs");

            migrationBuilder.DropTable(
                name: "SigninReminds");
        }
    }
}
