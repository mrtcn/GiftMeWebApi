using Gift.Data.Models;

namespace Gift.Core.Model
{
    public interface ICulturedEntityParams : IEntityParams {
        int BaseEntityId { get; set; }
        Status Status { get; set; }
    }
}
