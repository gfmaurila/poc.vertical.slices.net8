using Common.Net8.Filter.QueryStore;
using Dapper;

namespace Common.Net8.Tests.Filter.QueryStore;

public class OracleDynamicParametersFiltersTests
{
    [Fact]
    public void QueryBuilderAsync_AddsCorrectFilter()
    {
        // Arrange
        var parameters = new DynamicParameters();
        var query = "SELECT * FROM Table WHERE 1=1";
        var columnName = "Name";
        var value = "Test";

        // Act
        OracleDynamicParametersFilters.QueryBuilderAsync(parameters, columnName, value, ref query);

        // Assert
        Assert.Contains("AND UPPER(Name) = UPPER(:PR_Name)", query);
        Assert.True(parameters.ParameterNames.Contains("PR_Name"));
        Assert.Equal("Test", parameters.Get<string>("PR_Name"));
    }

    [Fact]
    public void QueryBuilderBetweenAsync_AddsCorrectBetweenFilter()
    {
        // Arrange
        var parameters = new DynamicParameters();
        var query = "SELECT * FROM Table WHERE 1=1";
        var columnName = "DateColumn";
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2020, 1, 31);

        // Act
        OracleDynamicParametersFilters.QueryBuilderBetweenAsync(parameters, columnName, startDate, endDate, ref query);

        // Assert
        Assert.Contains("AND CAST(DateColumn as date) BETWEEN TO_DATE(:PR_DateColumn, 'DD/MM/YYYY HH24:MI:SS') AND TO_DATE(:PR_DateColumn_FINAL, 'DD/MM/YYYY HH24:MI:SS')", query);
        Assert.True(parameters.ParameterNames.Contains("PR_DateColumn"));
        Assert.True(parameters.ParameterNames.Contains("PR_DateColumn_FINAL"));
        Assert.Equal("01/01/2020 00:00:00", parameters.Get<string>("PR_DateColumn"));
        Assert.Equal("31/01/2020 00:00:00", parameters.Get<string>("PR_DateColumn_FINAL"));
    }

    [Fact]
    public void AddPaginacaoAsync_AddsCorrectPagination()
    {
        // Arrange
        var query = "SELECT * FROM Products";
        uint page = 2;
        uint itemsPerPage = 10;

        // Act
        var (adjustedPage, adjustedItemsPerPage) = OracleDynamicParametersFilters.AddPaginacaoAsync(ref query, page, itemsPerPage);

        // Assert
        Assert.Equal(2u, adjustedPage);
        Assert.Equal(10u, adjustedItemsPerPage);
        Assert.Contains("OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", query);
    }
}
