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
        public ActionResult<List<BookApiModel>> Get()
        {
            return _bookRepository.GetBooks();
        }

        [HttpGet("{id}")]
        public ActionResult<BookApiModel> GetBook(string id) 
        {
            var book = _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult CreateBook(BookApiModel book)
        {
            try
            {
                _bookRepository.SaveBook(book);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }        
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBook(BookApiModel book)
        {
            try
            {
                _bookRepository.SaveBook(book);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteBook(string id)
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
