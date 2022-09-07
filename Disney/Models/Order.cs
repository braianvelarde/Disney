namespace Disney.Models
{
    public record Order
    {
        public record ASC() : Order();
        public record DESC() : Order();
    }
}
