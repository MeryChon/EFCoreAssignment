
using System;

namespace StudentsEFAssignment.Entities
{
    public class StudentSubject
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        protected virtual Student Student { get; set; }
        public long SubjectId { get; set; }
        protected virtual Subject Subject { get; set; }

        public  Nullable<double> Score { get; set; }
    }
}
