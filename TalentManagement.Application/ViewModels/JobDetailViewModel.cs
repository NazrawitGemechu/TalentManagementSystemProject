using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Domain.Entities;

namespace TalentManagement.Application.ViewModels
{
    public class JobDetailViewModel
    {
        public Job Job { get; set; }
        public bool IsApplied { get; set; }
    }
}
