using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventide.TournamentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDescriptionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantIds",
                table: "Tournaments");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tournaments",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tournaments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ParticipantIds",
                table: "Tournaments",
                type: "uuid[]",
                nullable: false);
        }
    }
}
