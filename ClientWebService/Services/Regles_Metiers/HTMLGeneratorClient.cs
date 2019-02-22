using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaysWebService.Services
{
    public class HTMLGeneratorClient
    {
        /// <summary>
        /// Construire le HTML pour la liste des Clients
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public static string GetHTMLString(IEnumerable<Client> clients)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Liste des Clients</h1></div>
                                <table align='center' style='border:2px solid black'>
                                    <tr>
                                        <th>Code</th>
                                        <th>Nom</th>
                                    </tr>");

            foreach (var c in clients)
            {
                stringBuilder.AppendFormat(@"<tr>
                                    <td class='centre'>{0}</td>
                                    <td class='centre'>{1}</td>
                                  </tr>",
                                  c.Code,
                                  c.Nom);
            }

            stringBuilder.Append(@"
                                </table>
                                <p></p>
                            </body>
                        </html>");

            return stringBuilder.ToString();
        }
    }
}
