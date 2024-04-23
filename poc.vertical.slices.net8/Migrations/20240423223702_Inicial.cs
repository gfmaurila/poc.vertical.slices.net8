using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc.vertical.slices.net8.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Tags = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "DATE", nullable: false),
                    PublishedOnUtc = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.InsertData(
            table: "Article",
            columns: new[] { "Id", "Title", "Description", "Tags", "CreatedOnUtc", "PublishedOnUtc" },
            values: new object[,]
            {
                { Guid.NewGuid(), "First Article", "This is the first article description", "News,First", DateTime.UtcNow, null },
                { Guid.NewGuid(), "Second Article", "This is the second article description", "News,Second", DateTime.UtcNow, null }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");
        }
    }
}
