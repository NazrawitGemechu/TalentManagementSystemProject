using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using TalentManagement.Application.Commands.SkillCommand;
using TalentManagement.Application.Commands.TalentCommand;
using TalentManagement.Application.Queries.EducationLevelQuery;
using TalentManagement.Application.Queries.SkillQuery;
using TalentManagement.Application.Queries.TalentQuery;
using TalentManagement.Domain.Entities;
using TalentManagement.Persistance.Data;
using TalentManagement.UI.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TalentManagement.UI.Controllers
{
    public class TalentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public TalentController(IMediator mediator, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           
            var talents = await _mediator.Send(new GetAllTalentsQuery());
            return View(talents);
        }
        public async Task<IActionResult> Create()
        {         
            var vm = new CreateTalentViewModel()
            {
                Skills = await BindSkills(),
                EducationLevels = await BindEducationLeves()

            };
            vm.TalentExperiences.Add(new TalentExperience() { Id = 1 });
           
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTalentViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (IsDuplicateTalent(model.FirstName, model.LastName, model.Email) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Talent talent = new Talent()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Gender = model.Gender,
                        Country = model.Country,
                        Language = model.Language,
                        PhoneNo = model.PhoneNo,
                        TalentExperiences = model.TalentExperiences,
                        FilePath = CVUpload(model),
                    };

                    foreach (var item in model.SelectedSkills)
                    {
                        talent.Skills.Add(new TalentSkill()
                        {
                            SkillId = item
                        });
                    }

                    foreach (var item in model.SelectedEducation)
                    {
                        talent.EducationLevels.Add(new TalentEducationLevel()
                        {
                            EducationLevelId = item
                        });

                    }
                    var command = new CreateTalentCommand() { NewTalent = talent };
                    var result = await _mediator.Send(command);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {          
            CreateTalentViewModel model = new CreateTalentViewModel(); 

            List<int> skillsIds = new List<int>();
            List<int> educationIds = new List<int>();
                    
               //Get talent 
                var talent = _context.Talents.Include("Skills").FirstOrDefault(x => x.Id == Id);
                //Get talent skills and add each skillId into selectedskills list
                talent.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));

                //get talent wirh education levels
                var talentE = _context.Talents.Include("EducationLevels")
                                              .FirstOrDefault(x=>x.Id== Id);
                talent.EducationLevels.ToList().ForEach(result=> educationIds.Add(result.EducationLevelId));

             
            Talent tal = _context.Talents.Include(e => e.TalentExperiences)
                                         .Where(a => a.Id == Id)
                                         .FirstOrDefault();
            

            //bind model 
            model.Skills = _context.Skills.Select
                (x => new SelectListItem { 
                    Text = x.SkillName, 
                    Value = x.Id.ToString() 
                }).ToList();
            model.EducationLevels = _context.EducationLevels.Select
                (x => new SelectListItem {
                    Text = x.EducationLevelName,
                    Value = x.Id.ToString()
                }).ToList();           
            model.TalentExperiences = tal.TalentExperiences;
            model.Id = talent.Id;
            model.FirstName = talent.FirstName;
            model.LastName = talent.LastName;
            model.Country = talent.Country;
            model.Gender = talent.Gender;
            model.Email = talent.Email;
            model.PhoneNo= talent.PhoneNo;
            model.FileCV = talent.FileCV;
            model.Language = talent.Language;
            model.SelectedSkills = skillsIds.ToArray();
            model.SelectedEducation= educationIds.ToArray();
                
                      
                return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CreateTalentViewModel model)
        {

            Talent talent = new Talent();
            List<TalentSkill> talentSkills = new List<TalentSkill>();
            List<TalentEducationLevel> talentEducationLevels = new List<TalentEducationLevel>();

            if (ModelState.IsValid)
            {
                //first find talent skill list and then remove all from db 
                talent = _context.Talents.Include("Skills").FirstOrDefault(x => x.Id == model.Id);
                talent.Skills.ToList().ForEach(result => talentSkills.Add(result));
                _context.TalentSkill.RemoveRange(talentSkills);
                _context.SaveChanges();
                //second find talent education level list and remove all from db
                talent = _context.Talents.Include("EducationLevels").FirstOrDefault(x => x.Id == model.Id);
                talent.EducationLevels.ToList().ForEach(result => talentEducationLevels.Add(result));
                _context.TalentEducationLevel.RemoveRange(talentEducationLevels);
                _context.SaveChanges();
                //third find talent experience list and remove all from db
                List<TalentExperience> talentExp = _context.TalentExperiences.Where(d=>d.TalentId== model.Id).ToList();
                _context.TalentExperiences.RemoveRange(talentExp);
                _context.SaveChanges();
                //fourth update talent details
                talent.FirstName = model.FirstName;
                talent.LastName= model.LastName;
                talent.Gender = model.Gender;
                talent.Country = model.Country;
                talent.Email = model.Email;
                talent.Language = model.Language;
                talent.PhoneNo= model.PhoneNo;
                talent.TalentExperiences = model.TalentExperiences;
                if (model.FileCV != null)
                {
                    string uinqueFileName = CVUpload(model) ;
                    talent.FilePath= uinqueFileName;

                }
                //talent skills
                if (model.SelectedSkills.Length > 0)
                {
                    talentSkills = new List<TalentSkill>();

                    foreach (var skillid in model.SelectedSkills)
                    {
                        talentSkills.Add(new TalentSkill { SkillId = skillid, TalentId = model.Id });
                    }
                    talent.Skills = talentSkills;
                }
                _context.SaveChanges();
                // talent education levels
                if (model.SelectedEducation.Length > 0)
                {
                    talentEducationLevels = new List<TalentEducationLevel>();

                    foreach (var educationid in model.SelectedSkills)
                    {
                        talentEducationLevels.Add(new TalentEducationLevel { EducationLevelId = educationid, TalentId = model.Id });
                    }
                    talent.EducationLevels = talentEducationLevels;
                }
                _context.SaveChanges();
               // _context.TalentExperiences.AddRange(talent.TalentExperiences);
                _context.Attach(talent);
                _context.Entry(talent).State= EntityState.Modified;
               // _context.TalentExperiences.AddRange(talent.TalentExperiences);
                _context.SaveChanges();


                return RedirectToAction("Index");              
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            CreateTalentViewModel model = new CreateTalentViewModel();

            List<int> skillsIds = new List<int>();
            List<int> educationIds = new List<int>();

            //Get talent 
            var talent = _context.Talents.Include("Skills").FirstOrDefault(x => x.Id == Id);
            //Get talent skills and add each skillId into selectedskills list
            talent.Skills.ToList().ForEach(result => skillsIds.Add(result.SkillId));

            //get talent wirh education levels
            var talentE = _context.Talents.Include("EducationLevels")
                                          .FirstOrDefault(x => x.Id == Id);
            talent.EducationLevels.ToList().ForEach(result => educationIds.Add(result.EducationLevelId));


            Talent tal = _context.Talents.Include(e => e.TalentExperiences)
                                         .Where(a => a.Id == Id)
                                         .FirstOrDefault();


            //bind model 
            model.Skills = _context.Skills.Select
                (x => new SelectListItem
                {
                    Text = x.SkillName,
                    Value = x.Id.ToString()
                }).ToList();
            model.EducationLevels = _context.EducationLevels.Select
                (x => new SelectListItem
                {
                    Text = x.EducationLevelName,
                    Value = x.Id.ToString()
                }).ToList();
            model.TalentExperiences = tal.TalentExperiences;
            model.Id = talent.Id;
            model.FirstName = talent.FirstName;
            model.LastName = talent.LastName;
            model.Country = talent.Country;
            model.Gender = talent.Gender;
            model.Email = talent.Email;
            model.PhoneNo = talent.PhoneNo;
            model.FileCV = talent.FileCV;
            model.Language = talent.Language;
            model.SelectedSkills = skillsIds.ToArray();
            model.SelectedEducation = educationIds.ToArray();


            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(CreateTalentViewModel model)
        {

            Talent talent = new Talent();
            List<TalentSkill> talentSkills = new List<TalentSkill>();
            List<TalentEducationLevel> talentEducationLevels = new List<TalentEducationLevel>();

           
                
                talent.FirstName = model.FirstName;
                talent.LastName = model.LastName;
                talent.Gender = model.Gender;
                talent.Country = model.Country;
                talent.Email = model.Email;
                talent.Language = model.Language;
                talent.PhoneNo = model.PhoneNo;
                talent.TalentExperiences = model.TalentExperiences;
                talent.FileCV = model.FileCV;

            talent = _context.Talents.Include("Skills").FirstOrDefault(x => x.Id == model.Id);
            talent.Skills.ToList().ForEach(result => talentSkills.Add(result));
            _context.TalentSkill.RemoveRange(talentSkills);
            _context.SaveChanges();

            talent = _context.Talents.Include("EducationLevels").FirstOrDefault(x => x.Id == model.Id);
            talent.EducationLevels.ToList().ForEach(result => talentEducationLevels.Add(result));
            _context.TalentEducationLevel.RemoveRange(talentEducationLevels);
            _context.SaveChanges();
            //third find talent experience list and remove all from db
            List<TalentExperience> talentExp = _context.TalentExperiences.Where(d => d.TalentId == model.Id).ToList();
            _context.TalentExperiences.RemoveRange(talentExp);
            _context.SaveChanges();

            _context.Attach(talent);    
                _context.Entry(talent).State = EntityState.Deleted;
                _context.SaveChanges();
               
                


                return RedirectToAction("Index");
           
           
        }
        public async Task<List<SelectListItem>> BindSkills()
        {
           
            var skillsFromDb = _mediator.Send(new GetAllSkillsQuery());
           
            var selectList = new List<SelectListItem>();
            
            foreach (var item in await skillsFromDb)
            {

                selectList.Add(new SelectListItem(item.SkillName, item.Id.ToString()));
            }
           
            return selectList;
        }
        public async Task<List<SelectListItem>> BindEducationLeves()
        {
           
            var educationFromDb = _mediator.Send(new GetAllEducationLevelsQuery());
            var selectListE = new List<SelectListItem>();
            foreach (var item in await educationFromDb)
            {
                selectListE.Add(new SelectListItem(item.EducationLevelName, item.Id.ToString()));
            }
            return selectListE;
        }
        public string CVUpload(CreateTalentViewModel model)
        {
          
            string uniqueFileName = null;
            if (model.FileCV != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "file");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileCV.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.FileCV.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private bool IsDuplicateTalent(string fname, string lname, string email)
        {
            //var talents = _mediator.Send(new GetAllTalentsQuery());
            return _context.Talents.Any(d => d.FirstName == fname && d.LastName == lname && d.Email==email);
        }
        
    }
}
