using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "BorrowRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordExpiry",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Email", "ResetPasswordExpiry", "ResetPasswordToken" },
                values: new object[] { "nghethuat24tren7@gmail.com", null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Email", "ResetPasswordExpiry", "ResetPasswordToken" },
                values: new object[] { "viethdhe161234@fpt.edu.vn", null, null });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "Email", "ResetPasswordExpiry", "ResetPasswordToken" },
                values: new object[] { "dicket60@gmail.com", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "BorrowRequests");

            migrationBuilder.DropColumn(
                name: "ResetPasswordExpiry",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Email",
                value: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Email",
                value: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Email",
                value: "");
        }
    }
}
