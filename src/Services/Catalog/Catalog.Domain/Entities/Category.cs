using System;
using System.Collections.Generic;

namespace Catalog.Domain.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
