using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientWebService.Repositories.Interfaces;
using ClientWebService.Data.Models;
using ClientWebService.Services.Interfaces;

namespace adresseWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {

        private readonly IAdresseService adresseService;

        public AdressesController(IAdresseService adresseService)
        {

            this.adresseService = adresseService;
        }

        /// <summary>
        /// Retourne l'ensemble des adresses
        /// </summary>
        /// <returns></returns>
        /// <remarks> La methode GetAllAdresses</remarks>
        //GET: api/Adresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            return Ok(await adresseService.GetAll());
        }

        /// <summary>
        /// Retourne une adresses avec son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Adresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adresse>> Getadresse(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Adresse adresse = await adresseService.GetByKey(id);

            if (adresse == null)
            {
                return NotFound();
            }


            return Ok(adresse);
        }

        /// <summary>
        /// Modifier une adresse
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adresse"></param>
        /// <returns></returns>
        // PUT: api/Adresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putadresse(string id, Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(adresse.Id))
            {
                return BadRequest();
            }

            try
            {
                var e = await adresseService.Update(id, adresse);
                if (e != null)
                {
                    return Ok(e);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await adresseService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Ajouter une adresse
        /// </summary>
        /// <param name="adresse"></param>
        /// <returns></returns>
        // POST: api/Adresses
        [HttpPost]
        public async Task<ActionResult<Adresse>> Postadresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var e = await adresseService.Insert(adresse);
            if (e != null)
            {
                return Ok(e);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Ajouter une liste des adresses
        /// </summary>
        /// <param name="adresses"></param>
        /// <returns></returns>
        // POST: api/Adresses/list
        [HttpPost("{list}")]
        public async Task<ActionResult<Adresse>> PostListClient([FromBody]IEnumerable<Adresse> adresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             await adresseService.InsertList(adresses);
            return adresses.FirstOrDefault();
        }

        /// <summary>
        /// Supprimer une adresse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Adresse>> Deleteadresse(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adresse = await adresseService.GetByKey(id);
            if (adresse == null)
            {
                return NotFound();
            }

            await adresseService.Delete(adresse);

            return Ok(adresse);
        }

        /// <summary>
        /// Telecharger le liste des adresses en format PDF
        /// </summary>
        /// <returns></returns>
        // GET: api/Adresse/pdf
        [HttpGet("pdf")]
        public async Task<IActionResult> TelechargerListePDF()
        {
            // Option 1: Creer et retourner un fichier à telecharger directement
            var file = await adresseService.GetInPDFBinaryFileAsync();
            if (file != null)
            {
                return File(file, "application/pdf", "Clients_liste.pdf");
            }
            return NoContent();
        }

        /// <summary>
        /// Imprimer la liste des adresses PDF
        /// </summary>
        /// <returns></returns>
        // GET: api/Adresse/pdf/visuel
        [HttpGet("pdf/visuel")]
        public async Task<IActionResult> ImprimerListePDF()
        {
            // Option 2: Creer et retourner un fichier à afficher dans le navigateur
            var file = await adresseService.GetInPDFBinaryFileAsync();
            if (file != null)
            {
                return File(file, "application/pdf");
            }

            return NoContent();
        }
    }
}
