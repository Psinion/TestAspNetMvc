using Microsoft.AspNetCore.Mvc;
using TestAspNetMvc.Controllers.Base;
using TestAspNetMvc.Data.Models;
using TestAspNetMvc.ViewModels;

namespace TestAspNetMvc.Controllers;

public class DepartmentsController : ConnectedController
{
    public DepartmentsController(IConfiguration configuration) : base(configuration)
    {
    }

    public IActionResult Index()
    {
        var usersVm = new DepartmentsViewModel()
        {
            Departments = UnitOfWork.DepartmentsRepository.GetAll()
        };
        return View(usersVm);
    }

    [HttpGet]
    [Route("Departments/DepartmentEdit")]
    [Route("Departments/DepartmentEdit/{id}")]
    public IActionResult DepartmentEdit(int? id)
    {
        if (id == null)
        {
            return View(new DepartmentEditViewModel());
        }

        var department = UnitOfWork.DepartmentsRepository.GetById(id.Value);
        if (department == null)
        {
            return NotFound();
        }

        var departmentVm = new DepartmentEditViewModel()
        {
            Id = department.Id,
            Name = department.Name
        };

        return View(departmentVm);
    }

    [HttpPost]
    [Route("Departments/DepartmentEdit")]
    [Route("Departments/DepartmentEdit/{id}")]
    public IActionResult DepartmentEdit(DepartmentEditViewModel departmentModel)
    {
        if (departmentModel.Name?.Length < 2)
        {
            ModelState.AddModelError(nameof(departmentModel.Name), "Слишком короткое имя");
        }

        if (ModelState.IsValid)
        {
            var department = new Department()
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name
            };

            if (department.Id == 0)
            {
                UnitOfWork.DepartmentsRepository.Add(department);
            }
            else
            {
                UnitOfWork.DepartmentsRepository.Update(department);
            }

            return RedirectToAction("Index");
        }

        return View(departmentModel);
    }

    [HttpDelete("{id}")]
    [Route("Departments/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var depRep = UnitOfWork.DepartmentsRepository;

        bool hasUser = depRep.HasUser(id);

        if (!hasUser)
        {
            depRep.Remove(id);
        }

        return RedirectToAction("Index");
    }
}