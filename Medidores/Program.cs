using MedidorModel.DAL;
using MedidorModel.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medidores
{
    class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDALLectura.GetInstacia();

        static bool Menu()
        {
            bool continuar = true;
            string fecha = ObtenerFecha();
            Console.WriteLine("¿Que necesita hacer?");
            Console.WriteLine(" 1. Ingresar \n 2. Mostrar \n 3. Abrir cliente \n 0. Salir \n");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }


        static void Main(string[] args)
        {
            //1.- Iniciar el servidor en el puerto 3000
            do 
            {
                HebraServidor hebra = new HebraServidor();
                Thread t = new Thread(new ThreadStart(hebra.EjecutarServidor));
                t.IsBackground = false;
                t.Start();
            }
            while (Menu()) ;
        }

        static void Ingresar()
        {
            Console.WriteLine("Ingrese nombre :");
            string nombre = Console.ReadLine().Trim();
            string fecha = ObtenerFecha().Trim();
            Console.WriteLine("Ingrese Lectura :");
            string lectura = Console.ReadLine().Trim();
            Medidor medidor = new Medidor()
            {
                Nombre = nombre,
                Fecha = fecha,
                Lectura = lectura,
                Tipo = "Aplicacion"
            };
            medidorDAL.AgregarMedidor(medidor);
        }

        static void Mostrar()
        {
            List<Medidor> medidores = medidorDAL.ObtenerMedidor();
            foreach (Medidor medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
        }
        static string ObtenerFecha()
        {
            var dateTime = DateTime.Now;
            var Date = dateTime.ToString("dd-MM-yyyy HH:mm:ss");
            return Date;
        }
        
    }
}

