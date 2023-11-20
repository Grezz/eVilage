using Microsoft.AspNetCore.Mvc;
using Piranha.AspNetCore.Services;
using Piranha;
using Piranha.Models;
using eVilage.Models;

namespace eVilage.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class CmsController : Controller
{
    private readonly IApi _api;
    private readonly IModelLoader _loader;

    public CmsController(IApi api, IModelLoader loader) 
    { 
        _api = api; 
        _loader = loader;
    }

    /// <summary>
    /// Gets the page with the given id.
    /// </summary>
    /// <param name="id">The unique page id</param>
    /// <param name="draft">If a draft is requested</param>
    [Route("page")]
    public async Task<IActionResult> Page(Guid id, bool draft = false)
    {
        try
        {
            var model = await _loader.GetPageAsync<StandardPage>(id, HttpContext.User, draft);

            return View(model);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

}
