using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using ClientWebService.Services.Interfaces;

namespace contactWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {

            this.contactService = contactService;
        }

        /// <summary>
        /// Retourne l'ensemble des contacts
        /// </summary>
        /// <returns></returns>
        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return Ok(await contactService.GetAll());
        }

        /// <summary>
        /// Retourne un contact avec son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact contact = await contactService.GetByKey(id);

            if (contact == null)
            {
                return NotFound();
            }


            return Ok(contact);
        }

        /// <summary>
        /// Modifier un contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcontact(string id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(contact.Id))
            {
                return BadRequest();
            }

            try
            {
                var e = await contactService.Update(id, contact);
                if (e != null)
                {
                    return Ok(e);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await contactService.Exists(id))
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
        /// Ajouter un contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> Postcontact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var e = await contactService.Insert(contact);
            if (e != null)
            {
                return Ok(e);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Ajouter un liste des contacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        // POST: api/Adresses/list
        [HttpPost("{list}")]
        public async Task<ActionResult<Contact>> PostListClient([FromBody]IEnumerable<Contact> contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await contactService.InsertList(contacts);
            return contacts.FirstOrDefault();
        }

        /// <summary>
        /// Supprimer un contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Deletecontact(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await contactService.GetByKey(id);
            if (contact == null)
            {
                return NotFound();
            }

            await contactService.Delete(contact);

            return Ok(contact);
        }

        /// <summary>
        /// Telecharger la liste des contacts en format PDF
        /// </summary>
        /// <returns></returns>
        // GET: api/Contacts/pdf
        [HttpGet("pdf")]
        public async Task<IActionResult> TelechargerListePDF()
        {
            // Option 1: Creer et retourner un fichier à telecharger directement
            var file = await contactService.GetInPDFBinaryFileAsync();
            if (file != null)
            {
                return File(file, "application/pdf", "Contacts_liste.pdf");
            }
            return NoContent();
        }

        /// <summary>
        /// Imprimer la liste des contacts PDF
        /// </summary>
        /// <returns></returns>
        // GET: api/Contacts/pdf/visuel
        [HttpGet("pdf/visuel")]
        public async Task<IActionResult> ImprimerListePDF()
        {
            // Option 2: Creer et retourner un fichier à afficher dans le navigateur
            var file = await contactService.GetInPDFBinaryFileAsync();
            if (file != null)
            {
                return File(file, "application/pdf");
            }

            return NoContent();
        }
    }
}
