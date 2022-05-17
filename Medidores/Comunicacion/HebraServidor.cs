using MedidorModel.DAL;
using MedidorModel.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medidores
{
    public class HebraServidor
    {
        

        private static IMedidorDAL medidorDAL = MedidorDALLectura.GetInstacia();
        public void EjecutarServidor()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.WriteLine("S: Lenvantando servidor en puerto: {0}", puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando Cliente..");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido \n");
                    ClienteCom clienteCom = new ClienteCom(cliente);
                    
                    clienteCom.Escribir("Ingrese nombre : ");
                    string nombre = clienteCom.Leer();
                    clienteCom.Escribir("Ingrese fecha: ");
                    string fecha = clienteCom.Leer();
                    clienteCom.Escribir("Ingrese lectura: ");
                    string lectura = clienteCom.Leer();
                    Medidor medidor = new Medidor()
                    {
                        Nombre = nombre,
                        Fecha = fecha,
                        Lectura = lectura,
                        Tipo = "TCP"
                    };
                    medidorDAL.AgregarMedidor(medidor);
                    clienteCom.Desconectar();
                }
            }
            else
            {
                Console.WriteLine("ERROR, no se puede levantar server en puerto: {0}", puerto);
            }
        }
    }
}