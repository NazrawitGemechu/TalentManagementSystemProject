using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.ViewModels;

namespace TalentManagement.Application.Queries.JobQuery
{
    public class GetJobQuery : IRequest<PostAJobViewModel>
    {
        public int JobId { get; set; }
    }
}
