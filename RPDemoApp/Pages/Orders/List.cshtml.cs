using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemoApp.Pages.Orders
{
    public class ListModel : PageModel
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        public List<OrderModel> Orders { get; set; }
        public List<FoodModel> Foods { get; set; }

        public ListModel(IOrderData orderData, IFoodData foodData)
        {
            this._orderData = orderData;
            this._foodData = foodData;
        }

        public async Task OnGet()
        {
            Foods = await _foodData.GetAllFood();
            Orders = await _orderData.GetAllOrders();
        }
    }
}
