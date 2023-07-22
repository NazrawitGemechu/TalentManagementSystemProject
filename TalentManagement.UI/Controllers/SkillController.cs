using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentManagement.Application.Commands.SkillCommand;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Domain.Entities;

namespace TalentManagement.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {
        private readonly IMediator _mediator;

        public SkillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            //reads all skillss from db by using getalltalentsguery then puts it in var result 
            //then returns the view stored in var result
            var result = await _mediator.Send(new GetAllSkillsQuery());
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Skill skillObj)
        {
            var command = new CreateSkillCommand() { NewSkill = skillObj };
            var result = await _mediator.Send(command);
            return RedirectToAction("Index");
        }
    }
}
