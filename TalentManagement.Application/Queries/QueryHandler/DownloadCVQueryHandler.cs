using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.TalentQuery;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class DownloadCVQueryHandler : IRequestHandler<DownloadCVQuery, FileContentResult>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DownloadCVQueryHandler(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<FileContentResult> Handle(DownloadCVQuery request, CancellationToken cancellationToken)
        {
            
               
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "file", request.FileName);
           
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                return new FileContentResult(fileBytes, "application/octet-stream")
                {
                    FileDownloadName = request.FileName
                };
            
           
        }
    }
}
