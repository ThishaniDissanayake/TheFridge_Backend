namespace FridgeManagerAPI.Models
{
    public class FridgeItem
    {
        public int Id { get; set; }               // Primary key
        public string Name { get; set; }          // Name of the fridge item
        public DateTime ExpiryDate { get; set; }  // Expiration date of the item
    }
}