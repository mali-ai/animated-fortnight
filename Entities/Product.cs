using System;

namespace Entities
{
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public int typeId { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string picURL { get; set; }
        public DateTime updatedOn { get; set; }
        public int updatedBy { get; set; }
        public bool isActive { get; set; }
        public String typeName { get; set; }
        public String adminName { get; set; }
    }
}
