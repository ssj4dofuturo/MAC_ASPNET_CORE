﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mvc_BO.Models;

namespace Mvc_BO.Controllers
{
    public class HomeController : Controller
    {
        private IAlunoBLL alunoBll;
        public HomeController(IAlunoBLL _alunoBLL)
        {
            alunoBll = _alunoBLL;
        }

        public IActionResult Index()
        {
            AlunoBLL _aluno = new AlunoBLL();

            List<Aluno> alunos = _aluno.GetAlunos().ToList();

            return View("Lista",alunos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            //AlunoBLL alunoBll = new AlunoBLL();
            Aluno aluno = alunoBll.GetAlunos().Single(x => x.Id == id);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            [Bind(nameof(Aluno.Id), nameof(Aluno.Sexo), nameof(Aluno.Email), nameof(Aluno.Nascimento))] 
        Aluno aluno)
        {
            aluno.Nome = alunoBll.GetAlunos().Single(a => a.Id == aluno.Id).Nome;

            if (ModelState.IsValid)
            {
                //AlunoBLL alunoBll = new AlunoBLL();
                alunoBll.AtualizarAluno(aluno);
                return RedirectToAction("Index");
            }
            {
                return View(aluno);
            }
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            //if (aluno?.Nome == null || aluno?.Email == null || aluno.Sexo == null)
            //{
            //    ViewBag.Erro = "Dados inválidos!";
            //    return View();
            //}
            //else
            //{

            //if (string.IsNullOrEmpty(aluno.Nome))
            //    ModelState.AddModelError("Nome","O nome é obrigatório");

            //if (string.IsNullOrEmpty(aluno.Sexo))
            //    ModelState.AddModelError("Sexo", "O Sexo é obrigatório");

            //if (string.IsNullOrEmpty(aluno.Email))
            //    ModelState.AddModelError("Email", "O Email é obrigatório");

            //if (aluno.Nascimento <= DateTime.Now.AddYears(-18))
            //    ModelState.AddModelError("Nascimento", "Data de nascimento inválida");

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                //AlunoBLL _aluno = new AlunoBLL();
                alunoBll.IncluirAluno(aluno);
                return RedirectToAction("Index");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    Aluno aluno = alunoBll.GetAlunos().Single(a => a.Id == id);
        //    return View(aluno);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            alunoBll.DeletarAluno(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Aluno aluno = alunoBll.GetAlunos().Single(a => a.Id == id);
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Procurar(string procurarPor, string criterio)
        {
            if (procurarPor == "Email")
            {
                Aluno aluno = alunoBll.GetAlunos().SingleOrDefault(a=>a.Email == criterio);
                return View(aluno);
            }
            else
            {
                Aluno aluno = alunoBll.GetAlunos().SingleOrDefault(a => a.Nome == criterio);
                return View(aluno);
            }
        }
    }
}
