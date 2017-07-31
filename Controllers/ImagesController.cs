using AutoMapper;
using mego.Core;
using mego.Core.Models;
using mego.Core.Models.Resourses;
using mego.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mego.Controllers
{
    [Route("/api/orders/{orderId}/images")]
    public class ImagesController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IImageRepo _imageRepo;
        private readonly ImageSettings imageSettings;

        public ImagesController(IHostingEnvironment host, IImageRepo imageRepo, IOrderRepository repository, IUnitOfWork uow, IMapper mapper, IOptionsSnapshot<ImageSettings> options)
        {
            imageSettings = options.Value;
            _host = host;
            _repository = repository;
            _uow = uow;
            _mapper = mapper;
            _imageRepo = imageRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<ImageResource>> GetImages(int orderId)
        {
            var images = await _imageRepo.GetImages(orderId);

            return _mapper.Map<IEnumerable<Image>, IEnumerable<ImageResource>>(images);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(int orderId, IFormFile fileImage)
        {
            var order = await _repository.GetOrder(orderId, includeRelated: false);

            if (order == null)
                return NotFound();

            if (fileImage == null) return BadRequest("Null Image");

            if (fileImage.Length == 0) return BadRequest("Empty File");

            if (fileImage.Length > imageSettings.MaxSize) return BadRequest("Maximum sixe exceeded");

            if (!imageSettings.IsSupported(fileImage.FileName))
                return BadRequest("Invalid type");

            var pathUpload = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(pathUpload))
                Directory.CreateDirectory(pathUpload);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName);

            var filePath = Path.Combine(pathUpload, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileImage.CopyToAsync(stream);
            }

            var image = new Image { FileName = fileName };

            order.Images.Add(image);

            await _uow.CompleteAsync();

            return Ok(_mapper.Map<Image, ImageResource>(image));
        }
    }
}
