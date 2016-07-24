using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace SampleFive.DomainLayer.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDatetime { get; set; }

        public int EditedByUserId { get; set; }
        public DateTime EditeDateTime { get; set; }

        public string Language { get; set; }
    }
}