using MedidorModel.Cliente;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidores
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectando a servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectado...");
                String mensaje = clienteSocket.Leer();
                Console.WriteLine(mensaje);
                string respuesta = Console.ReadLine().Trim();
                clienteSocket.Escribir(respuesta);
                mensaje = clienteSocket.Leer();
                Console.WriteLine(mensaje);
                respuesta = Console.ReadLine().Trim();
                clienteSocket.Escribir(respuesta);
                mensaje = clienteSocket.Leer();
                Console.WriteLine(mensaje);
                respuesta = Console.ReadLine().Trim();
                clienteSocket.Escribir(respuesta);


            }
            else
            {
                Console.WriteLine("Error de comunicacion...");
            }
            Console.ReadKey();

        }
    }
}