using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gift.Core.Model;
using Gift.Core.Model.DashboardModule;
using Gift.Core.Services.DashboardServices;
using Gift.Core.Services.IdentityServices;
using Gift.Data.Entities.ModulePermissions;
using Gift.Data.Models;
using Gift.Web.Areas.Dashboard.Models;
using Gift.Web.Areas.Dashboard.Utilities.CustomAttributes;
using Gift.Web.Areas.Dashboard.ViewModel.BaseModels;
using Newtonsoft.Json;

namespace Gift.Web.Areas.Dashboard.Controllers.BaseControllers
{
    [DashboardController(DashboardControllerType.RoleModulePermission, DashboardControllerType.DashboardPermission, 1, "fa fa-link")]
    public class RoleModulePermissionController : BaseController
    {
        private readonly IModulePermissionService _modulePermissionService;
        private readonly IModuleService _moduleService;
        private readonly ApplicationRoleManager _applicationRoleManager;

        public RoleModulePermissionController(IModulePermissionService modulePermissionService
            , IModuleService moduleService, ApplicationRoleManager applicationRoleManager)
        {
            _modulePermissionService = modulePermissionService;
            _moduleService = moduleService;
            _applicationRoleManager = applicationRoleManager;
        }

        // GET: Dashboard/RoleModulePermission
        [DashboardAction("Grup İzni Düzenle", "fa fa-link")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void CreateOrUpdate(string items, int roleId)
        {
            var roleModulePermissionParams = JsonConvert.DeserializeObject<IEnumerable<DashboardPermissionItemModel>>(items);
            foreach (var roleModulePermission in roleModulePermissionParams)
            {
                var rolemodulePermissionModel = new ModulePermissionParams(roleModulePermission, null, roleId);
                _modulePermissionService.CreateOrUpdate(rolemodulePermissionModel);
            }
        }
        [HttpPost]
        public JsonResult GetRoles()
        {
            var roles = _applicationRoleManager.Roles
                .Select(x => new { Text = x.Name, Value = x.Id.ToString() });
            return Json(roles);

        }
        [HttpPost]
        public JsonResult List(int? id)
        {
            if (id == null)
                return null;
            var modules = _moduleService.Entities.ToList();
            var rolePermissions = _modulePermissionService.Entities.Where(x => x.RoleId == id).ToList();
            var roleModulePermissionModel = GetRoleModulePermission(rolePermissions, modules);
            return Json(roleModulePermissionModel, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<DashboardPermissionItemModel> GetRoleModulePermission(List<ModulePermission> rolePermissions, List<Module> modules)
        {

            var rolePermissionModule = modules.GroupJoin(rolePermissions
                , module => module.Id
                , permission => permission.ModuleId
                , (module, rolePermission) => new { module, rolePermission })
                .SelectMany(x => x.rolePermission.DefaultIfEmpty()
                , (module, rolePermission) => new { module, rolePermission });


            var roleModulePermissionModel = rolePermissionModule.Select(x => new DashboardPermissionItemModel
            {
                Id = x.rolePermission?.Id ?? 0,
                ModuleId = x.module.module.Id,
                ModuleName = x.module.module.ModuleName,
                Create = x.rolePermission != null && (x.rolePermission.Permission & Permissions.Create) == Permissions.Create,
                Delete = x.rolePermission != null && (x.rolePermission.Permission & Permissions.Delete) == Permissions.Delete,
                Edit = x.rolePermission != null && (x.rolePermission.Permission & Permissions.Edit) == Permissions.Edit,
                Export = x.rolePermission != null && (x.rolePermission.Permission & Permissions.Export) == Permissions.Export,
                View = x.rolePermission != null && (x.rolePermission.Permission & Permissions.View) == Permissions.View
            });
            return roleModulePermissionModel;
        }

        public RemoveResultStatus Remove(int id)
        {
            var result = _modulePermissionService.Remove(id);
            return result;
        }
    }
}