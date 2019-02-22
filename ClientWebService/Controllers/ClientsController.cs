using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientWebService.Data;
using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using ClientWebService.Services.Interfaces;

namespace ClientWebService.Controllers
{
    [Produces("application/json")]//déclarer que les actions du contrôleur prennent en charge une réponse de type de contenu json
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAdresseService _adresseService;
        private readonly IContactService _contactService;

        public ClientsController(IClientService clientService, IContactService contactService, IAdresseService adresseService)
        {
            _clientService = clientService;
            _adresseService = adresseService;
            _contactService = contactService;

        }


        /// <summary>
        /// Retourne tous les clients
        /// </summary>
        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return Ok(await _clientService.GetAll());
        }

        ///// <summary>
        ///// Retourne un client specifique à partir de son id
        ///// </summary>
        ///// <remarks>Je manque d'imagination</remarks>
        ///// <param name="id">id du client a retourné</param>   
        ///// <response code="200">client selectionné</response>
        ///// <response code="404">client introuvable pour l'id specifié</response>
        ///// <response code="500">Oops! le service est indisponible pour le moment</response>
        //[HttpGet("{id}", Name = "GetById")]
        //[ProducesResponseType(typeof(Client), 200)]
        //[ProducesResponseType(typeof(NotFoundResult), 404)]
        //[ProducesResponseType(typeof(void), 500)]

            /// <summary>
            /// Retourne un client avec son Id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await _clientService.GetByKey(id);

            if (client == null)
            {
                return NotFound();
            }


            return Ok(client);
        }
        /// <summary>
        /// Modifier un client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(string id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(client.Id))
            {
                return BadRequest();
            }

            try
            {
                var e = await _clientService.Update(id, client);
                if (e != null)
                {
                    return Ok(e);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _clientService.Exists(id))
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
        /// Ajouter un client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        // POST: api/Clients
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var e = await _clientService.Insert(client);
            if (e != null)
            {
                return Ok(e);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Ajouter une liste des clients
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        // POST: api/Clients/list
        [HttpPost("{list}")]
        public async Task<ActionResult<Client>> PostListClient([FromBody]IEnumerable<Client> clients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             await _clientService.InsertList(clients);

            return clients.FirstOrDefault();
        }

        /// <summary>
        /// Supprime le client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _clientService.GetByKey(id);
            if (client == null)
            {
                return NotFound();
            }
            await _adresseService.DeleteWhere( x=> x.ClientId == id);//Using les arbres d'expressions LINQ manuellement (methode DeleteWhere(Expression<>))
            await _contactService.RemoveContactByClient(id);
            await _clientService.Delete(client);

            return client;
        }

        /// <summary>
        /// Telecharger la liste des client en format PDF
        /// </summary>
        /// <returns></returns>
        // GET: api/Clients/pdf
        [HttpGet("pdf")]
        public async Task<IActionResult> TelechargerListePDF()
        {
                // Option 1: Creer et retourner un fichier à telecharger directement
                var file = await _clientService.GetInPDFBinaryFileAsync();
                if (file != null)
                {
                    return File(file, "application/pdf", "Clients_liste.pdf");
                }
            return NoContent();
        }

        /// <summary>
        /// Imprimer la liste des clients PDF
        /// </summary>
        /// <returns></returns>
        // GET: api/Clients/pdf/visuel
        [HttpGet("pdf/visuel")]
        public async Task<IActionResult> ImprimerListePDF()
        {
            // Option 2: Creer et retourner un fichier à afficher dans le navigateur
            var file = await _clientService.GetInPDFBinaryFileAsync();
            if (file != null)
            {
                return File(file, "application/pdf");
            }

            return NoContent();
        }
    }

}

