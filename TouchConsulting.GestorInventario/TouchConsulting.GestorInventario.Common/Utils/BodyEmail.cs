using System;
using System.Collections.Generic;
using System.Text;
using TouchConsulting.GestorInventario.Common.Interfaces;

namespace TouchConsulting.GestorInventario.Common.Utils
{
  

    public static class BodyEmail
    {
        public static string BuildMai(List<IProduct> lowStockProducts) { 
            string mailBody = @"
            <html>
            <head>
                <style>
                    table {
                        border-collapse: collapse;
                        width: 100%;
                        font-family: Arial, sans-serif;
                    }
                    th, td {
                        border: 1px solid #dddddd;
                        padding: 8px;
                        text-align: left;
                    }
                    th {
                        background-color: #f2f2f2;
                    }
                    tr:nth-child(even) {
                        background-color: #f9f9f9;
                    }
                </style>
            </head>
            <body>
                <table>
                    <tr>
                        <th>Producto</th>
                        <th>Stock</th>
                    </tr>";

            // Iterar sobre los productos de bajo stock
            foreach (var product in lowStockProducts)
            {
                mailBody += $"<tr><td>{product.Nombre}</td><td>{product.CantidadInventario}</td></tr>";
            }

            mailBody += @"
                </table>
            </body>
            </html>";

            return mailBody;
        }
    }
}
