//using BusinessLogic.Interfaces;
//using DataAccess.DTOs;
//using DataAccess.Entities;
//using DataAccess.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace BusinessLogic.BusinessLogics
//{
//    public class BookLogic : IBookLogic
//    {
//        private readonly IBookRepository bookRepository;

//        public BookLogic(IBookRepository _bookRepository)
//        {
//            this.bookRepository = _bookRepository;
//        }
//        public ServiceResult<List<BookDTO>> GetAllBooks()
//        {
//           List<Book> books = bookRepository.GetAllBooks();

//            return new ServiceResult<List<BookDTO>>(MapBooksToDTOs(books), 200, "returning books");
             
//        }

//        public ServiceResult<bool> AddBook(BookDTO bookDTO)
//        {
//            var book = MapDTOToBook(bookDTO);
//           bool isSuccessful = bookRepository.AddBook(book);
//            if (isSuccessful)
//            {
//                return new ServiceResult<bool>(true, 200, "Added successfully");
//            }
//            return new ServiceResult<bool>(true, 400, "Not Added successfully");
//        }

//        public ServiceResult<bool> UpdateBook(BookDTO bookDTO)
//        {
//            var book = MapDTOToBook(bookDTO);
//            bookRepository.UpdateBook(book);

//            bool isSuccessful = bookRepository.UpdateBook(book);
//            if (isSuccessful)
//            {
//                return new ServiceResult<bool>(true, 200, "Updated successfully");
//            }
//            return new ServiceResult<bool>(true, 400, "Not Updated successfully");
//        }

//        public ServiceResult<bool> DeleteBook(Guid bookId)
//        {

//            bool isSuccessful = bookRepository.DeleteBook(bookId);
//            if (isSuccessful)
//            {
//                return new ServiceResult<bool>(true, 200, "Delete successfully");
//            }
//            return new ServiceResult<bool>(true, 400, "Not Deleted ");
//        }

//        private List<BookDTO> MapBooksToDTOs(List<Book> books)
//        {
//            return books.Select(book => new BookDTO
//            {
//                Id = book.Id,
//                Title = book.Title,
//                Author = book.Author,
//                Description = book.Description,
//                Price = book.Price,
//                Quantity = book.Quantity,
//                Category = book.Category,
//                PublicationDate = book.PublicationDate
//            }).ToList();
//        }

//        private Book MapDTOToBook(BookDTO bookDTO)
//        {
//            return new Book
//            {
//                Id = bookDTO.Id,
//                Title = bookDTO.Title,
//                Author = bookDTO.Author,
//                Description = bookDTO.Description,
//                Price = bookDTO.Price,
//                Quantity = bookDTO.Quantity,
//                Category = bookDTO.Category,
//                PublicationDate = bookDTO.PublicationDate
//            };
//        }
//    }
//}
