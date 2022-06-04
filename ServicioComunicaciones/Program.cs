using MedidorModel;
using MedidorModel.DAL;
using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicaciones
{
    class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDALLectura.GetInstacia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("¿Que necesita hacer?");
            Console.WriteLine(" 1. Mostrar solo medidores \n 2. Mostrar Lecturas completas  \n 0. Salir \n");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    MostrarMedidor();
                    break;
                case "2":
                    MostrarLectura();
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
         
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.EjecutarServidor));
            t.IsBackground = false;
            t.Start();

            while (Menu());
        }

        static void MostrarLectura()
        {
            List<Lecturas> lecturas = medidorDAL.ObtenerLecturas();
            foreach (Lecturas lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }
        }

        static void MostrarMedidor()
        {
            List<Medidor> medidores = medidorDAL.ObtenerMedidor();
            foreach (Medidor medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
        }
        
    }
}

