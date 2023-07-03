using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Queries.TalentQuery
{
    public class GetTalentByIdQuery : IRequest<Talent>
    {
        public GetTalentByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        
    }
}
