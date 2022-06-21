using AutoMapper;
using BooksApi.Model;
using BooksApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksCollectionController : ControllerBase
    {
        private readonly IBookRepository _booksRepository;
        private readonly IMapper _mapper;
        public BooksCollectionController(IBookRepository bookRepository, IMapper mapper)
        {
            _booksRepository = bookRepository??
                throw new ArgumentNullException(nameof(bookRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        //  simple interaxtion and maping can solve the issuee of bulk Inserrt
        public async Task<IActionResult> CreateBookCollection(
            [FromBody] IEnumerable<BookForCreation> bookCollection)
        {
            var bookEntities = _mapper.Map<IEnumerable<Entities.Book>>(bookCollection);
            foreach (var bookEntity in bookEntities)
            {
                _booksRepository.AddBook(bookEntity);
            }
            await _booksRepository.SaveChangesAsync();
            var booksToReturn = await _booksRepository.
                GetBooksAsync(bookEntities.Select(b => b.Id).ToList());
            var booksIds = string.Join(",", booksToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetBooksCollection", new { booksIds}, booksToReturn);
        }

        // since we have created the Collection of resource we need to get the 
        // resources as well

        //api/booksColection/ {id1, id2}

        // the binding between the Ienumberable of Guids and the bookId in the route is quite tough
        // so we need a custome model binder for this sTUFF
        [HttpGet("({bookIds})", Name ="GetBooksCollection")]
        public async Task<IActionResult> GetBookCollection(
            [ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> bookIds)
        {
            var bookEntities = await _booksRepository.GetBooksAsync(bookIds);
            if (bookIds.Count() !=bookEntities.Count())
            {
                return NotFound();
            }
            return Ok(bookEntities); 
        }
    }
}
