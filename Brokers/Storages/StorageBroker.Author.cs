using Dapper;
using library.Models;
using Microsoft.Data.Sqlite;
namespace library.Brokers.Storages;

/* 
 public int author_id { get; set; }
    public string name { get; set; }
 */
public partial class StorageBroker : IStorageBroker
{
    public async ValueTask InsertAuthorAsync(Author author)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("INSERT INTO Author(author_id,name) VALUES (@author_id, @name )", author);
    }

    public async ValueTask<List<Author>> SelectAllAuthorsAsync()
    {
        using var connection = CreateConnection();
        return (await connection.QueryAsync<Author>("SELECT * FROM Author")).ToList();
    }
    public async ValueTask<Author> SelectAuthorByIdAsync(int authorId)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Author WHERE author_id = @author_id", new { author_id = authorId });
    }
    public async ValueTask UpdateAuthorAsync(Author author)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("UPDATE Author SET name = @name WHERE author_id = @author_id", author);
    }
    public async ValueTask DeleteAuthorAsync(int authorId)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Author WHERE author_id = @author_id", new { author_id = authorId });
    }
}
