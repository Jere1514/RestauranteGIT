using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    partial class RestauranteContext
    {

        public virtual DbSet<DTOs.DtoSPGetAll> SPGetAll { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DTOs.DtoSPGetAll>().HasNoKey();
        }


    }
}
