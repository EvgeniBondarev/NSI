using System;
using System.Collections.Generic;

namespace NSI.Models;

public partial class Normative
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Designation { get; set; }

    public int? Quantity { get; set; }

    public string? UnitOfMeasure { get; set; }

    public int? DetailType { get; set; }

    public int? Attribute { get; set; }

    public virtual Attribute? AttributeNavigation { get; set; }

    public virtual DetailType? DetailTypeNavigation { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();
}
