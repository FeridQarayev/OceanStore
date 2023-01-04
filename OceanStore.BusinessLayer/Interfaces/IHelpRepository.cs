using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Interfaces
{
    public interface IHelpRepository
    {
        Task<string> CheckImage(IFormFile photo);
        Task<string> SavePhotoProject(IFormFile photo, string folder);
    }
}
