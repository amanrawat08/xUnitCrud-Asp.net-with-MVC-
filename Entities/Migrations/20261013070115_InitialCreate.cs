using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CountryName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthOfDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CountryName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecieveNewsLetter = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { new Guid("12b8d5f4-6e03-47a9-b1d2-c4e7a9305f06"), "Germany" },
                    { new Guid("3f2a9c1e-7b4d-4e8a-9a61-1c5d2e8f0a01"), "India" },
                    { new Guid("5d92f7b3-a6c4-4108-93ef-0b8d6e2a5d04"), "Canada" },
                    { new Guid("6f1d8a2b-c3e5-4970-a2b4-18d9f5e3c708"), "Japan" },
                    { new Guid("8b7d4e52-2c19-4f3a-b6d8-5a90e1c47b02"), "United States" },
                    { new Guid("94a0c6e2-8d7b-4153-bf6a-3e2d1c9b5a10"), "Singapore" },
                    { new Guid("a9c3e1d7-4b82-4f60-8d15-6e0b27c9a107"), "France" },
                    { new Guid("c41e6a0d-9f38-4d27-8e5b-72a3b9d10c03"), "United Kingdom" },
                    { new Guid("d3e5b7a9-0f21-4c86-9b3d-a7c1e48f2d09"), "Brazil" },
                    { new Guid("e07a3c68-1d5f-4b92-a4c7-93f1d8b06e05"), "Australia" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "Address", "BirthOfDate", "CountryID", "CountryName", "Email", "Gender", "PersonName", "RecieveNewsLetter" },
                values: new object[,]
                {
                    { new Guid("b1a2c3d4-0001-4e5f-8a9b-1c2d3e4f5a01"), "12 MG Road, Connaught Place, New Delhi 110001", new DateTime(1992, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3f2a9c1e-7b4d-4e8a-9a61-1c5d2e8f0a01"), "India", "aarav.sharma@example.com", "Male", "Aarav Sharma", true },
                    { new Guid("b1a2c3d4-0002-4e5f-8a9b-1c2d3e4f5a02"), "48 Marine Drive, Kochi, Kerala 682001", new DateTime(1995, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3f2a9c1e-7b4d-4e8a-9a61-1c5d2e8f0a01"), "India", "priya.nair@example.com", "Female", "Priya Nair", false },
                    { new Guid("b1a2c3d4-0003-4e5f-8a9b-1c2d3e4f5a03"), "221 Maple Street, Austin, TX 73301", new DateTime(1988, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8b7d4e52-2c19-4f3a-b6d8-5a90e1c47b02"), "United States", "john.miller@example.com", "Male", "John Miller", true },
                    { new Guid("b1a2c3d4-0004-4e5f-8a9b-1c2d3e4f5a04"), "75 Lakeview Ave, Chicago, IL 60601", new DateTime(1990, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8b7d4e52-2c19-4f3a-b6d8-5a90e1c47b02"), "United States", "emily.davis@example.com", "Female", "Emily Davis", false },
                    { new Guid("b1a2c3d4-0005-4e5f-8a9b-1c2d3e4f5a05"), "9 Baker Street, London NW1 6XE", new DateTime(1985, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c41e6a0d-9f38-4d27-8e5b-72a3b9d10c03"), "United Kingdom", "oliver.smith@example.com", "Male", "Oliver Smith", true },
                    { new Guid("b1a2c3d4-0006-4e5f-8a9b-1c2d3e4f5a06"), "34 Princes Street, Edinburgh EH2 2AN", new DateTime(1998, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c41e6a0d-9f38-4d27-8e5b-72a3b9d10c03"), "United Kingdom", "sophie.taylor@example.com", "Female", "Sophie Taylor", true },
                    { new Guid("b1a2c3d4-0007-4e5f-8a9b-1c2d3e4f5a07"), "560 Rue Sherbrooke O, Montreal, QC H3A 1B9", new DateTime(1993, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5d92f7b3-a6c4-4108-93ef-0b8d6e2a5d04"), "Canada", "liam.tremblay@example.com", "Male", "Liam Tremblay", false },
                    { new Guid("b1a2c3d4-0008-4e5f-8a9b-1c2d3e4f5a08"), "18 George Street, Sydney NSW 2000", new DateTime(1991, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e07a3c68-1d5f-4b92-a4c7-93f1d8b06e05"), "Australia", "chloe.wilson@example.com", "Female", "Chloe Wilson", true },
                    { new Guid("b1a2c3d4-0009-4e5f-8a9b-1c2d3e4f5a09"), "Friedrichstrasse 101, 10117 Berlin", new DateTime(1987, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("12b8d5f4-6e03-47a9-b1d2-c4e7a9305f06"), "Germany", "lukas.weber@example.com", "Male", "Lukas Weber", false },
                    { new Guid("b1a2c3d4-0010-4e5f-8a9b-1c2d3e4f5a10"), "27 Rue de Rivoli, 75004 Paris", new DateTime(1996, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a9c3e1d7-4b82-4f60-8d15-6e0b27c9a107"), "France", "camille.dubois@example.com", "Female", "Camille Dubois", true },
                    { new Guid("b1a2c3d4-0011-4e5f-8a9b-1c2d3e4f5a11"), "3-5-1 Shibuya, Shibuya-ku, Tokyo 150-0002", new DateTime(1994, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6f1d8a2b-c3e5-4970-a2b4-18d9f5e3c708"), "Japan", "haruto.tanaka@example.com", "Male", "Haruto Tanaka", true },
                    { new Guid("b1a2c3d4-0012-4e5f-8a9b-1c2d3e4f5a12"), "1-2-3 Namba, Chuo-ku, Osaka 542-0076", new DateTime(2000, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6f1d8a2b-c3e5-4970-a2b4-18d9f5e3c708"), "Japan", "sakura.ito@example.com", "Female", "Sakura Ito", false },
                    { new Guid("b1a2c3d4-0013-4e5f-8a9b-1c2d3e4f5a13"), "Av. Paulista 1578, Sao Paulo, SP 01310-200", new DateTime(1989, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d3e5b7a9-0f21-4c86-9b3d-a7c1e48f2d09"), "Brazil", "mateus.silva@example.com", "Male", "Mateus Silva", true },
                    { new Guid("b1a2c3d4-0014-4e5f-8a9b-1c2d3e4f5a14"), "10 Orchard Road, #05-12, Singapore 238888", new DateTime(1992, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("94a0c6e2-8d7b-4153-bf6a-3e2d1c9b5a10"), "Singapore", "wei.tan@example.com", "Other", "Wei Tan", false },
                    { new Guid("b1a2c3d4-0015-4e5f-8a9b-1c2d3e4f5a15"), null, null, new Guid("3f2a9c1e-7b4d-4e8a-9a61-1c5d2e8f0a01"), "India", null, "Female", "Ananya Iyer", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
