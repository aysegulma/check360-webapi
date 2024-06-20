using System;
using System.Collections.Generic;

namespace CheckWebApi.Models;

public partial class ChkProductDetail
{
    public int ProductDetailId { get; set; }

    public int? ProductId { get; set; }

    public int? SellerId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public decimal? ShippingPrice { get; set; }

    public bool? IsReturnChargeable { get; set; }
}
