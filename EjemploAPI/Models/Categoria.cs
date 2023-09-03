using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EjemploAPI.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]  //ignora este objeto al mostrarlo en los response
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
