﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApHora
{
    public class ApHora
    {
        private ICollection<Profesor> profesores;
        private ICollection<Asignatura> asignaturas;

        //Tenemos una cardinalidad mínima 1 a 1 con Asignatura, relajamos en este lado 
        // y eliminamos asginatura del constructor
        //Tenemos una cardinalidad mínima 1 a 1 con Profesor, relajamos en este lado 
        // y eliminamos profesor del constructor
        public ApHora()
        {
            this.profesores = new List<Profesor>();
            //this.profesores.Add(profesor);--> tendrá que asegurarse por código
            this.asignaturas = new List<Asignatura>();
            //this.asignaturas.Add(asignatura);--> tendrá que asegurarse por código

        }
        public void AddProfesores(Profesor profesor) {
            this.profesores.Add(profesor);
                
        }
        public void AddAsignaturas(Asignatura asignatura) {
            this.asignaturas.Add(asignatura);
        }

    }

    public class Asignatura
    {
        private string nombre;
        private int codigo;
        private string curso;
        private ApHora enApHora;

        private ICollection<Profesor> profesores;
        private ICollection<Teoria> gruposTeoria;
        private ICollection<Practica> gruposPracticas;


        //Tenemos una cardinalidad mínima 1 a 1 con profesor, relajamos en este lado 
        // y eliminamos profesor del constructor
        //Tenemos una cardinalidad mínima 1 a 1 con ApHora, relajaremos en ApHora
        //Tenemos una cardinalidad mínima 1 a 1 con Teoria, relajamos en este lado 
        // y eliminamos teoria del constructor	
        public Asignatura(String nombre, int codigo, String curso, ApHora apHora)
        {
            this.nombre = nombre;
            this.codigo = codigo;
            this.curso = curso;

            this.enApHora = apHora;

            this.profesores = new List<Profesor>();
            //this.profesores.Add(profesor); --> tendrá que asegurarse por código

            this.gruposTeoria = new List<Teoria>();
            //this.gruposTeoria.Add(teoria); --> tendrá que asegurarse por codigo

            this.gruposPracticas = new List<Practica>();

        }

        public void AddProfesores(Profesor profesor)
        {
            this.profesores.Add(profesor);

        }

        public void AddTeoria(Teoria teoria) {
            this.gruposTeoria.Add(teoria);
        }
        public void AddPractica(Practica practica)
        {
            this.gruposPracticas.Add(practica);
        }
            

    }

    public class Profesor
    {
        private int codigo;
        private string nombre;
        private ApHora enApHora;
        private ICollection<Asignatura> asignaturas;
        private ICollection<Grupo> grupos;

        //Tenemos una cardinalidad mínima 1 a 1 con Grupo, relajamos de este lado y eliminamos grupo del constructor
        //Tenemos una cardinalidad mínima 1 a 1 con ApHora, relajaremos en ApHora
        //Tenemos una cardinalidad mínima 1 a 1 con Asignatura, relajaremos en Asignatura
        public Profesor(int codigo, string nombre, Asignatura asignatura, ApHora apHora)
        {
            this.codigo = codigo;
            this.nombre = nombre;

            this.enApHora = apHora;
            
            this.asignaturas = new List<Asignatura>();
            this.asignaturas.Add(asignatura);
            
            this.grupos = new List<Grupo>();
            //this.grupos.Add(grupo);  --> tendrá que asegurarse por codigo

        }

        public void AddGrupos(Grupo grupo) {
            this.grupos.Add(grupo);
        }


    }

    public class Grupo
    {
        private int cod_g;
        private int tamanyo;
        private DateTime hora_desde;
        private DateTime hora_hasta;
        private Aula aula;
        private Profesor profesor;

        public Grupo(int cod_g, int tamanyo, DateTime hora_desde, DateTime hora_hasta, Aula aula, Profesor profesor)
        {
            this.cod_g = cod_g;
            this.tamanyo = tamanyo;
            this.hora_desde = hora_desde;
            this.hora_hasta = hora_hasta;
            this.aula = aula;
            this.profesor = profesor;
        }


    }

    public class Aula
    {
        private int cod_a;
        private int capacidad;
        private ICollection<Grupo> grupos;


        public Aula(int cod_a, int capacidad)
        {
            this.cod_a = cod_a;
            this.capacidad = capacidad;
            grupos = new List<Grupo>();

        }
        public void AddGrupo(Grupo grupo)
        {
            this.grupos.Add(grupo);

        }


    }
    public class Teoria : Grupo
    {
        private Asignatura asignaturaTeoria;

        public Teoria(int cod_g, int tamanyo, DateTime hora_desde, DateTime hora_hasta, Aula aula,
          Profesor profesor, Asignatura asignatura) : base(cod_g, tamanyo, hora_desde, hora_hasta, aula, profesor)
        {

            asignaturaTeoria = asignatura;

        }


    }
    public class Practica : Grupo
    {
        private Asignatura asignaturaPracticas;

        public Practica(int cod_g, int tamanyo, DateTime hora_desde, DateTime hora_hasta, Aula aula,
          Profesor profesor, Asignatura asignatura) : base(cod_g, tamanyo, hora_desde, hora_hasta, aula, profesor)
        {

            asignaturaPracticas = asignatura;

        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            ApHora miApHora = new ApHora();

            Aula aula104 = new Aula(104,70);

            Asignatura isw = new Asignatura("ISW", 1, "tercero", miApHora);            
            Profesor soledad = new Profesor(1, "soledad", isw, miApHora);
            isw.AddProfesores(soledad); //aseguramos por código la relajación en Asignatura con Profesor
            Teoria teo_isw = new Teoria(11, 50, new DateTime(2020, 9, 14, 15, 0, 0), new DateTime(2020, 9, 14, 16, 30, 0), aula104,soledad, isw);
            isw.AddTeoria(teo_isw); //aseguramos por código la relajación en Asignatura con teoria

            
            soledad.AddGrupos(teo_isw); //aseguramos por código la relajación en Profesor con Grupo

            miApHora.AddAsignaturas(isw); //aseguramos por código la relajación de ApHora con Asignarua
            miApHora.AddProfesores(soledad); //aseguramos por código la relajación de ApHora con Profesor

            aula104.AddGrupo(teo_isw); //dejamos consistente el modelo, la navegación de la asociación es doble

            Practica prac_isw = new Practica(12, 20, new DateTime(2020, 9, 16, 18, 30, 0), new DateTime(2020, 9, 16, 20, 00, 0), aula104, soledad, isw);
            aula104.AddGrupo(prac_isw); //dejamos consistente el modelo, la navegación de la asociación es doble
            soledad.AddGrupos(prac_isw);//dejamos consistente el modelo, la navegación de la asociación es doble
            isw.AddPractica(prac_isw);//dejamos consistente el modelo, la navegación de la asociación es doble




        }
    }
}
