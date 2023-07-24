using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.ViewModels;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Queries.JobQuery
{
    public class GetJobDetailQuery : IRequest<Job>
    {
        public int Id { get; set; }
    }
}
