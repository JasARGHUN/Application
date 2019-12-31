using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Components
{
    public class AppNameViewComponent : ViewComponent
    {
        private readonly IInfoRepository _infoRepository;
        public AppNameViewComponent(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }
        public IViewComponentResult Invoke()
        {
            var info = _infoRepository.GetInfo(1);
            return View(info);
        }
    }
}
