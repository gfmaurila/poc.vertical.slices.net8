using Common.Net8.Enumerado;
using Dapper;
using System.Data;

namespace Common.Net8.Filter.QueryStore;


public static class OracleDynamicParametersFilters
{
    /// <summary>
    /// Constrói dinamicamente uma cláusula SQL de filtro e adiciona parâmetros à consulta.
    /// </summary>
    /// <param name="param">A coleção DynamicParameters para adicionar os parâmetros.</param>
    /// <param name="name">O nome da coluna a ser filtrada.</param>
    /// <param name="obj">O valor a ser filtrado. Se for nulo ou um array vazio, o método retorna sem adicionar qualquer filtro.</param>
    /// <param name="query">A string de consulta SQL que será construída dinamicamente.</param>
    /// <param name="filterType">O tipo de filtro (AND ou OR).</param>
    /// <param name="operadorType">O operador a ser usado no filtro (Igual, Diferente, etc.).</param>
    /*
     Exemplo:.

    public async Task<List<ProdutoDto>> ObterProdutosAsync(ProdutoRequest request)
    {
        var param = new DynamicParameters();
        var query = "SELECT * FROM Produtos WHERE 1=1";

        QueryBuilderAsync(param, "CategoriaId", request.CategoriaId, ref query);
        QueryBuilderAsync(param, "Preco", request.PrecoMinimo, ref query, QueryStoreFilterType.And, QueryStoreOperador.MaiorOuIgual);
        QueryBuilderAsync(param, "Preco", request.PrecoMaximo, ref query, QueryStoreFilterType.And, QueryStoreOperador.MenorOuIgual);

        query += " ORDER BY Nome";

        using (var connection = new SqlConnection(_connectionString))
        {
            var produtos = await connection.QueryAsync<ProdutoDto>(query, param);
            return produtos.ToList();
        }
    }
     */
    public static void QueryBuilderAsync(DynamicParameters param, string name, object obj, ref string query,
        QueryStoreFilterType filterType = QueryStoreFilterType.And, QueryStoreOperador operadorType = QueryStoreOperador.Igual)
    {
        if (obj == null)
            return;

        if (obj is Array { Length: 0 })
            return;

        name = name.Trim();
        var tipoFiltro = filterType == QueryStoreFilterType.And ? "AND" : "OR";
        var operador = GetOperador(operadorType);
        var parametroNome = $":PR_{name.Split('.').Last()}";
        var columnConverter = ColumnTypeConverter(GetTypeCode(obj), name);

        query += $" {tipoFiltro} {columnConverter} {operador} {UpperParametroValue(obj, parametroNome)}";

        AddParam(param, parametroNome, obj);
    }

    /// <summary>
    /// Constrói dinamicamente uma cláusula SQL BETWEEN e adiciona parâmetros à consulta.
    /// </summary>
    /// <param name="param">A coleção DynamicParameters para adicionar os parâmetros.</param>
    /// <param name="name">O nome da coluna a ser filtrada.</param>
    /// <param name="objInicial">O valor inicial do intervalo. Se for nulo, o método retorna sem adicionar qualquer filtro.</param>
    /// <param name="objFinal">O valor final do intervalo. Se for nulo, o método retorna sem adicionar qualquer filtro.</param>
    /// <param name="query">A string de consulta SQL que será construída dinamicamente.</param>
    /// <param name="filterType">O tipo de filtro (AND ou OR).</param>
    /// <param name="isColumnConverter">Indica se o nome da coluna deve ser convertido para o tipo SQL correspondente.</param>
    /*
     Exemplo 
    public async Task<List<TransacaoDto>> ObterTransacoesAsync(TransacaoRequest request)
    {
        var param = new DynamicParameters();
        var query = "SELECT * FROM Transacoes WHERE 1=1";

        QueryBuilderBetweenAsync(param, "DataTransacao", request.DataInicio, request.DataFim, ref query);

        query += " ORDER BY DataTransacao";

        using (var connection = new SqlConnection(_connectionString))
        {
            var transacoes = await connection.QueryAsync<TransacaoDto>(query, param);
            return transacoes.ToList();
        }
    }
     */
    public static void QueryBuilderBetweenAsync(DynamicParameters param, string name, object objInicial, object objFinal, ref string query,
        QueryStoreFilterType filterType = QueryStoreFilterType.And, bool isColumnConverter = true)
    {
        if (objInicial == null || objFinal == null)
            return;

        if (objInicial.GetType() != objFinal.GetType())
            return;

        objInicial = AplicarDateFilterDefault(objInicial, ref objFinal);

        name = name.Trim();
        var tipoFiltro = filterType == QueryStoreFilterType.And ? "AND" : "OR";
        var parametroNome = $":PR_{name.Split('.').Last()}";
        var segundoParametro = $":PR_{name.Split('.').Last()}_FINAL";
        var columnConverter = isColumnConverter ? ColumnTypeConverter(GetTypeCode(objInicial), name) : name;
        query += $" {tipoFiltro} {columnConverter} BETWEEN TO_DATE({parametroNome}, 'DD/MM/YYYY HH24:MI:SS') AND TO_DATE({segundoParametro}, 'DD/MM/YYYY HH24:MI:SS') ";

        AddParam(param, parametroNome, Convert.ToDateTime(objInicial).ToString("dd/MM/yyyy HH:mm:ss"));
        AddParam(param, segundoParametro, Convert.ToDateTime(objFinal).ToString("dd/MM/yyyy HH:mm:ss"));
    }

    /// <summary>
    /// Adiciona uma cláusula de paginação à consulta SQL.
    /// </summary>
    /// <param name="query">A string de consulta SQL que será construída dinamicamente.</param>
    /// <param name="pagina">O número da página. O valor padrão é 1.</param>
    /// <param name="itensPorPagina">O número de itens por página. O valor padrão é 15, e o valor máximo é 50.</param>
    /// <returns>Uma tupla contendo o número da página e o número de itens por página ajustados.</returns>
    /*
     Exemplo 
    public async Task<List<ProdutoDto>> ObterProdutosPaginadosAsync(uint pagina, uint itensPorPagina)
    {
        var query = "SELECT * FROM Produtos WHERE 1=1";
    
        // Adiciona a cláusula de paginação à consulta
        var (paginaAjustada, itensPorPaginaAjustados) = AddPaginacaoAsync(ref query, pagina, itensPorPagina);

        using (var connection = new SqlConnection(_connectionString))
        {
            var produtos = await connection.QueryAsync<ProdutoDto>(query);
            return produtos.ToList();
        }
    }
     */
    public static (uint, uint) AddPaginacaoAsync(ref string query, uint pagina = 1, uint itensPorPagina = 15)
    {
        pagina = pagina == 0 ? 1 : pagina;
        itensPorPagina = itensPorPagina == 0 ? 15 : itensPorPagina > 50 ? 50 : itensPorPagina;

        var offset = (pagina - 1) * itensPorPagina;
        query += $" OFFSET {offset} ROWS FETCH NEXT {itensPorPagina} ROWS ONLY";

        return (pagina, itensPorPagina);
    }

    /// <summary>
    /// Gera uma cláusula de paginação SQL.
    /// </summary>
    /// <param name="paginaAtual">O número da página atual.</param>
    /// <param name="itensPorPagina">O número de itens por página.</param>
    /// <returns>Uma string contendo a cláusula de paginação SQL.</returns>
    /*
        public async Task<List<ProdutoDto>> ObterProdutosPaginadosAsync(uint pagina, uint itensPorPagina)
        {
            var query = "SELECT * FROM Produtos WHERE 1=1";
    
            // Gera a cláusula de paginação
            var paginacao = DynamicParametersFilters.GetPaginacao(pagina, itensPorPagina);
            query += paginacao;

            using (var connection = new SqlConnection(_connectionString))
            {
                var produtos = await connection.QueryAsync<ProdutoDto>(query);
                return produtos.ToList();
            }
        }
     */
    public static string GetPaginacao(uint paginaAtual, uint itensPorPagina)
    {
        var queryFilter = string.Empty;
        OracleDynamicParametersFilters.AddPaginacaoAsync(ref queryFilter, paginaAtual, itensPorPagina);
        return queryFilter;
    }

    /// <summary>
    /// Constrói dinamicamente uma cláusula SQL BETWEEN e adiciona parâmetros à consulta.
    /// </summary>
    /// <param name="param">A coleção DynamicParameters para adicionar os parâmetros.</param>
    /// <param name="name">O nome da coluna a ser filtrada.</param>
    /// <param name="objInicial">O valor inicial do intervalo. Se for nulo, o método retorna sem adicionar qualquer filtro.</param>
    /// <param name="objFinal">O valor final do intervalo. Se for nulo, o método retorna sem adicionar qualquer filtro.</param>
    /// <param name="query">A string de consulta SQL que será construída dinamicamente.</param>
    /// <param name="filterType">O tipo de filtro (AND ou OR).</param>
    /// <param name="isColumnConverter">Indica se o nome da coluna deve ser convertido para o tipo SQL correspondente.</param>
    private static object AplicarDateFilterDefault(object objInicial, ref object objFinal)
    {
        if (objInicial is DateTime)
        {
            if (objInicial.Equals(default(DateTime)))
                objInicial = ((DateTime)objFinal).AddMonths(-1);

            if (objFinal.Equals(default(DateTime)))
                objFinal = ((DateTime)objInicial).AddMonths(1);
        }

        return objInicial;
    }

    /// <summary>
    /// Adiciona um parâmetro à coleção DynamicParameters.
    /// </summary>
    /// <param name="param">A coleção DynamicParameters.</param>
    /// <param name="parametroNome">O nome do parâmetro.</param>
    /// <param name="obj">O valor do parâmetro. Se for um array, o tipo será tratado como um array.</param>
    private static void AddParam(DynamicParameters param, string parametroNome, object obj)
    {
        if (obj.GetType().IsArray)
            param.Add(parametroNome, obj, direction: ParameterDirection.Input);
        else
            param.Add(parametroNome, obj, direction: ParameterDirection.Input, dbType: GetDbType(obj));

    }

    /// <summary>
    /// Converte o tipo de uma coluna para um tipo específico no SQL, dependendo do tipo de dado do objeto.
    /// </summary>
    /// <param name="column">O código do tipo da coluna.</param>
    /// <param name="name">O nome da coluna.</param>
    /// <returns>Uma string representando a conversão da coluna no SQL.</returns>
    private static string ColumnTypeConverter(TypeCode column, string name)
    {
        return column switch
        {
            TypeCode.String => $"UPPER({name})",
            TypeCode.DateTime => $"CAST({name} as date)",
            _ => name
        };
    }

    /// <summary>
    /// Verifica se o objeto é uma string e retorna o nome do parâmetro envolto em uma chamada de função SQL UPPER,
    /// caso contrário, retorna o nome do parâmetro sem alterações.
    /// </summary>
    /// <param name="obj">O objeto a ser verificado.</param>
    /// <param name="parametroNome">O nome do parâmetro SQL.</param>
    /// <returns>O nome do parâmetro, possivelmente envolto em uma chamada de função SQL UPPER.</returns>
    private static string UpperParametroValue(object obj, string parametroNome)
        => obj is string ? "UPPER(" + parametroNome + ")" : parametroNome;

    /// <summary>
    /// Converte um valor enumerado QueryStoreOperador em um operador SQL correspondente.
    /// </summary>
    /// <param name="operador">O valor enumerado QueryStoreOperador.</param>
    /// <returns>Uma string representando o operador SQL correspondente.</returns>
    private static string GetOperador(QueryStoreOperador operador)
    {
        return operador switch
        {
            QueryStoreOperador.Contem => "LIKE",
            QueryStoreOperador.Diferente => "<>",
            QueryStoreOperador.Maior => ">",
            QueryStoreOperador.Menor => "<",
            QueryStoreOperador.MaiorIgual => ">=",
            QueryStoreOperador.MenorIgual => "<=",
            QueryStoreOperador.In => "IN",
            QueryStoreOperador.Igual => "=",
            _ => "="
        };
    }

    /// <summary>
    /// Retorna o código do tipo de um objeto.
    /// </summary>
    /// <param name="obj">O objeto para o qual o código do tipo será retornado.</param>
    /// <returns>O código do tipo do objeto.</returns>
    private static TypeCode GetTypeCode(object obj) => Type.GetTypeCode(obj.GetType());

    /// <summary>
    /// Determina o tipo de banco de dados (DbType) apropriado para um valor fornecido.
    /// </summary>
    /// <param name="value">O valor para o qual o tipo de banco de dados será determinado.</param>
    /// <param name="typeCode">O código do tipo do valor. Se não fornecido, será determinado automaticamente.</param>
    /// <returns>O tipo de banco de dados (DbType) correspondente.</returns>
    /// <exception cref="ArgumentException">Lançada quando um tipo de valor desconhecido é fornecido.</exception>
    public static DbType GetDbType(object value, TypeCode typeCode = TypeCode.Empty)
    {
        if (value is null)
            return DbType.Object;

        var type = value.GetType();
        if (typeCode == TypeCode.Empty)
            typeCode = GetTypeCode(value);

        switch (typeCode)
        {
            case TypeCode.Boolean:
                return DbType.Boolean;

            case TypeCode.Byte:
                return DbType.Byte;

            case TypeCode.Char:
                return DbType.StringFixedLength;

            case TypeCode.DateTime:
                return DbType.DateTime;

            case TypeCode.Decimal:
                return DbType.Decimal;

            case TypeCode.Double:
                return DbType.Double;

            case TypeCode.Int16:
                return DbType.Int16;

            case TypeCode.Int32:
                return DbType.Int32;

            case TypeCode.Int64:
                return DbType.Int64;

            case TypeCode.Object:
                if (type.IsArray)
                {
                    // type.GetElementType()
                    var ty = Type.GetTypeCode(type.GetElementType());
                    return GetDbType(value, ty);
                }
                if (value is Guid)
                    return DbType.Guid;
                if (value is byte[])
                    return DbType.Binary;
                break;

            case TypeCode.SByte:
                return DbType.SByte;

            case TypeCode.Single:
                return DbType.Single;

            case TypeCode.String:
                return DbType.String;

            case TypeCode.UInt16:
                return DbType.UInt16;

            case TypeCode.UInt32:
                return DbType.UInt32;

            case TypeCode.UInt64:
                return DbType.UInt64;
        }

        throw new ArgumentException($"Tipo de valor desconhecido {typeCode}");
    }

    /// <summary>
    /// Verifica se um objeto é de um tipo numérico.
    /// </summary>
    /// <param name="obj">O objeto a ser verificado.</param>
    /// <returns>True se o objeto for de um tipo numérico, caso contrário, False.</returns>
    public static bool ObjectIsNumeric(object obj)
    {
        return obj is int or long or decimal or double or float;
    }
}