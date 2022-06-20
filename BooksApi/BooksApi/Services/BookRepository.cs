using BooksApi.Contexts;
using BooksApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    // we neec to implement IDisposable on our Repository 
    //so that when the repo is disposed of our context will be disposed as well
    public class BookRepository : IBookRepository, IDisposable
    {
        private BookContext _Context;
        public BookRepository(BookContext bookContext)
        {
            _Context = bookContext ?? throw new ArgumentNullException(nameof(bookContext));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_Context != null)
                {
                    _Context.Dispose();
                    _Context = null;
                }
            }
        }

        public async Task<Book> GetBookAsync(Guid Id)
        {
            return await _Context.Books.Include(b => b.Author)
                 .FirstOrDefaultAsync(b => b.Id == Id);

        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _Context.Books.Include(b=>b.Author).ToListAsync();
        }
    }
    
}
