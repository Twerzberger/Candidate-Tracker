using System;

namespace CandidateTracking.Data
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool Pending { get; set; }
        public bool Confirmed { get; set; }
        public bool Declined { get; set; }
    }
}
