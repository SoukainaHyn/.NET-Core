using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaysWebService.Services
{
    public class HTMLGeneratorContacts
    {
        /// <summary>
        /// Construire le HTML pour la liste des Contacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public static string GetHTMLString(IEnumerable<Contact> contacts)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Liste des Contacts</h1></div>
                                <table align='center' style='border:2px solid black'>
                                    <tr>
                                        <th>Code</th>
                                        <th>Prenom</th>
                                    </tr>");

            foreach (var contact in contacts)
            {
                stringBuilder.AppendFormat(@"<tr>
                                    <td class='centre'>{0}</td>
                                    <td class='centre'>{1}</td>
                                  </tr>",
                                  contact.Code,
                                  contact.Prenom);
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
