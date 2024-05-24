// See https://aka.ms/new-console-template for more information
using LibraryManagementSystem;

public class Program
{
    public static void Main()
    {
        LibrarySystem librarySystem = new LibrarySystem();

        // Add sample books into the library
        librarySystem.Books.Add(new Book { BookId = 1, Title = "Book1", Author = "Author A", PublicationDate = new DateTime(2020, 1, 1), AvailableCopies = 5, NumberOfCopies = 5 });
        librarySystem.Books.Add(new Book { BookId = 2, Title = "Book2", Author = "Author B", PublicationDate = new DateTime(2021, 2, 1), AvailableCopies = 3, NumberOfCopies = 3 });

        // Add sample users
        librarySystem.Users.Add(new User { UserId = 1, Name = "Vanie", EmailAddress = "vanie@example.com" });
        librarySystem.Users.Add(new User { UserId = 2, Name = "Abi", EmailAddress = "abi@example.com" });

        // Borrow a book
        librarySystem.BorrowBook(1, 1);

        // View all books
        var allBooks = librarySystem.GetAllBooks();
        foreach (var book in allBooks)
        {
            Console.WriteLine($"{book.Title} by {book.Author} - Available Copies: {book.AvailableCopies}");
        }

        // View borrowed books
        var borrowedBooks = librarySystem.GetAllBorrowedBooks();
        foreach (var borrowedBook in borrowedBooks)
        {
            Console.WriteLine($"{borrowedBook.User.Name} borrowed {borrowedBook.Book.Title} - Due Date: {borrowedBook.DueDate}");
        }
    }
}