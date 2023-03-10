using BookRestApi.Contracts;
using BookRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return _bookRepository.GetBooks();
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id) 
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            try
            {
                _bookRepository.AddBook(book);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }        
        }

        [HttpPut]
        public ActionResult UpdateBook(Book book)
        {
            try
            {
                _bookRepository.UpdateBook(book);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                _bookRepository.DeleteBook(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
