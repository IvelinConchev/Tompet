﻿namespace Tompet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Tompet.Infrastructure.Data;
    using Tompet.Infrastructure.Data.Models;
    using Tompet.Models.Companies;

    public class CompaniesController : Controller
    {
        private readonly TompetDbContext data;

        public CompaniesController(TompetDbContext data) => this.data = data;
        
        public IActionResult Add() => View(new AddCompaniesFormModel());

        //public IActionResult All()
        //{

        //}

        [HttpPost]
        public IActionResult Add(AddCompaniesFormModel company)
        {
            if (!ModelState.IsValid)
            {
                
                return View(company);
            }

            var companyData = new Company
            {
                Name = company.Name,
                Manager = company.Manager,
                Phone = company.Phone,
                Address = company.Address,
            };

            this.data.Companies.Add(companyData);

            this.data.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
