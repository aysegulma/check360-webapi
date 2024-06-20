using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkSeller
{
    public int SellerId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? WebUrl { get; set; }

    public string? Logo { get; set; }
}
