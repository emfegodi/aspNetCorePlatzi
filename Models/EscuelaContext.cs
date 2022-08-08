using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace platzi_asp_net_core.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }

        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Evaluacion>().HasOne(e => e.Alumno).WithMany(a => a.Evaluaciones);

            var escuela = new Escuela();
            escuela.AñoDeCreación = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Platzi School";
            escuela.Ciudad = "Bogota";
            escuela.Pais = "Colombia";
            escuela.Dirección = "Avd Siempre viva";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            //modelBuilder.Entity<Escuela>().HasData(escuela);

            //Cargar Cursos de la escuela
            var cursos = CargarCursos(escuela);

            //x cada curso cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);

            //x cada curso cargar alumnos
            var alumnos = CargarAlumnos(cursos);

            //x cada alumno cargar evaluaciones
            var evaluaciones = CargarEvaluaciones(alumnos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
            modelBuilder.Entity<Evaluacion>().HasData(evaluaciones.ToArray());
            //{ 
            //    modelBuilder.Entity<Asignatura>().HasData(
            //                 new Asignatura { Nombre = "Matemáticas", Id = Guid.NewGuid().ToString() },
            //                 new Asignatura { Nombre = "Educación Física", Id = Guid.NewGuid().ToString() },
            //                 new Asignatura { Nombre = "Castellano", Id = Guid.NewGuid().ToString() },
            //                 new Asignatura { Nombre = "Ciencias Naturales", Id = Guid.NewGuid().ToString() },
            //                 new Asignatura { Nombre = "Programación", Id = Guid.NewGuid().ToString() }
            //                );

            // modelBuilder.Entity<Alumno>()
            //             .HasData(GenerarAlumnosAlAzar().ToArray());
            //}
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();
            Random rnd = new Random();

            foreach(var curso in cursos)
            {
                int cantRandom = rnd.Next(5,20);
                var tmplist = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmplist);
            }
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (Curso curso in cursos)
            {
                var tmpList = new List<Asignatura>{new Asignatura{Nombre = "Matemáticas",CursoId = curso.Id,Id = Guid.NewGuid().ToString()},
                            new Asignatura{Nombre = "Educación Física",CursoId = curso.Id,Id = Guid.NewGuid().ToString()},
                            new Asignatura{Nombre = "Castellano",CursoId = curso.Id,Id = Guid.NewGuid().ToString()},
                            new Asignatura{Nombre = "Ciencias Naturales",CursoId = curso.Id,Id = Guid.NewGuid().ToString()},
                            new Asignatura{Nombre = "Programación",CursoId = curso.Id,Id = Guid.NewGuid().ToString()}};
                listaCompleta.AddRange(tmpList);
                //curso.Asignaturas = tmpList;
            }
            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso(){Id = Guid.NewGuid().ToString(),Nombre = "101",EscuelaId = escuela.Id,Jornada = TiposJornada.Mañana},
                new Curso(){Id = Guid.NewGuid().ToString(),Nombre = "201",EscuelaId = escuela.Id,Jornada = TiposJornada.Mañana},
                new Curso(){Id = Guid.NewGuid().ToString(),Nombre = "301",EscuelaId = escuela.Id,Jornada = TiposJornada.Mañana},
                new Curso(){Id = Guid.NewGuid().ToString(),Nombre = "401",EscuelaId = escuela.Id,Jornada = TiposJornada.Mañana},
                new Curso(){Id = Guid.NewGuid().ToString(),Nombre = "501",EscuelaId = escuela.Id,Jornada = TiposJornada.Mañana},
            };
        }

        private List<Evaluacion> CargarEvaluaciones(List<Alumno> alumnos)
        {
            var listaEvaluaciones = new List<Evaluacion>();
            Random rnd = new Random();

            foreach(var alumno in alumnos)
            {
                int cantidad = rnd.Next(3,5);
                var evaluaciones = GenerarEvaluaciones(alumno, cantidad);
                //alumno.Evaluaciones = evaluaciones;
                listaEvaluaciones.AddRange(evaluaciones);
            }
            return listaEvaluaciones;
        }
        
       private List<Evaluacion> GenerarEvaluaciones(Alumno alum, int cantidad)
       {
        string[] nombre = {"Eva-Matemáticas", "Eva-Castellano", "Eva-Edu.Física", "Eva-Ciencias", "Eva-Programación"};
        float[] nota ={1, 2, 3, 4, 5};

        var evaluaciones = from n1 in nombre
                            from n2 in nota
                            select new Evaluacion{Id=Guid.NewGuid().ToString(), Nombre = $"{n1}",Nota= n2, AlumnoId=alum.Id};
        
        return evaluaciones.OrderBy((ev)=>ev.Id).Take(cantidad).ToList();
       }
       private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }
   
    }
}