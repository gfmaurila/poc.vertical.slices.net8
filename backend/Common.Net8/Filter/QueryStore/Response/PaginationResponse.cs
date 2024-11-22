namespace Common.Net8.Filter.QueryStore.Response;

/// <summary>
/// Classe genérica para encapsular a resposta de paginação.
/// </summary>
/// <typeparam name="T">O tipo de itens na resposta de paginação.</typeparam>
public class PaginationResponse<T>
{
    /// <summary>
    /// A página atual.
    /// </summary>
    public uint CurrentPage { get; set; }

    /// <summary>
    /// O número de itens por página.
    /// </summary>
    public uint ItemsPerPage { get; set; }

    /// <summary>
    /// O número total de itens.
    /// </summary>
    public uint TotalItems { get; set; }

    /// <summary>
    /// A lista de itens na página atual.
    /// </summary>
    public List<T> Items { get; set; }

    /// <summary>
    /// A lista de filtros aplicados na consulta.
    /// </summary>
    public Dictionary<string, object> Filters { get; set; }

    /// <summary>
    /// Construtor da classe PaginationResponse.
    /// </summary>
    /// <param name="currentPage">A página atual.</param>
    /// <param name="itemsPerPage">O número de itens por página.</param>
    /// <param name="totalItems">O número total de itens.</param>
    /// <param name="items">A lista de itens na página atual.</param>
    /// <param name="filters">A lista de filtros aplicados na consulta.</param>
    public PaginationResponse(uint currentPage, uint itemsPerPage, uint totalItems, List<T> items, Dictionary<string, object> filters)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        Items = items;
        Filters = filters;
    }
}


/*
     Exemplo de ultilização 

    public async Task<PaginationResponse<ProductDto>> GetPaginatedProductsAsync(uint page, uint itemsPerPage, string productName = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var filters = new Dictionary<string, object>();
            var param = new DynamicParameters();
            var query = "SELECT * FROM Products WHERE 1=1";

            // Add search filters
            if (!string.IsNullOrEmpty(productName))
            {
                query += " AND UPPER(Name) LIKE UPPER(@productName)";
                param.Add("@productName", $"%{productName}%");
                filters.Add("productName", productName);
            }

            if (minPrice.HasValue)
            {
                query += " AND Price >= @minPrice";
                param.Add("@minPrice", minPrice.Value);
                filters.Add("minPrice", minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query += " AND Price <= @maxPrice";
                param.Add("@maxPrice", maxPrice.Value);
                filters.Add("maxPrice", maxPrice.Value);
            }

            // Get the total number of items
            var totalItems = await connection.ExecuteScalarAsync<uint>($"SELECT COUNT(*) FROM ({query}) AS CountQuery", param);

            // Calculate offset and adjust query with pagination
            var (adjustedPage, adjustedItemsPerPage) = AddPaginationAsync(ref query, page, itemsPerPage);

            // Get paginated items
            var products = (await connection.QueryAsync<ProductDto>(query, param)).ToList();

            return new PaginationResponse<ProductDto>(adjustedPage, adjustedItemsPerPage, totalItems, products, filters);
        }
    }

    public async Task<IActionResult> GetPaginatedProducts(uint page = 1, uint itemsPerPage = 15, string productName = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        var result = await _productService.GetPaginatedProductsAsync(page, itemsPerPage, productName, minPrice, maxPrice);
        return Ok(result);
    }


     */

