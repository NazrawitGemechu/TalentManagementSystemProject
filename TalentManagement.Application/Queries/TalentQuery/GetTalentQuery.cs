using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.ViewModels;

namespace TalentManagement.Application.Queries.TalentQuery
{
    public class GetTalentQuery : IRequest<CreateTalentViewModel>
    {
        public int TalentId { get; set; }
    }
}
