using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RealBotzDemo.Data;
using RealBotzDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RealBotzDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repo;

        public HomeController(ILogger<HomeController> logger, IRepository repository)
        {
            _logger = logger;
            _repo = repository;
        }

        public IActionResult Index()
        {
            var user = new User();

            #region bind country dropdown

            user.Countries = _repo.GetCountries();

            #endregion bind country dropdown

            #region bind Gender dropdown
            var genderList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Male", Selected = true },
                new SelectListItem { Value = "2", Text = "Female" },
                new SelectListItem { Value = "3", Text = "Other" }
            };
            user.GenderList = genderList;

            #endregion

            #region Bind Hobbies
            var hobbies = new List<Hobby>
            {
                new Hobby { Id = 1, Name = "Reading", IsSelected = false },
                new Hobby { Id = 2, Name = "Cooking", IsSelected = false },
                new Hobby { Id = 3, Name = "Cricket", IsSelected = false },
                new Hobby { Id = 4, Name = "Writing", IsSelected = false },
            };
            user.Hobbies = hobbies;

            #endregion

            return View(user);
        }

        #region Insert Or Update
        [HttpPost]
        public JsonResult CrudOperation(User obj)
        {
            string Message = _repo.Add_Update(obj);
            return Json(new
            {
                Message,
            });
        }
        #endregion

        #region Get Data List
        [HttpPost]
        public PartialViewResult GetList()
        {
            var list = _repo.GetAll();

            return PartialView("_DataListPartial", list);
        }
        #endregion

        #region Get Data by Id
        [HttpPost]
        public JsonResult GetDataById(int Id)
        {
            int StatusCode = 0;
            string Message = "Exception Occured.";
            var obj = new User();
            try
            {
                #region Get Data
                if (Id > 0)
                {
                    obj = _repo.GetById(Id);

                    if (obj != null)
                    {
                        StatusCode = 1;
                        Message = "Record Fetched Successfully!";
                    }
                    else
                    {
                        StatusCode = 0;
                        Message = "Record not found!";
                    }
                }
                else
                {
                    StatusCode = 0;
                    Message = "Invalid Id.";
                }
                #endregion
            }
            catch (Exception e)
            {
                StatusCode = 0;
                Message = "Something Went Wrong!";
                _logger.LogError(e, e.Message);
            }
            return Json(new
            {
                Message,
                StatusCode,
                Data = obj
            });
        }
        #endregion

        #region Delete Data by Id
        [HttpGet]
        public JsonResult Delete(int Id)
        {
            string Message = _repo.Delete(Id);
            return Json(new
            {
                Message,
            });
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
