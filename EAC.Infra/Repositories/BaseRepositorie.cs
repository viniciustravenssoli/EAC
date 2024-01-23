using EAC.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Infra.Repositories
{
    public class BaseRepositorie
    {
        protected readonly AppDbContext _dbContext;

        public BaseRepositorie(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
