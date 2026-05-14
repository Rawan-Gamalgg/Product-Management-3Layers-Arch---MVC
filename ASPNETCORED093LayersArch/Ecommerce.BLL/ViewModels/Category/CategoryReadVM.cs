
namespace Ecommerce.BLL
{
    public class CategoryReadVM
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int?  ProductsCount { get; set; }
        //list of products in this category
        public List<ProductReadVM> Products { get; set; }


    }
}
