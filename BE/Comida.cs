using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BE
{
    /**
        Listar todas las columnas
        var listadoCompleto = db.Tabla.ToList();

        Seleccionar una columna --> Listado de string, porque es el tipo de dato de la propiedad
        var listaNombre = db.Personas.Select( x => x.Nombre).ToList();

        Seleccionar varias columnas y utilizando un tipo 'anonimo'
        var listaAnonima = db.Persona.Select( x => new { Nombre = x.Nombre, Edad = x.Edad }).ToList();

        Seleccionar varias columnas  y proyectarlas hacia 'Persona'
        var listaAnonima = db.Persona.Select( x => new { Nombre = x.Nombre, Edad = x.Edad }).ToList()
                                            .Select( x => new Persona(){ Nombre = x.Nombre, Edad= x.Edad}).ToList();


        Damos a entender a EF que x, ya existe en la base de datos y que le tiene que prestar atencion
        db.Persona.Attach(x)

        Si la una propiedad tiene 'virtual', se activa la funcionalidad de Lazy Loading, en cual permite, traer los elementos
        solo si son llamados, ejemplo, traer nuna lista dentro un objeto, esta lista permanecera nula si nunca es llamada.
        Esta manera de utilizar puede realizar dos Querys.

        'Include' -->  de esta manera realizamos una llamada a la base de datos y tambien trae con ella 'Direcciones', el cual
                       es una lista. 
        var persona = db.Persona.Include("Direcciones").FirstOrDefault(x => x.Id == 2);
    */
    [Table("Comida")]
    public class Comida
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Nombre { get; set; }
        public ICollection<Equipo> Equipos { get; set; }
        public Comida()
        {
            this.Equipos = new HashSet<Equipo>();
        }
    }
}
