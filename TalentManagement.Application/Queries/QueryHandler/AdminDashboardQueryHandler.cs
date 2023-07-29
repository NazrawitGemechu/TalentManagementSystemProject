using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Queries.AdminQuery;
using TalentManagement.Application.ViewModels;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Queries.QueryHandler
{
    public class AdminDashboardQueryHandler : IRequestHandler<AdminDashboardQuery, AdminDashboardViewModel>
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdminDashboardViewModel> Handle(AdminDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboard = new AdminDashboardViewModel
            {
                TotalPosts = await _context.Jobs.Where(x => x.IsAccepted == true).CountAsync(),
                TotalPending = await _context.Jobs.Where(x => x.IsAccepted == null).CountAsync(),
                TotalRejected = await _context.Jobs.Where(x => x.IsAccepted == false).CountAsync(),
                TotalUsers = await _context.ApplicationUser.CountAsync(),
                Roles = await _context.Roles.CountAsync(),
                RejectedTalents = await _context.Talents.Where(x => x.IsAccepted == false).CountAsync(),
                TotalSkills= await _context.Skills.CountAsync(),
                TotalEducationLevels= await _context.EducationLevels.CountAsync(),
                PendingTalents = await _context.Talents.Where(x => x.IsAccepted == null).CountAsync(),
                TotalTalents = await _context.Talents.Where(x => x.IsAccepted == true).CountAsync(),
                RecentPosts = await _context.Jobs.Where(x => x.IsAccepted == true).OrderByDescending(o => o.PostedDate)
                .Include(x => x.Skills)
                .Include(x => x.Recruter)
                .ToListAsync()
            };

            return dashboard;
        }
    }
}
