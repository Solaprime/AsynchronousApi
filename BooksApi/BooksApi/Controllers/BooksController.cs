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
    public class BooksController : ControllerBase
    {
        private IBookRepository _bookRepository;
        private IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ??
                throw new ArgumentNullException(nameof(bookRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntites =  await _bookRepository.GetBooksAsync();
            return Ok(_mapper.Map<Model.Book>(bookEntites));
            //return Ok(bookEntites);
        }
        [HttpGet("{id}", Name ="GetBook")]
         public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(_mapper.Map<Model.Book>(book));
            }
        }

         [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]BookForCreation book)
        {
            var bookEntity = _mapper.Map<Entities.Book>(book);
            _bookRepository.AddBook(bookEntity);
            await _bookRepository.SaveChangesAsync();
            return CreatedAtRoute("GetBook", new { id  = bookEntity.Id}, bookEntity);
        }
    }
}
// we make our code asynchronous in the Api, Repository(Efcore persistence) Level