using MedidorModel.DAL;
using MedidorModel.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Medidores
{
    class HebraCliente
    {
        private ClienteCom clienteCom;
        private static IMedidorDAL medidorDAL = MedidorDALLectura.GetInstacia();

        public HebraCliente(ClienteCom cliente)
        {
            this.clienteCom = cliente;
        }

        public void Ejecutar()
        {

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
}
