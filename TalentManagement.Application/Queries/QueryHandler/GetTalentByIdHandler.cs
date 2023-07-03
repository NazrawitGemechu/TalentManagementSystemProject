using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class GetTalentByIdHandler : IRequestHandler<GetTalentByIdQuery,Talent>
    {
      //  private ApplicationDbContext _context;
      private readonly IMediator _mediator;

        public GetTalentByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Talent> Handle(GetTalentByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetAllTalentsQuery());
            var output= results.FirstOrDefault(x=>x.Id==request.Id);
           //var output = results.Include
            return output;
        }
    }
}
