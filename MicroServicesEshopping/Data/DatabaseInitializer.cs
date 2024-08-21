using MicroServicesEshopping.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MicroServicesEshopping.Data
{
    public class DatabaseInitializer
    {
        private readonly IMongoDatabase _database;
        private readonly IOptions<MongoSettings> _mongoSettings;

        public DatabaseInitializer(IMongoDatabase database, IOptions<MongoSettings> mongoSettings)
        {
            _database = database;
            _mongoSettings = mongoSettings;
        }

        public void Initialize()
        {
            SeedBooksCollection();
        }

        private void SeedBooksCollection()
        {
            var booksCollection = _database.GetCollection<Book>("Books");

            // Check if the collection already has data
            var bookCount = booksCollection.CountDocuments(FilterDefinition<Book>.Empty);

            if (bookCount > 0)
            {
                return;
            }
                var books = new List<Book>
                {
                    new Book
    {
        BooksId = 1,
        Name = "The Lessons of History",
        Author = "Ariel Durant",
        Description = "The celebrated collection of essays compiling over 5000 years of history by two of the greatest thinkers of our time",
        Url = "https://th.bing.com/th/id/OIP.4SL2sufnPQjexf0kisGFFwHaLZ?w=186&h=287&c=7&r=0&o=5&pid=1.7",
        Price = 20.4,
        RentalPrice = 10.7,
        Language = "English",
        QuantityAvailable = 14
    },
    new Book
    {
        BooksId = 2,
        Name = "Sapiens: A Brief History of Humankind",
        Author = "Yuval Noah Harari",
        Description = "A groundbreaking narrative of humanity’s creation and evolution that explores how history has shaped human societies and individual lives.",
        Url = "https://m.media-amazon.com/images/I/91+SKkHEF+L._AC_SY679_.jpg",
        Price = 22.5,
        RentalPrice = 12.0,
        Language = "English",
        QuantityAvailable = 20
    },
    new Book
    {
        BooksId = 3,
        Name = "1984",
        Author = "George Orwell",
        Description = "A dystopian novel set in a totalitarian society under the watchful eye of Big Brother, exploring themes of surveillance, control, and individual freedom.",
        Url = "https://m.media-amazon.com/images/I/71kxa1-0FBL._AC_SY679_.jpg",
        Price = 15.0,
        RentalPrice = 8.0,
        Language = "English",
        QuantityAvailable = 18
    },
    new Book
    {
        BooksId = 4,
        Name = "The Catcher in the Rye",
        Author = "J.D. Salinger",
        Description = "A classic novel that follows the story of Holden Caulfield, a disenchanted teenager navigating the challenges of adolescence and identity.",
        Url = "https://m.media-amazon.com/images/I/81OthjkJBuL._AC_SY679_.jpg",
        Price = 18.0,
        RentalPrice = 9.5,
        Language = "English",
        QuantityAvailable = 12
    },
    new Book
    {
        BooksId = 5,
        Name = "Educated: A Memoir",
        Author = "Tara Westover",
        Description = "A memoir about a woman who grows up in a strict and abusive household in rural Idaho but eventually escapes to learn about the wider world through education.",
        Url = "https://m.media-amazon.com/images/I/81Wojh8b3bL._AC_SY679_.jpg",
        Price = 19.0,
        RentalPrice = 10.0,
        Language = "English",
        QuantityAvailable = 15
    },
    new Book
    {
        BooksId = 6,
        Name = "To Kill a Mockingbird",
        Author = "Harper Lee",
        Description = "A Pulitzer Prize-winning novel set in the American South during the 1930s, dealing with serious issues of race and morality through the eyes of a young girl.",
        Url = "https://m.media-amazon.com/images/I/81O4w1bHhtL._AC_SY679_.jpg",
        Price = 17.5,
        RentalPrice = 9.0,
        Language = "English",
        QuantityAvailable = 25
    },
        new Book
    {
        BooksId = 7,
        Name = "Sapiens: A Brief History of Humankind",
        Author = "Yuval Noah Harari",
        Description = "An exploration of the history of humankind, from the emergence of Homo sapiens in the Stone Age to the modern age.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/71s2tpnd8cL.jpg",
        Price = 18.0,
        RentalPrice = 9.0,
        Language = "English",
        QuantityAvailable = 20
    },
    new Book
    {
        BooksId = 8,
        Name = "Educated",
        Author = "Tara Westover",
        Description = "A memoir about a woman who grows up in a strict and abusive household in rural Idaho but escapes to learn about the wider world through education.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/81Wojaf05yL.jpg",
        Price = 16.5,
        RentalPrice = 8.5,
        Language = "English",
        QuantityAvailable = 15
    },
    new Book
    {
        BooksId = 9,
        Name = "The Silent Patient",
        Author = "Alex Michaelides",
        Description = "A psychological thriller about a woman who shoots her husband and then stops speaking, unraveling a deep mystery.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/71H2LBD0xIL.jpg",
        Price = 14.0,
        RentalPrice = 7.0,
        Language = "English",
        QuantityAvailable = 25
    },
    new Book
    {
        BooksId = 10,
        Name = "Where the Crawdads Sing",
        Author = "Delia Owens",
        Description = "A compelling tale of a young girl who grows up isolated in the marshes of North Carolina and becomes a suspect in a murder case.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/71kqD9ErxSL.jpg",
        Price = 17.0,
        RentalPrice = 8.0,
        Language = "English",
        QuantityAvailable = 18
    },
    new Book
    {
        BooksId = 11,
        Name = "The Night Circus",
        Author = "Erin Morgenstern",
        Description = "A magical story about a mysterious circus that appears without warning and only opens at night, featuring a contest between two young illusionists.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/71MJh0QHS6L.jpg",
        Price = 19.0,
        RentalPrice = 9.5,
        Language = "English",
        QuantityAvailable = 12
    },
    new Book
    {
        BooksId = 12,
        Name = "The Goldfinch",
        Author = "Donna Tartt",
        Description = "A Pulitzer Prize-winning novel about a young boy whose life is transformed by a tragic event and a stolen painting.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/81aPlG58waL.jpg",
        Price = 22.0,
        RentalPrice = 11.0,
        Language = "English",
        QuantityAvailable = 10
    },
    new Book
    {
        BooksId = 13,
        Name = "Becoming",
        Author = "Michelle Obama",
        Description = "The memoir of former First Lady Michelle Obama, offering insight into her life, experiences, and the journey to the White House.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/71xw9qNrxzL.jpg",
        Price = 20.0,
        RentalPrice = 10.0,
        Language = "English",
        QuantityAvailable = 14
    },
    new Book
    {
        BooksId = 14,
        Name = "The Road",
        Author = "Cormac McCarthy",
        Description = "A harrowing tale of a father and son's journey through a post-apocalyptic world, highlighting themes of survival and the bond between parent and child.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/81u3zLGcM3L.jpg",
        Price = 15.0,
        RentalPrice = 7.5,
        Language = "English",
        QuantityAvailable = 17
    },
    new Book
    {
        BooksId = 15,
        Name = "Dune",
        Author = "Frank Herbert",
        Description = "A science fiction epic about a young nobleman who becomes embroiled in a complex struggle for control of the desert planet Arrakis.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/81r5A1RUZBL.jpg",
        Price = 25.0,
        RentalPrice = 12.0,
        Language = "English",
        QuantityAvailable = 8
    },
    new Book
    {
        BooksId = 16,
        Name = "The Hitchhiker's Guide to the Galaxy",
        Author = "Douglas Adams",
        Description = "A humorous science fiction adventure following Arthur Dent's journey through space after Earth is destroyed to make way for an intergalactic highway.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/71H0jfrQ97L.jpg",
        Price = 12.0,
        RentalPrice = 6.0,
        Language = "English",
        QuantityAvailable = 22
    },
    new Book
    {
        BooksId = 17,
        Name = "The Alchemist",
        Author = "Paulo Coelho",
        Description = "A philosophical novel about a young shepherd's quest to discover his personal legend and fulfill his dreams.",
        Url = "https://images-na.ssl-images-amazon.com/images/I/81FybM2q-hL.jpg",
        Price = 13.0,
        RentalPrice = 6.5,
        Language = "English",
        QuantityAvailable = 30
    }
                };
                booksCollection.InsertMany(books,new InsertManyOptions { IsOrdered = false});
            }
        }
    }

