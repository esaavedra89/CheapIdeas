
//modelo para Idea
namespace Ideas.Models
{
    public class Idea
    {
        public int Id { get; set; }
        //titlulod e idea
        public string Title { get; set; }
        //Detalles de la idea
        public string Description { get; set; }
        //Cual es la categoria de la diea
        public string Category { get; set; }
        //a quien pertence la idea
        public string UserId { get; set; }
    }
}