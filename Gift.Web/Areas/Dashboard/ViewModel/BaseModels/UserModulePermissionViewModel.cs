using System.Collections.Generic;
using System.Web.Mvc;

namespace Gift.Web.Areas.Dashboard.ViewModel.BaseModels {
    public class UserModulePermissionViewModel {
        public int UserId { get; set; }
        public IEnumerable<SelectListItem> UserSelectListItems { get; set; }
    }
}