using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class CultureParams : ICulture, IEntityParams {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CultureCode { get; set; }
        public string ShortName { get; set; }
        public string FlagImagePath { get; set; }
        public string Url { get; set; }
        public bool IsDefault { get; set; }
        public Status Status { get; set; }
    }
}