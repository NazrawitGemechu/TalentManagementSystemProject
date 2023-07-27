using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Commands.JobCommand
{
    public class GetAppliedJobsQuery : IRequest<List<Job>>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
