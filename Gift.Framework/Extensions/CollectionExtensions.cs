using System.Collections.Generic;
using System.Web.Mvc;

namespace Gift.Framework.Extensions {
    public static class CollectionExtensions {
        public static MultiSelectList ToMultiSelectList(this IEnumerable<object> collection, string dataValueField, string dataTextField, IEnumerable<string> selectedValues) {
            return new MultiSelectList(collection, dataValueField, dataTextField, selectedValues);
        }
    }
}