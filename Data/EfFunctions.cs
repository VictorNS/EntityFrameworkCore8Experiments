using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;

namespace Data;

public static class EfFunctions
{
	/// <summary>
	/// CONVERT(NVARCHAR(15), 106)
	/// </summary>
	/// <param name="d">Any DateTime column</param>
	/// <returns></returns>
	/// <exception cref="NotSupportedException"></exception>
	public static string GetDayShortMonthYear(DateTime d) => d.ToString("dd MMM yyyy");

	/// <summary>
	/// FORMAT(value, 'dd MMMM yyyy')
	/// </summary>
	/// <param name="d">Any DateTime column</param>
	/// <returns></returns>
	/// <exception cref="NotSupportedException"></exception>
	public static string GetDayFullMonthYear(DateTime d) => d.ToString("dd MMMM yyyy");


	internal static void FunctionMapping(ModelBuilder modelBuilder)
	{
		modelBuilder.BuildFunction<DateTime>(
			nameof(GetDayShortMonthYear),
			arg => new SqlFunctionExpression(
				functionName: "CONVERT",
				arguments:
				[
					new SqlFragmentExpression("NVARCHAR(15)"),
					arg,
					new SqlFragmentExpression("106")
				],
				nullable: true,
				argumentsPropagateNullability: [false, true, false],
				type: typeof(string),
				typeMapping: null
			)
		);

		modelBuilder.BuildFunction<DateTime>(
			nameof(GetDayFullMonthYear),
			arg => new SqlFunctionExpression(
				functionName: "FORMAT",
				arguments:
				[
					arg,
					new SqlFragmentExpression("'dd MMMM yyyy'")
				],
				nullable: true,
				argumentsPropagateNullability: [true, false],
				type: typeof(string),
				typeMapping: null
			)
		);
	}

	private static void BuildFunction<T>(this ModelBuilder modelBuilder, string methodName, Func<SqlExpression, SqlFunctionExpression> func)
	{
		var dbFunction = typeof(EfFunctions).GetMethod(methodName, [typeof(T)])
			?? throw new ArgumentException($"{methodName} is expected in EfHelpers");
		modelBuilder.HasDbFunction(dbFunction).HasTranslation(args => func(args[0]));
	}
}
