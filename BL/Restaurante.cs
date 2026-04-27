using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Restaurante
    {

        private readonly DL.RestauranteContext _context;

        public Restaurante(DL.RestauranteContext context)
        {
            _context = context;
        }



    }
}
