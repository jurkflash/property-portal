using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSsoFieldsToParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SsoFailedMessage",
                table: "Parties",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SsoStatus",
                table: "Parties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SsoUserId",
                table: "Parties",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SsoFailedMessage",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "SsoStatus",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "SsoUserId",
                table: "Parties");
        }
    }
}
