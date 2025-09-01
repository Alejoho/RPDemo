using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RPDemoApp.Pages.Foods;

public class ListModel : PageModel
{
    private readonly IFoodData _foodData;

    public List<FoodModel> FoodList { get; set; }

    public ListModel(IFoodData foodData)
    {
        _foodData = foodData;
    }

    public async Task OnGet()
    {
        FoodList = await _foodData.GetAllFood();
    }
}

