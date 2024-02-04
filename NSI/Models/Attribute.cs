using System;
using System.Collections.Generic;

namespace NSI.Models;

public partial class Attribute
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Normative> Normatives { get; set; } = new List<Normative>();
}
