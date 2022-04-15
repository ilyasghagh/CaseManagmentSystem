using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Services;

namespace Case.web.Components
{
    public class TermsViewComponent: ViewComponent
    {
        private readonly ITermConditionService _termConditionService;
        public TermsViewComponent(ITermConditionService termConditionService)
        {
            _termConditionService = termConditionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _termConditionService.GetAll();
            // return await Task.FromResult((IViewComponentResult)View("SocialLinks", model));
            return await Task.FromResult((IViewComponentResult)View("Terms", model));
        }
    }
}
