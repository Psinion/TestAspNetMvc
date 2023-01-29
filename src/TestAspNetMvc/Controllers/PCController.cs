using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Controllers.Base;
using TestAspNetMvc.Data.Models;
using TestAspNetMvc.ViewModels;

namespace TestAspNetMvc.Controllers;

public class PCController : ConnectedController
{
    public PCController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        var usersVm = new PCViewModel()
        {
            PCs = UnitOfWork.PCRepository.GetAll()
        };
        return View(usersVm);
    }

    [HttpGet]
    [Route("PC/PCEdit")]
    [Route("PC/PCEdit/{id}")]
    public IActionResult PCEdit(int? id)
    {
        if (id == null)
        {
            return View(new PCEditViewModel());
        }

        var pc = UnitOfWork.PCRepository.GetById(id.Value);
        if (pc == null)
        {
            return NotFound();
        }

        var pcVm = new PCEditViewModel()
        {
            Id = pc.Id,
            Cpu = pc.Cpu,
            Memory = pc.Memory,
            Hdd = pc.Hdd
        };

        return View(pcVm);
    }

    [HttpPost]
    [Route("PC/PCEdit")]
    [Route("PC/PCEdit/{id}")]
    public IActionResult PCEdit(PCEditViewModel pcModel)
    {
        if (pcModel.Cpu < 0)
        {
            ModelState.AddModelError(nameof(pcModel.Cpu), "Требуется положительное число");
        }

        if (pcModel.Memory < 0)
        {
            ModelState.AddModelError(nameof(pcModel.Memory), "Требуется положительное число");
        }

        if (pcModel.Hdd < 0)
        {
            ModelState.AddModelError(nameof(pcModel.Hdd), "Требуется положительное число");
        }

        if (ModelState.IsValid)
        {
            var pc = new PC()
            {
                Id = pcModel.Id,
                Cpu = pcModel.Cpu,
                Memory = pcModel.Memory,
                Hdd = pcModel.Hdd
            };

            if (pc.Id == 0)
            {
                UnitOfWork.PCRepository.Add(pc);
            }
            else
            {
                UnitOfWork.PCRepository.Update(pc);
            }

            return RedirectToAction("Index");
        }

        return View(pcModel);
    }

    [HttpDelete("{id}")]
    [Route("PC/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var pcRep = UnitOfWork.PCRepository;

        bool hasUser = pcRep.HasUser(id);

        if (!hasUser)
        {
            pcRep.Remove(id);
        }

        return RedirectToAction("Index");
    }
}