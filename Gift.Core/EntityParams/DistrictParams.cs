using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class DistrictParams : IDistrict, IEntityParams {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOverpopulated { get; set; }
        public int CityId { get; set; }
        public Status Status { get; set; }
    }
}