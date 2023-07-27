using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManagement.Application.Queries.TalentQuery
{
    public class DownloadCVQuery : IRequest<FileContentResult>
    {
        public string FileName { get; set; }
    }
}
