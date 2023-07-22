using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TalentManagement.Application.Commands.EducationLevelCommand;
using TalentManagement.Application.Queries.EducationLevelQuery;
using TalentManagement.Domain.Entities;

namespace TalentManagement.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EducationLevelController : Controller
    {
        private readonly IMediator _mediator;

        public EducationLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            //reads all educationlevels from db by using getalltalentsguery then puts it in var result 
            //then returns the view stored in var result
            var result = await _mediator.Send(new GetAllEducationLevelsQuery());
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EducationLevel educationLevelObj)
        {
            var command = new CreateEducationLevelCommand() { NewEducationLevel = educationLevelObj };
            var result = await _mediator.Send(command);
            return RedirectToAction("Index");
        }
    }
}
