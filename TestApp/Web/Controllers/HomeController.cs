using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var st = new Stopwatch();
            st.Start();

            for (int i = 0; i < 10; i++)
            {
                var aa = await GetE2Data();
            }
            //var aa = await GetE2Data();
            //var bb = await GetE2Data();
            //var cc = await GetE2Data();
            //var dd = await GetE2Data();
            //var ee = await GetE2Data();


            st.Stop();


            var a = st.ElapsedMilliseconds;
            st = new Stopwatch();
            st.Start();


            //var aaa = GetE2Data();
            //var bbb = GetE2Data();
            //var ccc = GetE2Data();
            //var ddd = GetE2Data();
            //var eee = GetE2Data();
            var tList = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var t = GetE2Data();
                tList.Add(t);
            }
            await Task.WhenAll(tList);

            //await Task.WhenAll(aaa, bbb, ccc, ddd, eee);

            
            st.Stop();
            var b = st.ElapsedMilliseconds;
            //var cuss = new List<Customer>();
            //for (int i = 0; i < 1000000; i++)
            //{
            //    var cus = new Customer
            //    {
            //        Name = "abc" + i
            //    };
            //    cuss.Add(cus);
            //}


            //Parallel.ForEach(cuss, c =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionsBuilder.UseSqlServer("Server=DESKTOP-9PQ7I76;User Id=sa;Password=abc-1234;Database=test_app_db;MultipleActiveResultSets=true");
            //    using (var db = new ApplicationDbContext(optionsBuilder.Options))
            //    {
            //        db.Customers.Add(c);
            //        db.SaveChanges();
            //    }
            //});
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<Customer>> GetE2Data()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-9PQ7I76;User Id=sa;Password=abc-1234;Database=test_app_db;MultipleActiveResultSets=true");
            using (var db = new ApplicationDbContext(optionsBuilder.Options))
            {
                return await db.Customers.ToListAsync();
            }
        }
    }
}
