using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EventEnvelopeTableRenamedToEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventEnvelopes",
                table: "EventEnvelopes");

            migrationBuilder.RenameTable(
                name: "EventEnvelopes",
                newName: "Events");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "EventEnvelopes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventEnvelopes",
                table: "EventEnvelopes",
                column: "Id");
        }
    }
}
