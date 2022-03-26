using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BassClefPractice.WebApp.Pages
{
    public class TrebleClefPracticeModel : PageModel
    {
        private readonly ILogger<TrebleClefPracticeModel> _logger;

        public TrebleClefPracticeModel(ILogger<TrebleClefPracticeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
