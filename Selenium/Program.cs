using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Selenium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Configuracion configuracion = new Configuracion();

                configuracion.Usuario = "standard_user";
                Console.Write("Usuario: " + configuracion.Usuario + "\n");

                configuracion.Contraseña = "secret_sauce";
                Console.Write("Contraseña: " + configuracion.Contraseña + "\n");

                Navegador navegador = new Navegador();

                navegador.IrA("https://www.saucedemo.com/");

                Thread.Sleep(2000);

                IWebElement usuario = navegador.BuscarPorId("user-name");
                IWebElement contraseña = navegador.BuscarPorId("password");

                navegador.Escribir(usuario, configuracion.Usuario);
                navegador.Escribir(contraseña, configuracion.Contraseña);

                Thread.Sleep(1000);

                IWebElement botonLogin = navegador.BuscarPorId("login-button");

                navegador.HacerClick(botonLogin);


                Console.WriteLine("\n");
                Console.WriteLine("Productos disponibles:");
                Console.WriteLine("1 - Backpack");
                Console.WriteLine("2 - Bike Light");
                Console.WriteLine("3 - Bolt T-Shirt");
                Console.WriteLine("4 - Fleece Jacket");
                Console.WriteLine("5 - Onesie");
                Console.WriteLine("6 - Red T-Shirt");

                bool continuar = true;

                while (continuar)
                {

                    Console.Write("Seleccione un producto: ");

                    int opcion;

                    while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 6)
                    {
                        Console.Write("Opción inválida. Ingrese un número del 1 al 6: ");
                    }

                    configuracion.SeleccionarProducto(opcion);

                    Console.WriteLine();
                    Console.WriteLine("Producto seleccionado: " + configuracion.ProductoSeleccionado);

                    Console.Write("\nPresupuesto máximo: $");
                    decimal presupuesto;

                    while (!decimal.TryParse(Console.ReadLine(), out presupuesto) || presupuesto < 0)
                    {
                        Console.Write("Ingrese un presupuesto válido: $");
                    }

                    configuracion.PresupuestoMaximo = presupuesto;

                    IReadOnlyCollection<IWebElement> productos =
                        navegador.BuscarPorClase("inventory_item");

                    bool productoAgregado = false;

                    foreach (IWebElement producto in productos)
                    {
                        IWebElement nombre = producto.FindElement(By.ClassName("inventory_item_name"));
                        IWebElement precio = producto.FindElement(By.ClassName("inventory_item_price"));

                        

                        if (nombre.Text == configuracion.ProductoSeleccionado)
                        {
                            decimal valorPrecio = decimal.Parse(
                            precio.Text.Replace("$", "").Replace(".", ",")
                        );

                        Console.WriteLine("\nProducto: " + nombre.Text);
                        Console.WriteLine("Precio: " + precio.Text);

                            if (valorPrecio <= configuracion.PresupuestoMaximo)
                            {
                                Console.WriteLine("\nProducto dentro del presupuesto\n");

                                IWebElement boton = producto.FindElement(
                                    By.TagName("button")
                                );

                                navegador.HacerClick(boton);

                                Console.WriteLine("Producto agregado al carrito\n");

                                Thread.Sleep(1000);

                                productoAgregado = true;

                                Console.WriteLine("--------------------\n");
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("El producto supera tu presupuesto. ");
                                Console.WriteLine();
                                Console.WriteLine(" Precio: " + precio.Text);
                                Console.WriteLine(" Presupuesto máximo: $" + configuracion.PresupuestoMaximo.ToString("0.00"));
                                Console.WriteLine("--------------------\n");
                            }
                            break;
                        }

                        
                    }

                    if (productoAgregado)
                    {
                        IWebElement carrito = navegador.BuscarUnoPorClase("shopping_cart_link");
                        navegador.HacerClick(carrito);

                        Thread.Sleep(1000);

                        IWebElement botonCheckout = navegador.BuscarPorId("checkout");
                        navegador.HacerClick(botonCheckout);

                        Console.WriteLine("Complete el formulario con sus datos para finalizar la compra\n");

                        Console.Write("Nombre: ");
                        configuracion.nombre = Console.ReadLine();

                        while (string.IsNullOrWhiteSpace(configuracion.nombre))
                        {
                            Console.Write("El nombre no puede estar vacío. Ingrese nuevamente: ");
                            configuracion.nombre = Console.ReadLine();
                        }

                        Console.Write("Apellido: ");
                        configuracion.apellido = Console.ReadLine();

                        while (string.IsNullOrWhiteSpace(configuracion.apellido))
                        {
                            Console.Write("El apellido no puede estar vacío. Ingrese nuevamente: ");
                            configuracion.apellido = Console.ReadLine();
                        }

                        Console.Write("Código postal: ");

                        int codigoPostal;

                        while (!int.TryParse(Console.ReadLine(), out codigoPostal) || codigoPostal < 0)
                        {
                            Console.Write("Ingrese un código postal válido: ");
                        }

                        configuracion.codigoPostal = codigoPostal;

                        IWebElement campoNombre = navegador.BuscarPorId("first-name");
                        IWebElement campoApellido = navegador.BuscarPorId("last-name");
                        IWebElement campoCodigoPostal = navegador.BuscarPorId("postal-code");

                        navegador.Escribir(campoNombre, configuracion.nombre);
                        navegador.Escribir(campoApellido, configuracion.apellido);
                        navegador.Escribir(campoCodigoPostal, configuracion.codigoPostal.ToString());

                        IWebElement botonContinuar = navegador.BuscarPorId("continue");
                        navegador.HacerClick(botonContinuar);

                        IWebElement botonFinalizar = navegador.BuscarPorId("finish");
                        navegador.HacerClick(botonFinalizar);

                        Console.WriteLine();
                        Console.WriteLine("========================================");
                        Console.WriteLine("           COMPRA REALIZADA");
                        Console.WriteLine("========================================");
                        Console.WriteLine();
                        Console.WriteLine("Producto: " + configuracion.ProductoSeleccionado);
                        Console.WriteLine();
                        Console.WriteLine("La automatización finalizó correctamente.");
                        Console.WriteLine();

                        continuar = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("El producto supera tu presupuesto.");
                        Console.WriteLine();
                        Console.WriteLine("¿Desea intentar con otro producto?");
                        Console.WriteLine("1 - Sí");
                        Console.WriteLine("2 - No");

                        int respuesta;

                        while (!int.TryParse(Console.ReadLine(), out respuesta) || respuesta < 1 || respuesta > 2)
                        {
                            Console.Write("Opción inválida. Ingrese 1 para intentar nuevamente o 2 para finalizar: ");
                        }


                        if (respuesta == 2)
                        {
                            continuar = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Ocurrió un problema durante la automatización.");
                Console.WriteLine();
                //chrome puede mostrar una ventana de seguridad (contraseña no segura) que bloquea la automatización, por lo que es necesario cerrarla manualmente para que el programa pueda continuar.
                Console.WriteLine("Si aparece una ventana de seguridad de Chrome,");
                Console.WriteLine("cerrala manualmente y vuelve a ejecutar el programa.");
                Console.WriteLine();
                Console.WriteLine("Detalle del error: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para finalizar...");
                Console.ReadLine();
            }
        }
    }
}