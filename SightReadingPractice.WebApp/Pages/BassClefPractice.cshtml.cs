using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BassClefPractice.WebApp.Pages
{
    public class BassClefPracticeModel : PageModel
    {
        private readonly ILogger<BassClefPracticeModel> _logger;

        public BassClefPracticeModel(ILogger<BassClefPracticeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
