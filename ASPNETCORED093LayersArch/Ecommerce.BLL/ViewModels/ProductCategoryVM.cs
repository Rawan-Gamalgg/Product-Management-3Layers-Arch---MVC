
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.BLL
{
    public class ProductCategoryVM
    {
        //category
        public int CatId { get; set; } //set by user
        public List<SelectListItem>? Categories { get; set; } //set by controller and used by user to select category
        //--------------------------------------------------------------------
        //Product
        public int ProdId { get; set; } //set by user
        public List<SelectListItem>? Products { get; set; } //set by controller and used by user to select Product




    }
}
