using BooksApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
     public interface IBookRepository
    {
        //IEnumerable<Book> GetBooks();
        //Book GetBook(Guid Id);
        // this is the normal way pf working 
        // but we want to Dealwith Async and await

       Task<IEnumerable<Book>>GetBooksAsync();
        Task<Book> GetBookAsync(Guid Id);
    }
}

// the reason why our two method were not decorared with the async keyword is 
// recall an interface is a contract which makes our 2 method a contract

// using the async/Await keyword tells us how the method us Implemennted, which makes it an implementatin
//detail
