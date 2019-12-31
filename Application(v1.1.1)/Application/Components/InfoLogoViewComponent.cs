using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Components
{
    public class InfoLogoViewComponent : ViewComponent
    {
        private readonly IInfoRepository _infoRepository;

        public InfoLogoViewComponent(IInfoRepository infoRepository)
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
