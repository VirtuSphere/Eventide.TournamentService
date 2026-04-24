using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventide.TournamentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantIdsColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParticipantIds",
                table: "Tournaments",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantIds",
                table: "Tournaments");
        }
    }
}
