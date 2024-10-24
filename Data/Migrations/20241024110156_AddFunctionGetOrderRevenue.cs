using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

/// <inheritdoc />
public partial class AddFunctionGetOrderRevenue : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
		migrationBuilder.Sql(@"
CREATE OR ALTER FUNCTION dbo.ufn_GetOrderRevenue(@orderId as int)
RETURNS DECIMAL(20, 4) AS
BEGIN
	DECLARE @result AS DECIMAL(20, 4);
	SELECT @result = SUM(Quantity * Price)
		FROM OrderItems
		WHERE OrderId = @orderId;
	RETURN @result;
END
");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
    {
		migrationBuilder.Sql(@"
DROP PROCEDURE IF EXISTS dbo.ufn_GetOrderRevenue
");
	}
}
