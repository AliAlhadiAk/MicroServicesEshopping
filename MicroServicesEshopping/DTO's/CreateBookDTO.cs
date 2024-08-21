using MongoDB.Bson.Serialization.Attributes;

namespace MicroServicesEshopping.DTO_s
{
    public class CreateBookDTO
    {

        public int BooksId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Author { get; set; }
        public string Url { get; set; }  
        public double Price { get; set; }   
        public double RentalPrice { get; set; }
        public string Language { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
