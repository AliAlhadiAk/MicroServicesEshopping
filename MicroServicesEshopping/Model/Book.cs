using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MicroServicesEshopping.Model
{
    public class Book
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("books_id")]
        public int BooksId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("rental_price")]
        public double RentalPrice { get; set; }

        [BsonElement("language")]
        public string Language { get; set; }

        [BsonElement("quantity_available")]
        public int QuantityAvailable { get; set; }

    }
}
