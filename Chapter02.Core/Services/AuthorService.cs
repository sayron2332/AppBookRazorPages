using AutoMapper;
using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Dtos.Configuration;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace Chapter02.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthorService(IRepository<Author> repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<ServiceResponse> Create(IFormFile photo, AuthorDto model)
        {
            try
            {
                if (photo != null)
                {
                   
                    ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;
                    string existingFilePath = Path.Combine(imageSettings.AuthorImage, photo.FileName);

                    if (File.Exists(existingFilePath) && model.ImageName != "default.png")
                    {
                        File.Delete(existingFilePath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                    string upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.AuthorImage);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }

                    model.ImageName = fileName;
                }
               
                await _repository.Insert(_mapper.Map<Author>(model));
                await _repository.Save();
                    
                return new ServiceResponse()
                {
                    Success = true,
                    Message = "Author succsessfuly created"
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ServiceResponse> Delete(int Id)
        {
            Author? author = await _repository.GetByID(Id);
            if (author is null)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "Author not find some problem with Id",
                };
            }

            if (author.ImageName != "default.jpg")
            {
                ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;
                string image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.AuthorImage, author.ImageName);
                File.Delete(image);
            }
           
            await _repository.Delete(Id);
            await _repository.Save();
            return new ServiceResponse
            {
                Success = true,
                Message = "Author successfuly deleted"
            };
        }

        public async Task<IEnumerable<AuthorDto>> GetAll()
        { 
            IEnumerable<Author> authors = await _repository.GetAll();
            List<AuthorDto> mappedAuthors = new List<AuthorDto>();
            ImageSettings? imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>();
            foreach (var author in authors)
            {
                 author.ImageName = Path.Combine(imageSettings?.AuthorImage + author.ImageName);
                 mappedAuthors.Add(_mapper.Map<AuthorDto>(author));
            }
            return mappedAuthors;
        }
        public async Task<ServiceResponse> GetbyId(int Id)
        {
           Author? author = await _repository.GetByID(Id);
           if (author is null)
           {
               return new ServiceResponse()
               {
                   Success = false,
                   Message = "Author not find some problem with Id",
               };
           }
          
           return new ServiceResponse()
           {
               Success = true,
               Payload =  _mapper.Map<AuthorDto>(author)
           };

        }

        public async Task<ServiceResponse> Update(IFormFile photo, AuthorDto model)
        {
           Author? author = await _repository.GetByID(model.Id);
           if (author is null)
           {
               return new ServiceResponse()
               {
                   Success = false,
                   Message = "Author not find some problem with Id",
               };
           }
           if (photo != null)
           {
               ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;
                
               if (author.ImageName != "default.jpg")
               {
                   string oldImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.AuthorImage, author.ImageName);
                   File.Delete(oldImage);
               }
            
               string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
               string upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.AuthorImage);


               using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
               {
                   photo.CopyTo(fileStream);
               }

               author.ImageName = fileName;
           }

           author.Name = model.Name;
           author.Surname = model.Surname;

           await _repository.Update(author);
           await _repository.Save();
           return new ServiceResponse()
           { 
               Success = true,
               Message = "Author succsessfuly updated"
           };

        }
    }
}
