using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPDemoApp.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly IFoodData _foodData;
    private readonly IOrderData _orderData;

    [BindProperty]
    public OrderModel Order { get; set; }

    public List<SelectListItem> Items { get; set; }

    public CreateModel(IFoodData foodData, IOrderData orderData, IConfiguration config)
    {

        string connString = config.GetConnectionString("Default");
        this._foodData = foodData;
        this._orderData = orderData;
    }

    public async Task OnGet()
    {
        var foods = await _foodData.GetAllFood();

        Items = new List<SelectListItem>();

        // Items.Add(new SelectListItem() { Disabled = true, Selected = true, Text = "Select a meal" });

        foods.ForEach(f =>
            Items.Add(
                new SelectListItem()
                {
                    Value = f.Id.ToString(),
                    Text = f.Title
                })
        );
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid is false)
        {
            return Page();
        }

        var foods = await _foodData.GetAllFood();

        Order.Total = foods.First(f => f.Id == Order.FoodId).Price * Order.Quantity;

        int id = await _orderData.CreateOrderAsync(Order);

        return RedirectToPage("./Display", new { ID = id });
    }
}
