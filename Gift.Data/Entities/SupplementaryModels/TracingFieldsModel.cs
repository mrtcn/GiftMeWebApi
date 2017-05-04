using Gift.Data.Models;

namespace Gift.Data.Entities.SupplementaryModels {    
    public interface ITracingFieldsModel {
        int CreatedBy { get; set; }
        int? ModifiedBy { get; set; }
        UserTypes UserType { get; set; }
    }
}