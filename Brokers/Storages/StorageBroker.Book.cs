using Dapper;
namespace Library.Brokers.Storages;
public partial class StorageBroker : IStorageBroker
{
    public async ValueTask InsertBookAsync(Book book)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("INSERT INTO Book(book_id,title) VALUES (@book_id, @title)",book);
    }
    public async ValueTask<List<Book>> SelectAllBooksAsync()
    {
        using var connection = CreateConnection();
        return (await connection.QueryAsync<Book>("SELECT * FROM Book")).ToList();
    }
    public async ValueTask<Book?> SelectBookByIdAsync(int book_id)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Book WHERE book_id = @book_id", new { book_id });
    }
    public async ValueTask UpdateBookAsync(Book book)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("UPDATE Book SET title = @title WHERE book_id = @book_id", book);
    }
    public async ValueTask DeleteBookAsync(int book_id)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Book WHERE book_id = @book_id", new {  book_id });
    }
}