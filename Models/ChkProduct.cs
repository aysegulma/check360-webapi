using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkProduct
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public int? ProducerId { get; set; }

    public string? Image { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
