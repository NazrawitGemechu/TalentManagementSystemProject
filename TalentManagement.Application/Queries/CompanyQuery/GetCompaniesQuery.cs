using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.Queries.CompanyQuery
{
    public class GetCompaniesQuery : IRequest<List<Company>>
    {
    }

}
