using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidorDALLectura : IMedidorDAL
    {
        //pra implementar Singleton:
        //1. El contructor tiene que ser private
        private MedidorDALLectura()
        {

        }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static MedidorDALLectura instancia;
        //3. Tener un metodo GetInstance, que devuelve una referencia al atributo
        public static IMedidorDAL GetInstacia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALLectura();
            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";
        public void AgregarLectura(Lecturas lectura)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(lectura.Nombre + "|" + lectura.Fecha + "|" + lectura.Lectura + "|");
                    writer.Flush();
                }
            }
            catch (Exception)
            {

            }

        }

        public List<Lecturas> ObtenerLecturas()
        {

            List<Lecturas> lista = new List<Lecturas>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split('|');
                            Lecturas lectura = new Lecturas()
                            {
                                Nombre = arr[0],
                                Fecha = arr[1],
                                Lectura = arr[2]
                            };
                            lista.Add(lectura);
                        }

                    } while (texto != null);
                }
            }
            catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
        public List<Medidor> ObtenerMedidor()
        {
            Medidor medidor = new Medidor();
            List<Medidor> lista = new List<Medidor>();
            try
            {
                lista.Add(new Medidor() { Nombre = "01M"});
                lista.Add(new Medidor() { Nombre = "02M" });
                lista.Add(new Medidor() { Nombre = "03M" });
                lista.Add(new Medidor() { Nombre = "04M" });
            }
            catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
    }
}
