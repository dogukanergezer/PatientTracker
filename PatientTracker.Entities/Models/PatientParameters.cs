using System;
using System.Collections.Generic;
using System.Text;

namespace PatientTracker.Entities.Models
{
    public class PatientParameters:QueryStringParameters
    {
		public PatientParameters()
		{
			OrderBy = "name";
		}

		public string Genus { get; set; }
		public string OwnerInfo { get; set; }
		public string Diagnosis { get; set; }

		public string Name { get; set; }
	}
}
