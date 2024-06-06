using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crowbond.Modules.Products.Application.Products.UpdateProduct.Dtos;
public sealed record ProductDto(
    Guid? Id,
    string Sku,
    string Name,
    Guid? Parent,
    string FilterType,
    string UnitOfMeasure,
    Guid Category,
    string InventoryType,
    int? Barcode,
    decimal? PackSize,
    string HandlingNotes,
    bool QiCheck,
    string Notes,
    decimal? ReorderLevel,
    decimal? Height,
    decimal? Width,
    decimal? Length,
    bool WeightInput,
    bool Active);
