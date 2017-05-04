
using System.Collections.Generic;

namespace Gift.Data.Models
{
    public interface ICulturedCollection<TCulturedEntity> {
        ICollection<TCulturedEntity> CulturedEntities { get; set; }
    }
}