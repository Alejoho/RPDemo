using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPDemoApp.Models;

namespace RPDemoApp.Pages.Orders;

public class DisplayModel : PageModel
{
    private readonly IFoodData _foodData;
    private readonly IOrderData _orderData;

    public OrderModel Order { set; get; }

    public FoodModel Food { set; get; }

    [BindProperty(SupportsGet = true)]
    public int Id { set; get; }

    [BindProperty]
    public UpdateOrderModel UpdatedOrder { set; get; }

    public DisplayModel(IFoodData foodData, IOrderData orderData)
    {
        this._foodData = foodData;
        this._orderData = orderData;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        // Id = id;
        Order = await _orderData.GetOrderByIdAsync(Id);
        if (Order is not null)
        {
            var foods = await _foodData.GetAllFood();
            Food = foods.First(f => f.Id == Order.FoodId);
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        await _orderData.UpdateOrderName(UpdatedOrder.Id, UpdatedOrder.NewName);

        return RedirectToPage("./Display", new { Id });
    }
}
