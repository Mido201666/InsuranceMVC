using System.Collections.Generic;
using System.Data.Entity;

namespace InsuranceMVC.Models
{
    public class InsuranceMVCEntities : DbContext
    {
        public InsuranceMVCEntities() : base("name=InsuranceMVCEntities")
        {
        }

        public DbSet<Insuree> Insurees { get; set; }
    }
}