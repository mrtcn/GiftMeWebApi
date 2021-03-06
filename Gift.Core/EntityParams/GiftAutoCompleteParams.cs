﻿using Gift.Core.Model;
using Gift.Data.Entities;
using Gift.Data.Models;

namespace Gift.Core.EntityParams {
    public class GiftAutoCompleteParams : IGiftAutoComplete, IEntityParams {
        public int Id { get; set; }
        public string GiftAutoCompleteName { get; set; }

        public Status Status { get; set; }

        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public UserTypes UserType { get; set; }
    }
}