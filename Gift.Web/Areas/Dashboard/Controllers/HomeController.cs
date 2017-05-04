﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gift.Core.Localization;
using Gift.Core.Services;
using Gift.Framework.Repository;
using Gift.Web.Areas.Dashboard.Controllers.BaseControllers;

namespace Gift.Web.Areas.Dashboard.Controllers {
    [Authorize]
    public class HomeController : BaseController {
        private readonly ICultureService _cultureService;
        private readonly IObjectSetFactory _objectSetFactory;

        public HomeController(
            ICultureService cultureService,
            IObjectSetFactory objectSetFactory) {
            _cultureService = cultureService;
            _objectSetFactory = objectSetFactory;
        }

        public ActionResult ChangeCulture(int cultureId, string actionName, string controllerName) {
            CultureHelper.CurrentCulture = _cultureService.Get(cultureId);

            if (CultureHelper.CurrentCulture == null)
                CultureHelper.CurrentCulture = _cultureService.Entities.FirstOrDefault(x => x.IsDefault);

            var actionNameToRedirect = (actionName.ToLower() == "create" || actionName.ToLower() == "update")
                ? "index"
                : actionName;
            return RedirectToAction(actionNameToRedirect, controllerName);
        }
        // GET: Dashboard/Home
        public ActionResult Index() {
            //using (var dbContext = new DbContext("ApplicationDbContext")) {
            //    var stringList = new List<string>();
            //    var xx = _objectSetFactory.ExecuteStoreQuery<string>("Sp_CreateBranches").ToList();
            //}
            return View();
        }
        public class StoreList {
            public List<string> Branch { get; set; }
        }
    }
}