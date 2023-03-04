using System.ComponentModel.Design;

namespace Bookshop.App.Services.Resource
{
    public class ResourceService
    {
        private readonly IConfiguration _configuration;

        public ResourceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       public string Get(long fileName)
        {
           
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory() + _configuration.GetConnectionString("Storage"), "bookPhotos");

                var fullPath = Path.Combine(pathToSave, fileName + ".png");
                if (File.Exists(fullPath))
                    return $"{_configuration.GetConnectionString("Assets")}/images/{fileName}.png";
                return null;
          
        }
    }
}
