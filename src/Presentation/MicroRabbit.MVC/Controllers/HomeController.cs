﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroRabbit.MVC.Models;
using MicroRabbit.MVC.Services;
using MicroRabbit.MVC.Models.DTO;

namespace MicroRabbit.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransferService _transferService;

        public HomeController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                TransferDto transferDto = new TransferDto()
                {
                    FromAccount = model.FromAccount,
                    ToAccount = model.ToAccount,
                    TransferAmount = model.TransferAmount
                };

                await _transferService.Transfer(transferDto);
            }

            return View("Index");
        }
    }
}
