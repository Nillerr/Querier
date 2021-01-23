using Microsoft.AspNetCore.Mvc;

namespace Querier.AspNetCore.Demo.Framework
{
    public interface IActionResultSource
    {
        IActionResult? ActionResult { get; set; }
    }
}