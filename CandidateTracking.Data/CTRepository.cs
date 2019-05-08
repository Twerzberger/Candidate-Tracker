using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CandidateTracking.Data
{
    public class CTReposetory
    {
        private string _connectionString;

        public CTReposetory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCandidate(Candidate candidate)
        {
            using (var ctx = new CTContext(_connectionString))
            {
                ctx.Add(candidate);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<Candidate> GetPending()
        {
            using (var ctx = new CTContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Pending == true).ToList();
            }
        }

        public IEnumerable<Candidate> GetConfirmed()
        {
            using (var ctx = new CTContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Confirmed == true).ToList();
            }
        }

        public IEnumerable<Candidate> GetDeclined()
        {
            using (var ctx = new CTContext(_connectionString))
            {
                return ctx.Candidates.Where(c => c.Declined == true).ToList();
            }
        }

        public Candidate GetCandidate(int id)
        {
            using (var ctx = new CTContext(_connectionString))
            {
                return ctx.Candidates.FirstOrDefault(c => c.Id == id);
            }
        }

        public int GetPendingCount()
        {
            
            using (var ctx = new CTContext(_connectionString))
            {
                 return ctx.Candidates.Count(c => c.Pending == true);
            }            
        }

        public int GetConfirmedCount()
        {

            using (var ctx = new CTContext(_connectionString))
            {
                return ctx.Candidates.Count(c => c.Confirmed == true);
            }
        }

        public int GetDeclinedCount()
        {

            using (var ctx = new CTContext(_connectionString))
            {
                return ctx.Candidates.Count(c => c.Declined
 == true);
            }
        }

        public void UpdateToConfirmed(int id)
        {
            using (var ctx = new CTContext(_connectionString))
            {
                ctx.Database.ExecuteSqlCommand(
                        "UPDATE Candidates SET Confirmed = 'True', Pending = 'False' WHERE Id = @id",
                    new SqlParameter("@id", id));
            }
        }

        public void UpdateToDeclined(int id)
        {
            using (var ctx = new CTContext(_connectionString))
            {
                ctx.Database.ExecuteSqlCommand(
                        "UPDATE Candidates SET Declined = 'True', Pending = 'False' WHERE Id = @id",
                    new SqlParameter("@id", id));
            }
        }
    }
}
