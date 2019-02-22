using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaysWebService.Services
{
    public class HTMLGeneratorAdresses
    {
        /// <summary>
        /// Construire le HTML pour la liste des Regions
        /// </summary>
        /// <param name="adresses"></param>
        /// <returns></returns>
        public static string GetHTMLString(IEnumerable<Adresse> adresses)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Liste des Adresses</h1></div>
                                <table align='center' style='border:2px solid black'>
                                    <tr>
                                        <th>Code</th>
                                        <th>CodePostal</th>
                                    </tr>");

            foreach (var adresse in adresses)
            {
                stringBuilder.AppendFormat(@"<tr>
                                    <td class='centre'>{0}</td>
                                    <td class='centre'>{1}</td>
                                  </tr>",
                                  adresse.Code,
                                  adresse.CodePostal);
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
