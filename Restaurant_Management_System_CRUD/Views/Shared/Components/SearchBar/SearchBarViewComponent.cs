using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Management_System_CRUD.Views.Shared.Components.SearchBar
{
    public class SearchBarViewComponent: ViewComponent
    {
        public SearchBarViewComponent()
        {

        }

        public IViewComponentResult Invoke(SPager SearchPager)
        {
            return View("Default", SearchPager);
        }
    }
}
