using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeAtiendo.Domain.Entities.Auditory;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IAuditoriaRepository
    {
        Task<IEnumerable<Auditoria>> GetAllAsync();
        Task AddAsync(Auditoria auditoria);
    }
}