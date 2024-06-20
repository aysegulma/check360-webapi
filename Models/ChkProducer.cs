using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkProducer
{
    public int ProducerId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
