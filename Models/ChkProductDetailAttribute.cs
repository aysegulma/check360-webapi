using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkProductDetailAttribute
{
    public int ProductDetailAttributeId { get; set; }

    public int? ProductDetailId { get; set; }

    public string? AttributeName { get; set; }

    public string? AttributeValue { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
