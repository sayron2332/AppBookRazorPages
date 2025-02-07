using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chapter02.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduserphotoproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b3f1b36-c87a-4b5d-932b-092febc92cf1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d40c703b-fa58-4d7d-86ba-b4e8cd39cb69", "97e8873f-dca7-4304-8205-ec24fe812772" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d40c703b-fa58-4d7d-86ba-b4e8cd39cb69");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "97e8873f-dca7-4304-8205-ec24fe812772");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "372b58b6-3d70-4481-80aa-85a584b6a9aa", null, "admin", "ADMIN" },
                    { "96e6fdbf-b1aa-49be-857c-c39d33e68eed", null, "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bb90daf0-3394-4ee8-859d-dc1373752597", 0, "17652b7f-3123-43b4-8adf-f83004775ef6", "xvtnxjgbyv@gmail.com", true, "default.jpg", false, null, "Nazar", "xvtnxjgbyv@gmail.com", "xvtnxjgbyv@gmail.com", "AQAAAAIAAYagAAAAEEjGm+/nfe04HNublIvaH+Bidu7X3Kqe8A9V8D7x1SnYH7qrS/xubwa8hzQXgbD1mQ==", "+380959348105", false, "", "Kurylovych", false, "xvtnxjgbyv@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "bb90daf0-3394-4ee8-859d-dc1373752597");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "bb90daf0-3394-4ee8-859d-dc1373752597");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "372b58b6-3d70-4481-80aa-85a584b6a9aa", "bb90daf0-3394-4ee8-859d-dc1373752597" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96e6fdbf-b1aa-49be-857c-c39d33e68eed");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "372b58b6-3d70-4481-80aa-85a584b6a9aa", "bb90daf0-3394-4ee8-859d-dc1373752597" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "372b58b6-3d70-4481-80aa-85a584b6a9aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb90daf0-3394-4ee8-859d-dc1373752597");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b3f1b36-c87a-4b5d-932b-092febc92cf1", null, "user", "USER" },
                    { "d40c703b-fa58-4d7d-86ba-b4e8cd39cb69", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "97e8873f-dca7-4304-8205-ec24fe812772", 0, "2a8c818a-904e-4942-aad2-accce52b53bd", "xvtnxjgbyv@gmail.com", true, false, null, "Nazar", "xvtnxjgbyv@gmail.com", "xvtnxjgbyv@gmail.com", "AQAAAAIAAYagAAAAEMjTLjxY3SuAVjuo60Rwe45134xIx+w+FuhbA9KNeQTkWkNDOYJCODaCtdRLi2g+9g==", "+380959348105", false, "", "Kurylovych", false, "xvtnxjgbyv@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "97e8873f-dca7-4304-8205-ec24fe812772");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "97e8873f-dca7-4304-8205-ec24fe812772");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d40c703b-fa58-4d7d-86ba-b4e8cd39cb69", "97e8873f-dca7-4304-8205-ec24fe812772" });
        }
    }
}
