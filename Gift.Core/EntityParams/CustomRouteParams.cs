﻿using System.Net;
using Gift.Core.Model;
using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class CustomRouteParams : ICustomRoute, IEntityParams
    {
        public int Id { get; set; }

        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public int ContentId { get; set; }
        public bool IsAutoGenerated { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public PredefinedPage PredefinedPage { get; set; }
        public string[] UrlParts { get; set; }

        public Status Status { get; set; }
    }
}