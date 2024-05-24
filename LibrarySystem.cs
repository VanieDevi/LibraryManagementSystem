using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class LibrarySystem
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<User> Users { get; set; } = new List<User>();

        private const int MaxBorrowLimit = 5;

        public List<Book> SearchBooks(string query)
        {
            return Books.Where(book => book.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                       book.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Book ViewBookDetails(int bookId)
        {
            return Books.FirstOrDefault(book => book.BookId == bookId);
        }

        public void BorrowBook(int bookId, int userId)
        {
            var book = Books.FirstOrDefault(b => b.BookId == bookId);
            var user = Users.FirstOrDefault(u => u.UserId == userId);

            if (book == null) throw new InvalidOperationException("Book not found.");
            if (user == null) throw new InvalidOperationException("User not found.");
            if (user.BorrowedBooks.Count >= MaxBorrowLimit) throw new InvalidOperationException("User has reached the maximum borrow limit.");
            if (book.AvailableCopies <= 0) throw new InvalidOperationException("No available copies of the book.");

            book.AvailableCopies--;
            user.BorrowedBooks.Add(new BorrowedBook { Book = book, User = user, DueDate = DateTime.Now.AddDays(14) });
        }

        public void ReturnBook(int bookId, int userId)
        {
            var user = Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) throw new InvalidOperationException("User not found.");

            var borrowedBook = user.BorrowedBooks.FirstOrDefault(bb => bb.Book.BookId == bookId);
            if (borrowedBook == null) throw new InvalidOperationException("Book not borrowed by this user.");

            borrowedBook.Book.AvailableCopies++;
            user.BorrowedBooks.Remove(borrowedBook);
        }


        public void RenewBook(int bookId, int userId)
        {
            var user = Users.FirstOrDefault(u => u.UserId == userId);
            var borrowedBook = user?.BorrowedBooks.FirstOrDefault(bb => bb.Book.BookId == bookId);

            if (borrowedBook != null)
            {
                borrowedBook.DueDate = DateTime.Now.AddDays(14);
            }
        }

        public List<Book> GetAllBooks()
        {
            return Books;
        }

        public List<BorrowedBook> GetAllBorrowedBooks()
        {
            return Users.SelectMany(u => u.BorrowedBooks).ToList();
        }

        public List<User> GetAllUsersWithBorrowedBooks()
        {
            return Users.Where(u => u.BorrowedBooks.Any()).ToList();
        }
    }
}
