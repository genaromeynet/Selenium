using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public class Configuracion
    {
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public decimal PresupuestoMaximo { get; set; }
        public string ProductoSeleccionado { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public int codigoPostal { get; set; }


        public void SeleccionarProducto(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    ProductoSeleccionado = "Sauce Labs Backpack";
                    break;

                case 2:
                    ProductoSeleccionado = "Sauce Labs Bike Light";
                    break;

                case 3:
                    ProductoSeleccionado = "Sauce Labs Bolt T-Shirt";
                    break;

                case 4:
                    ProductoSeleccionado = "Sauce Labs Fleece Jacket";
                    break;

                case 5:
                    ProductoSeleccionado = "Sauce Labs Onesie";
                    break;

                case 6:
                    ProductoSeleccionado = "Test.allTheThings() T-Shirt (Red)";
                    break;
            }
        }
    }

}