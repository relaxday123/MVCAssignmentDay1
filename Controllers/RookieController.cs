using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using b1.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace b1.Controllers
{
    public class RookieController : Controller
    {
        private readonly ILogger<RookieController> _logger;
        private readonly IPersonService _personService;

        public RookieController(ILogger<RookieController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index()
        {
            var data = _personService.GetAll();
            return new JsonResult(data);
        }

        public IActionResult GetMaleMembers()
        {
            var data = _personService.GetMale();
            return new JsonResult(data);
        }

        public IActionResult GetOldestMember()
        {
            var data = _personService.GetOldest();
            return new JsonResult(data);
        }

        public IActionResult GetFullNames()
        {
            var data = _personService.GetFullNames();
            return new JsonResult(data);
        }

        #region 

        public IActionResult GetMemberByBirthYear(int year)
        {
            var data = _personService.GetPeopleByBirthYear(year);
            return new JsonResult(data);
        }

        public IActionResult GetMemberByBirthYearGreaterThan(int year)
        {
            var data = _personService.GetPeopleByBirthYearGreaterThan(year);
            return new JsonResult(data);
        }

        public IActionResult GetMemberByBirthYearLessThan(int year)
        {
            var data = _personService.GetPeopleByBirthYearLessThan(year);
            return new JsonResult(data);
        }

        public IActionResult FilterMember(int year, string type)
        {
            switch (type)
            {
                case "equalsTo":
                    return RedirectToAction("GetMemberByBirthYear", new { year });
                case "greaterThan":
                    return RedirectToAction("GetMemberByBirthYearGreaterThan", new { year });
                case "lessThan":
                    return RedirectToAction("GetMemberByBirthYearLessThan", new { year });
                default:
                    return RedirectToAction("Index");
            }
        }

        #endregion

        public IActionResult Export()
        {
            // var buffer = _personService.GetDataStream();
            // var stream = new MemoryStream(buffer);
            // return new FileStreamResult(stream, "text/csv") { FileDownloadName = "member.csv" };

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("FirstName"),
                                            new DataColumn("LastName"),
                                            new DataColumn("Gender"),
                                            new DataColumn("DateOfBirth"),
                                            new DataColumn("PhoneNumber"),
                                            new DataColumn("BirthPlace"),
                                            new DataColumn("IsGraduated") });

            var customers = _personService.GetAll();

            foreach (var customer in customers)
            {
                dt.Rows.Add(customer.FirstName, customer.LastName, customer.Gender, customer.DateOfBirth, customer.PhoneNumber, customer.BirthPlace, customer.IsGraduated);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
}