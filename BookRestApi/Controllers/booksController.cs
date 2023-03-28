using BookRestApi.Contracts;
using BookRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public booksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public ActionResult<List<BookApiModel>> GetBookList([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                var books = _bookRepository.GetBookList(page, pageSize);
                return Ok(books);
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving books");
            }
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
        public ActionResult CreateBook([FromBody] BookApiModel book)
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
        public ActionResult UpdateBook(string id, [FromBody] BookApiModel book)
        {
            try
            {
                book.id = id;

                _bookRepository.SaveBook(book);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
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
