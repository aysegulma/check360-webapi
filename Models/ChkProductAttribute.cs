using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkProductAttribute
{
    public int ProductAttributeId { get; set; }

    public int? ProductId { get; set; }

    public string? AttributeName { get; set; }

    public string? AttributeNameText { get; set; }

    public bool? ShowInProductName { get; set; }

    public bool? ShowInProductDetail { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
