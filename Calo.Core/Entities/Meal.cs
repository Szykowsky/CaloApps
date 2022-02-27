﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Calo.Core.Entities
{
    public class Meal : BaseEntity
    {
        public int Kcal { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid DietId { get; set; }

        [ForeignKey("DietId")]
        public virtual Diet Diet { get; set; }

    }
}