using Common.Net8.Filter.QueryStore.Response;

namespace Common.Net8.Tests.Filter.QueryStore;

public class PaginationResponseTests
{
    [Fact]
    public void Constructor_InitializesAllProperties()
    {
        // Arrange
        uint currentPage = 1;
        uint itemsPerPage = 10;
        uint totalItems = 100;
        var items = new List<string> { "item1", "item2" };
        var filters = new Dictionary<string, object> { { "filter1", "value1" } };

        // Act
        var response = new PaginationResponse<string>(currentPage, itemsPerPage, totalItems, items, filters);

        // Assert
        Assert.Equal(currentPage, response.CurrentPage);
        Assert.Equal(itemsPerPage, response.ItemsPerPage);
        Assert.Equal(totalItems, response.TotalItems);
        Assert.Equal(items, response.Items);
        Assert.Equal(filters, response.Filters);
    }

    [Fact]
    public void PaginationProperties_AreMutable()
    {
        // Arrange
        var response = new PaginationResponse<string>(1, 10, 100, new List<string> { "item1" }, new Dictionary<string, object>());

        // Act
        response.CurrentPage = 2;
        response.ItemsPerPage = 20;
        response.TotalItems = 200;
        response.Items.Add("item2");
        response.Filters.Add("newFilter", "newValue");

        // Assert
        Assert.Contains("item2", response.Items);
        Assert.True(response.Filters.ContainsKey("newFilter"));
        Assert.Equal("newValue", response.Filters["newFilter"]);
    }
}
