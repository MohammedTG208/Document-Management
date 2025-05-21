using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE PROCEDURE dbo.GetUserAndProfile 
            @userId INT 
        AS
        BEGIN 
            SELECT 
                u.Id AS UserId,
                u.UserName,
                u.UserRole,
                u.created_at AS UserCreatedAt,
                p.Id AS ProfileId,
                p.Email,
                p.PhoneNumber
            FROM Users u 
            JOIN Profiles p ON p.Id = u.Id 
            WHERE u.Id = @userId; 
        END
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.GetUserAndProfile");
        }
    }
}
