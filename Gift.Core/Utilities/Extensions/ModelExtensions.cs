using System;
using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;

namespace Gift.Core.Utilities.Extensions
{
    public static class ModelExtensions {
        public static T FillTracingFields<T>(this T model, ActionTypes actionType) where T : TracingDateModel {
            switch (actionType) {
                case ActionTypes.Create:
                    model.CreationDate = DateTime.Now;
                    break;
                case ActionTypes.Update:
                case ActionTypes.Remove:
                    model.ModificationDate = DateTime.Now;
                    break;
            }
            return model;
        }
    }
}
