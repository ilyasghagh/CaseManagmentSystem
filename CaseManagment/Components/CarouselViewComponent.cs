using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Case.Services;
namespace Case.web.Components
{
    public class CarouselViewComponent:ViewComponent
    {
        private readonly ICarouselService _carouselService;
        public CarouselViewComponent(ICarouselService carouselService)
        {
            _carouselService = carouselService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _carouselService.GetAll();
           // return await Task.FromResult((IViewComponentResult)View("SocialLinks", model));
            return await Task.FromResult((IViewComponentResult)View("Carousel",model));
        }
    }
}
