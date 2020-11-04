using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreAssignment.Models
{
    class StudentSubjectModel
    {
        public long Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string SubjectName { get; set; }
    }
}
