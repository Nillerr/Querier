using System;
using Microsoft.AspNetCore.Mvc;

namespace Querier.AspNetCore.Demo.Framework
{
    public sealed class ActionResultSource : IActionResultSource
    {
        private IActionResult? _actionResult;

        public IActionResult? ActionResult
        {
            get => _actionResult;
            set
            {
                if (_actionResult is not null)
                {
                    throw new InvalidOperationException($"An Action Result of type '{_actionResult.GetType()}' has already been set. A handler invoked through ASP.NET Core may not return multiple Action Results.");
                }
                
                _actionResult = value;
            }
        }
    }
}