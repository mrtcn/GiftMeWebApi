using System.Collections.Generic;
using System.Web.Mvc;
using Gift.Core.EntityParams;

namespace Gift.Web.Areas.Dashboard.ViewModel {
    public class DistrictViewModel : DistrictParams {
        public IEnumerable<SelectListItem> CitySelectListItems { get; set; }
    }
}