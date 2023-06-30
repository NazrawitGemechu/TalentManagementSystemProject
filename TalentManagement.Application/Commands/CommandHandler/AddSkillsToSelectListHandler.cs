using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManagement.Application.Commands.SkillCommand;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;

namespace TalentManagement.Application.Commands.CommandHandler
{ 
    
    public class AddSkillsToSelectListHandler : IRequestHandler<AddSkillsToSelectList, Skill>
    {
        private readonly ApplicationDbContext _context;

        public AddSkillsToSelectListHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Skill> Handle(AddSkillsToSelectList request, CancellationToken cancellationToken)
        {
            BindSkills();
           return request.selectList;
            //return request.BindSkills();
        }

        public List<SelectListItem> BindSkills()
        {
           
            
                var skillsFromDb = _context.Skills.ToList();
                //creates an object of selectListItem
                var selectList = new List<SelectListItem>();
                //for each item in the retrived data from database it adds new select list item to the select list
                foreach (var item in skillsFromDb)
                {
                    selectList.Add(new SelectListItem(item.SkillName, item.Id.ToString()));
                }
                //returns the select list that is populated by data
                return selectList;

            
        }
    }
}
