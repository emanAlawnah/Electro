namespace EcomarceFirstApp.ViewModels
{
    public class CategoriesViewComponent: ViewModels
    {
		private readonly ApplicationDbContext _context;
		public CategoriesViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var categories = _context.Categories.ToList();
			return View(categories);
		}
	}
}
