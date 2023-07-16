using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();
        public bool AddBook(Book book);
        public bool UpdateBook(Book book);
        public bool DeleteBook(Guid bookId);
        public Book GetBookById(Guid bookId);
    }
}
