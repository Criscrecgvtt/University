using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caso1_2024
{
    internal class Program
    {   public abstract class Persona
        {
            private String Apellidos { get; set; }
            private String Nombre { get; set; }
            private String Dni { get; set; }
            private String Email { get; set; }
            private String Telefono { get; set; }
            protected Persona(string apellidos, string nombre, string dni, string email, string telefono)
            {
                Apellidos = apellidos;
                Nombre = nombre;
                Dni = dni;              //Habría que comprobar que es un dato válido y lanzar excepción si no lo es
                Email = email;          //Habría que comprobar que es un dato válido y lanzar excepción si no lo es
                Telefono = telefono;    //Habría que comprobar que es un dato válido y lanzar excepción si no lo es
            }
        }
        public class Cliente: Persona
        {
            private string Password { get; set; }
            private DateTime FechaNacimiento { get; set; }
            //Relaciones
            private ICollection<Compra> compras;
            public void AddCompra(Compra compra)
            {
                if (compra != null)
                    compras.Add(compra);
                
            }
            
            public Cliente(string apellidos, string nombre, string dni, string email, 
                    string telefono, string password, 
                    DateTime fechaNacimiento): base (apellidos, nombre,dni,email,telefono) 
            {
                Password = password;
                FechaNacimiento = fechaNacimiento;

                compras = new List<Compra>();
                
            }
        }
        public class Miembro : Persona
        {
          
            private String NombreArtistico { get; set; }
            private String Descripcion { get; set; }
            //Relaciones
            private ICollection<Grupo> gruposFormaParte;
            public void AddGrupo(Grupo grupo)
            {
                if (grupo != null)
                    gruposFormaParte.Add(grupo);
                else
                    throw new ArgumentNullException(nameof(grupo), "El grupo a añadir no puede ser nulo");
            }
            public Miembro(string apellidos, string nombre, string dni, string email, 
                string telefono, string nombreArtistico, 
                string descripcion) : base(apellidos, nombre, dni, email, telefono) //mínima 1 a 1 con Grupo, Relajo de ese lado
            {
                NombreArtistico = nombreArtistico;
                Descripcion = descripcion;
                gruposFormaParte = new List<Grupo>();
            }


        }
        public class Entrada 
        {
            private int Numero { get; set; }
            private String Nombre { get; set; }
            private double ImporteFinal { get; set; }
            //Relaciones
            private Compra CompraCliente { get; set; }
            private Concierto Concierto { get; set; }
            public void AddCompra(Compra compra)
            {
                this.CompraCliente = compra;
                if (compra.Descuento != 0)
                    this.ActualizarImporteFinal();
            }
            public void ActualizarImporteFinal()
            {

                
                if (this.CompraCliente == null)
                    ImporteFinal = this.Concierto.PrecioBase;

                else
                    ImporteFinal = (1 - this.CompraCliente.Descuento) * this.Concierto.PrecioBase;

                
            }

            public Entrada(int numero, string nombre, Concierto concierto) //mínimo 1 a 1 con compra, relajamos de este lado
            {
                Numero = numero;
                Nombre = nombre;
                //Relaciones
                Concierto = concierto;

               ActualizarImporteFinal();
            }
        }
        public class Compra
        {
            private DateTime Fecha { get; set; }
            public double Descuento { get; set; }

            //Relaciones
            private Cliente ClienteCompra { get; set; }
            private Entrada EntradaCompra { get; set; }


            public Compra(DateTime fecha, double descuento, Cliente cliente, Entrada entrada)
            {
                Fecha = fecha;

                if(descuento >= 0 && descuento <=1 )
                    Descuento = descuento;
                else
                    throw new ArgumentOutOfRangeException(nameof(descuento), "El descuento debe ser un valor decimal entre 0 y 1");
                //Relaciones
                ClienteCompra = cliente;
                EntradaCompra = entrada;
            }
        }
        public class Concierto
        {
            private String Nombre { get; set; }
            private String Descripcion { get; set; }
            private DateTime Fecha { get; set; }
            private DateTime FechaVenta { get; set; }
            private DateTime HoraApertura { get; set; }
            private DateTime HoraInicio { get; set; }
            private DateTime Duracion { get; set; }
            public double PrecioBase { get; set; }

            //Relaciones
            private ICollection<Entrada> entradasConcierto;
            private ICollection<Grupo> gruposConcierto;
            public void AddGrupo(Grupo grupo)
            {
                if (grupo != null)
                    gruposConcierto.Add(grupo);
                else
                    throw new ArgumentNullException(nameof(grupo), "El grupo a añadir no puede ser nulo");
            }
            public void AddEntrada(Entrada entrada)
            {
                if (entrada != null)
                    entradasConcierto.Add(entrada);
                else
                    throw new ArgumentNullException(nameof(entrada), "La entrada a añadir no puede ser nulo");
            }

            public Concierto(string nombre, string descripcion, DateTime fecha, DateTime fechaVenta, 
                DateTime horaApertura, DateTime horaInicio, DateTime duracion, double precioBase, Grupo grupo)
            {
                Nombre = nombre;
                Descripcion = descripcion;
                Fecha = fecha;
                FechaVenta = fechaVenta;
                HoraApertura = horaApertura;
                HoraInicio = horaInicio;
                Duracion = duracion;
                PrecioBase = precioBase;

                entradasConcierto = new List<Entrada>();
                gruposConcierto = new List<Grupo>();
                gruposConcierto.Add(grupo);
            }
        }
        public class Grupo
        {
            private String Nombre { get; set; }
            private String Descripcion { get; set; }
            //Relaciones
            ICollection<Concierto> conciertosGrupo;
            ICollection<Miembro> miembrosGrupo;

            public void AddConcierto(Concierto concierto)
            {
                if (concierto != null)
                    conciertosGrupo.Add(concierto);
                else
                    throw new ArgumentNullException(nameof(concierto), "El concierto a añadir no puede ser nulo");
                

            }
            public void AddMiembro(Miembro miembro)
            {
                if (miembro != null)
                    miembrosGrupo.Add(miembro);
                else
                    throw new ArgumentNullException(nameof(miembro), "El miembro a añadir no puede ser nulo");


            }

            public Grupo(string nombre, string descripcion, Miembro miembro)
            {
                Nombre = nombre;
                Descripcion = descripcion;
                conciertosGrupo = new List<Concierto>();
                miembrosGrupo = new List<Miembro>();
                miembrosGrupo.Add(miembro);
            }
        }
        static void Main(string[] args)
        {
            Miembro cantante = new Miembro("García", "Manolo", "37567473D", "manoloGarcia@gmail.com",
                "+3456789678","Manolo García", "Cantante y compositor");
            Grupo grupo = new Grupo("Manolo García y su banda", "´Manolo García en directo", cantante);
            //aseguramos min 1 a 1 relajado en Miembro
            cantante.AddGrupo(grupo);

            DateTime fechaConcierto = new DateTime(2024, 10, 24, 22, 30, 0);
            Concierto concierto = new Concierto("ManoloTour Valencia", "Concierto de Manolo García en Valencia", fechaConcierto ,
                                   fechaConcierto.AddDays(-31), fechaConcierto.AddMinutes(-60), fechaConcierto,
                                   fechaConcierto.AddMinutes(120), 25, grupo);
            //Cumplimos el modelo
            grupo.AddConcierto(concierto);
            int numEntradas = 0;

            Entrada entrada = new Entrada(++numEntradas, "Soledad Valero", concierto);
            //Cumplimos el modelo
            concierto.AddEntrada(entrada);

            Cliente cliente = new Cliente("Valero", "Soledad", "74036884E", "svalero@dsic.upv.es",
                "6574567", "master@ticket", new DateTime(1977, 02, 10));

            Compra compra = new Compra(DateTime.Now,0.1, cliente, entrada);
            //aseguramos min 1 a 1
            entrada.AddCompra(compra);

            //Cumplimos el modelo
            cliente.AddCompra(compra);

            Console.WriteLine("Instancia del modelo creada. Pulse intro para terminar");
            Console.ReadLine();


        }
    }
}
