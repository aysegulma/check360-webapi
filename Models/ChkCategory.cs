using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkCategory
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public int? ParentCategoryId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
