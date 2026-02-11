using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cart_King.Services;
using Cart_King.Models;

namespace Cart_King.ViewComponents
{
    public class DealsViewComponent : ViewComponent
    {
        private readonly DealsService _dealsService;

        public DealsViewComponent(DealsService dealsService)
        {
            _dealsService = dealsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var deals = await _dealsService.GetFeaturedDealsAsync();
            return View(deals);
        }
    }
}