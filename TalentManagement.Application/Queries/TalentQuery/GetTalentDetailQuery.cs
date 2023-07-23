using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Queries.TalentQuery
{
    public class GetTalentDetailQuery : IRequest<Talent>
    {
        public int Id { get; set; }
    }
    
}
