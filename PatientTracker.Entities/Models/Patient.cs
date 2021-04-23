using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PatientTracker.Entities.Models
{
    public class Patient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string OwnerInfo { get; set; }

        public string Genus { get; set; }

        public string Diagnosis { get; set; }



    }
}
