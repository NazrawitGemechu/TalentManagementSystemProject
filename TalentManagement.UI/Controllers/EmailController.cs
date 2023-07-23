//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using TalentManagement.UI.Migrations;
//using TalentManagement.Domain.Entities;

//namespace TalentManagement.UI.Controllers
//{
//    public class EmailController : Controller
//    {
//       // private UserManager<ApplicationUser> userManager;
//        private readonly UserManager<ApplicationUser> _userManager;
//        public EmailController(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        public async Task<IActionResult> ConfirmEmail(string token, string email)
//        {
//            var user = await _userManager.FindByEmailAsync(email);
//            if (user == null)
//                return View("Error");

//            var result = await _userManager.ConfirmEmailAsync(user, token);
//            return View(result.Succeeded ? "ConfirmEmail" : "Error");
//        }
//    }
//}











//using MediatR;
//using TalentManagement.Application.Commands.TalentCommand;
//using TalentManagement.Domain.Entities;
//using TalentManagement.Persistance.Data;

//public class CreateTalentHandler : IRequestHandler<CreateTalentCommand, Talent>
//{
//    private readonly ApplicationDbContext _context;

//    public CreateTalentHandler(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<Talent> Handle(CreateTalentCommand request, CancellationToken cancellationToken)
//    {
//        _context.Add(request.NewTalent);
//        await _context.SaveChangesAsync();
//        return request.NewTalent;
//    }
//}


//using MediatR;
//using TalentManagement.Domain.Entities;

//public class CreateTalentCommand : IRequest<Talent>
//{
//    public Talent NewTalent { get; set; }
//}