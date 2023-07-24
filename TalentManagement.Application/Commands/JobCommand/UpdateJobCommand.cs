using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.ViewModels;

namespace TalentManagement.Application.Commands.JobCommand
{
    public class UpdateJobCommand : IRequest<IActionResult>
    {
        public PostAJobViewModel Model { get; set; }
    }

}
