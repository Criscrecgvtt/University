using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioTema5_2
{
    class Program
    {
        public class Concesionario
        {
            private string nombre;
            private string direccion;

            private ICollection<Cliente> clientes; //Tenemos una cardinalidad máxima de N, necesitamos colección
            private ICollection<Modelo> modelos;   //Tenemos una cardinalidad máxima de N, necesitamos colección

            //Tenemos una cardinalidad mínima 1 a 1 con Modelo,  relajamos en este lado 
            // y eliminamos Modelo del constructor
            public Concesionario(string nombre, string direccion)
            {
                this.nombre = nombre;
                this.direccion = direccion;

                this.clientes = new List<Cliente>(); //cardinalidad mínima de cero, solo inicializar

                this.modelos = new List<Modelo>();
                //this.modelos.Add(modelo); --> tendrá que asegurarse por código
            }
            public void AddCliente(Cliente cliente)
            {
                clientes.Add(cliente);
            }

            public void AddModelo(Modelo modelo)
            {
                modelos.Add(modelo);
            }
        }

        public class Cliente
        {
            private string nombre;
            private string dni;

            private ICollection<Modelo> modelos; //Tenemos una cardinalidad máxima de N, necesitamos colección
            private Concesionario concesionario;

            //Cardinalidad minima de 1 con concesionario y con modelo, hay que pasarlos en el contructor
            public Cliente(string nombre, string dni, Concesionario concesionario, Modelo modelo)
            {
                this.nombre = nombre;
                this.dni = dni;
                this.concesionario = concesionario;

                this.modelos = new List<Modelo>();
                this.modelos.Add(modelo);

            }

        }

        public class Modelo
        {
            private string nombre;

            private ICollection<Concesionario> concesionarios;  //Tenemos una cardinalidad máxima de N, necesitamos colección
            private ICollection<Contiene> contienes; //El atributo de enlace en una relación N a N promociona a clase
            private ICollection<Opcion> opciones; //Tenemos una cardinalidad máxima de N, necesitamos colección
            private Cliente cliente; //cardinalidad maxima 1, minima cero, no hay que pasarlo en el constructor
            private Motor motor;


            private double importe; //atributo de la relación con cliente, se pone en el lado de muchos
            private DateTime fecha; //atributo de la relación con cliente, se pone en el lado de muchos

            //Cardinalidad minima de 1 a 1 con concesionario, lo pasamos al constructor y relajamos al otro lado
            //Cardinalidad mínimma de 1 a 1 con Motor, lo pasamos al contructor y relajamos al otro lado 
            //Cardinalidad mínima de a 1 a 1 con Opcion, lo pasamos al contructor y relajamos al otro lado	
            public Modelo(string nombre, double importe, DateTime fecha, Concesionario concesionario, Motor motor, Opcion opcion)
            {
                this.nombre = nombre;
                this.importe = importe;
                this.fecha = fecha;

                this.concesionarios = new List<Concesionario>();
                this.concesionarios.Add(concesionario);

                this.contienes = new List<Contiene>(); //cardinalidad minima de cero, solo inicializar la colección

                this.opciones = new List<Opcion>();
                this.opciones.Add(opcion);

                this.motor = motor;


            }
            public void AddContiene(Contiene contiene)
            {
                this.contienes.Add(contiene);
            }
            public void setCliente(Cliente cliente)
            {
                this.cliente = cliente;
            }

        }
        public class Contiene
        {
            //Esta clase surge al promocionar un atributo de una relacion N a N a clase
            private double descuento;

            private Modelo modelo;
            private Campanya campanya;

            //Cadinalidad minima de 1 a 1 con Campanya,  lo pasamos al constructor y relajamos al otro lado
            //Cardinalida mínima de 1 con Modelo
            public Contiene(double descuento, Modelo modelo, Campanya campanya)
            {
                this.descuento = descuento;
                this.modelo = modelo;
                this.campanya = campanya;
            }
        }

        public class Campanya
        {

            private ICollection<Contiene> contienes; //El atributo de enlace en una relación N a N promociona a clase

            //Cadinalidad minima de 1 a 1 con Contiene, relajamos en este lado, eliminando del constructor
            public Campanya()
            {
                this.contienes = new List<Contiene>();
                //this.contienes.Add(contiene); //Tendrá que asegurarse por código
            }
            public void AddContiene(Contiene contiene)
            {
                this.contienes.Add(contiene);
            }

        }

        public class Opcion
        {
            private double precio_anyadido;
            private int codigo;
            private ICollection<Modelo> modelos;//Tenemos una cardinalidad máxima de N, necesitamos colección
            private ICollection<Motor> motores;//Tenemos una cardinalidad máxima de N, necesitamos colección


            //Cardinalidad mínimma de 1 a 1 con modelo, hemos relajado en este lado
            public Opcion(double precio_anyadido, int codigo)
            {
                this.precio_anyadido = precio_anyadido;
                this.codigo = codigo;

                this.modelos = new List<Modelo>();
                //This.modelo.Add(modelo) ---> tendrá que asegurarse por código. Hemos relajado en este lado

                this.motores = new List<Motor>(); //cardinalidad minima de cero, solo inicializar la colección
            }
            public void AddModelo(Modelo modelo)
            {
                this.modelos.Add(modelo);
            }
        }

        public class Motor
        {
            private double precio_base;
            private int codigo;

            private ICollection<Modelo> modelos;//Tenemos una cardinalidad máxima de N, necesitamos colección
            private ICollection<Opcion> opciones; //Tenemos una cardinalidad máxima de N, necesitamos colección


            //Cardinalidad mínimma de 1 a 1 con Modelo, hemos relajado en este lado
            public Motor(double precio_base, int codigo, Opcion opcion)
            {
                this.precio_base = precio_base;
                this.codigo = codigo;

                this.opciones = new List<Opcion>();
                this.opciones.Add(opcion); //carddinalidad minima de 1

                this.modelos = new List<Modelo>();
                //this.modelos.Add(modelo); --> tendrá que asegurarse por código. Hemos relajado en este lado
            }
            public void AddModelo(Modelo modelo)
            {
                this.modelos.Add(modelo);
            }
        }
        static void Main(string[] args)
        {
            Concesionario valenciaMotor = new Concesionario("Valencia Motor, S.A", "Avda. Tres Cruces, Valencia");
            Opcion opcionBlue = new Opcion(3500, 1);
            Motor motor93 = new Motor(2000, 1, opcionBlue);
            

            Modelo c3 = new Modelo("Citroën c3",12000,new DateTime(2020,10,30), valenciaMotor, motor93, opcionBlue);
            opcionBlue.AddModelo(c3);//Aseguramos por código la cardinalidad mínima que habíamos relajado
            motor93.AddModelo(c3); //Aseguramos por código la cardinalidad mínima que habíamos relajado
            Cliente cliente = new Cliente("sole", "11111111G", valenciaMotor, c3);
            c3.setCliente(cliente);  //Dejamos consistente el modelo

            valenciaMotor.AddCliente(cliente); //Dejamos consistente el modelo
            valenciaMotor.AddModelo(c3);  ///Aseguramos por código la cardinalidad mínima que habíamos relajado

            Campanya campanya = new Campanya();
            Contiene descuentoCampanya = new Contiene(0.10, c3, campanya);
            campanya.AddContiene(descuentoCampanya); //Aseguramos por código la cardinalidad mínima que habíamos relajado
            c3.AddContiene(descuentoCampanya); //Dejamos consistente el modelo

            Console.WriteLine("Instancia del sistema creada. Pulse enter para cerrar\n");
            Console.ReadLine();









        }
    }
}
