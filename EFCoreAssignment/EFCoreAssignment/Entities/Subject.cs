using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace StudentsEFAssignment.Entities
{
    class Subject
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public List<StudentSubject> students { get; set; }
    }
}
