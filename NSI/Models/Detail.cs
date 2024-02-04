using System;
using System.Collections.Generic;

namespace NSI.Models;

public partial class Detail
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NormativeId { get; set; }

    public virtual Normative? Normative { get; set; }
}
