using Application.Models;
using Application.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInfoRepository _infoRepository;

        public AdminController(IProductRepository repository, IWebHostEnvironment hostingEnvironment, IInfoRepository infoRepository)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
            _infoRepository = infoRepository;
        }

        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Name")
        {
            var query = _repository.Products.AsNoTracking().OrderBy(p => p.ProductID).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.Name.Contains(filter));
            }

            var model = await PagingList.CreateAsync(query, 10, page, sortExpression, "Name"); // 10 - it how many objects will be show in view
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter}
            };
            return View(model);
        }

        [HttpGet]
        public async Task<ViewResult> Edit(int productId)
        {
            Product product = await _repository.GetProduct(productId);
            ProductEditViewModel productEditViewModel = new ProductEditViewModel
            {
                Id = product.ProductID,
                Name = product.Name,
                Category = product.Category,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Price = product.Price,
                ExistingPhotoPath = product.Image,
                ExistingPhotoPath2 = product.Image2,
                ExistingPhotoPath3 = product.Image3
            };

            return View(productEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = await _repository.GetProduct(model.Id);
                product.Name = model.Name;
                product.Price = model.Price;
                product.ShortDescription = model.ShortDescription;
                product.Description = model.Description;
                product.Category = model.Category;
                if (model.Images != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image = ProcessUploadFile(model);
                }
                if (model.Images2 != null)
                {
                    if (model.ExistingPhotoPath2 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath2);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image2 = ProcessUploadFile2(model);
                }
                if (model.Images3 != null)
                {
                    if (model.ExistingPhotoPath3 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath3);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image3 = ProcessUploadFile3(model);
                }

                _repository.SaveProduct(product);
                TempData["message"] = $"Объект {product.Name} был отредактирован.";
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
                string uniqueFileName2 = ProcessUploadFile2(model);
                string uniqueFileName3 = ProcessUploadFile3(model);
                var newProduct = new Product
                {
                    Name = model.Name,
                    ShortDescription = model.ShortDescription,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,
                    Image = uniqueFileName,
                    Image2 = uniqueFileName2,
                    Image3 = uniqueFileName3
                };

                _repository.Add(newProduct);
                TempData["message"] = $"Объект {model.Name} был создан.";
                return RedirectToAction("Index", new { id = newProduct.ProductID });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            Product deleteProduct = await _repository.DeleteProduct(productId);

            if (deleteProduct != null)
            {
                TempData["message"] = $"Объект {deleteProduct.Name} был удален.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult CreateInfo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInfo(InfoCreateViewModel model) //НЕИСПОЛЬЗУЕТСЯ!
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadAppImage(model);
                string uniqueSocialFileName = ProcessUploadAppSocialImage(model);
                string uniqueBackgroundImage = ProcessUploadAppBackgroundImage(model);
                var newInfo = new Info
                {
                    AppName = model.AppName,
                    AppAddress = model.AppAddress,
                    AppPhone1 = model.AppPhone1,
                    AppPhone2 = model.AppPhone2,
                    AppSocialAddress = model.AppSocialAddress,
                    AppImage = uniqueFileName,
                    AppSocialImg = uniqueSocialFileName,
                    AppBackgroundImage = uniqueBackgroundImage,
                    AppEmail = model.AppEmail
                };
                _infoRepository.Add(newInfo);
                TempData["message"] = $"{model.AppName} был создан.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ViewResult EditInfo() //получаем текущие данные о приложении(Название, логотип, контакты и тд. приложения).
        {
            Info info = _infoRepository.GetInfo(1);
            InfoEditViewModel infoEditViewModel = new InfoEditViewModel
            {
                Id = info.InfoID,
                AppName = info.AppName, //имя приложения
                AppAddress = info.AppAddress, //адресс(контакты)
                AppPhone1 = info.AppPhone1, //телефон(контакты)
                AppPhone2 = info.AppPhone2, //телефон(контакты)
                AppSocialAddress = info.AppSocialAddress, //ссылка на социальную сеть(контакты)
                ExistingImagePath = info.AppImage, //логотип
                ExistingSocialImagePath = info.AppSocialImg, //социальная ссылка
                ExistingBackgroundImagePath = info.AppBackgroundImage, //задний фон приложения
                AppEmail = info.AppEmail //почтовый адрес приложения
            };
            return View(infoEditViewModel);
        }
        [HttpPost]
        public IActionResult EditInfo(InfoEditViewModel model) //отправляем обновленные данные о приложении в Базу Данных(Название, логотип, контакты и тд. приложения).
        {
            if (ModelState.IsValid)
            {
                Info info = _infoRepository.GetInfo(model.Id = 1);
                info.AppName = model.AppName;
                info.AppAddress = model.AppAddress;
                info.AppPhone1 = model.AppPhone1;
                info.AppPhone2 = model.AppPhone2;
                info.AppSocialAddress = model.AppSocialAddress;
                info.AppEmail = model.AppEmail;

                if (model.AppImages != null) //обновление изображения логотипа сайта.
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppImage = ProcessUploadAppImage(model);
                }

                if (model.AppSocialImgs != null) //обновление изображение социальной ссылки.
                {
                    if (model.ExistingSocialImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingSocialImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppSocialImg = ProcessUploadAppSocialImage(model);
                }

                if (model.AppBackgroundImages != null) //обновление изображение заднего фона приложения.
                {
                    if (model.ExistingBackgroundImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingBackgroundImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppBackgroundImage = ProcessUploadAppBackgroundImage(model);
                }

                _infoRepository.Update(info);
                TempData["message"] = $"Информация о приложения {model.AppName} была отредактирована.";
                return RedirectToAction("Index");
            }
            return View();
        }


        //Методы отвечающие за получение изображения и загрузкии ее в БД.
        private string ProcessUploadAppImage(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppImages != null && model.AppImages.Count > 0)
            {
                foreach (IFormFile photo in model.AppImages)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadAppSocialImage(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppSocialImgs != null && model.AppSocialImgs.Count > 0)
            {
                foreach (IFormFile photo in model.AppSocialImgs)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }

        private string ProcessUploadAppBackgroundImage(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppBackgroundImages != null && model.AppBackgroundImages.Count > 0)
            {
                foreach (IFormFile photo in model.AppBackgroundImages)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }

        private string ProcessUploadFile(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadFile2(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images2 != null && model.Images2.Count > 0)
            {
                foreach (IFormFile photo in model.Images2)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadFile3(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images3 != null && model.Images3.Count > 0)
            {
                foreach (IFormFile photo in model.Images3)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
    }
}
