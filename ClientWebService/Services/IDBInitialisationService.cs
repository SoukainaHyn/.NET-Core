using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Services.Interfaces
{
    interface IDBInitialisationService
    {
        /// <summary>
        ///  Ajouter quelques données dans la BD
        /// </summary>
        ///
        void AlimenterDonnees();

        /// <summary>
        /// Initialiser la BD en Appliquant toutes les Migrations en attente pour le Context de la DB !
        /// La BD sera creée si elle n'existe pas 
        /// </summary>
        void Initialiser();
    }
}
