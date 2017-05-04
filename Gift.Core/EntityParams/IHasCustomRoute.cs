﻿using System.Net;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public interface IHasCustomRoute {
        int Id { get; set; }
        string Url { get; set; }
        string SeoTitle { get; set; }
        string MetaKeyword { get; set; }
        string MetaDescription { get; set; }
        int ContentId { get; set; }
        bool IsAutoGenerated { get; set; }
        HttpStatusCode HttpStatusCode { get; set; }
        PredefinedPage PredefinedPage { get; set; }

        string[] UrlParts { get; set; }
    }
}