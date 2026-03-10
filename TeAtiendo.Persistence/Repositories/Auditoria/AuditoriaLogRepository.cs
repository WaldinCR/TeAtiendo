using System;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;
using TeAtiendo.Domain.Entities.Auditory;

namespace TeAtiendo.Persistence.Repositories.Auditory
{
    public class AuditoriaRepository : BaseRepository<Auditoria>, IAuditoriaRepository
    {
        public AuditoriaRepository(TeAtiendoContext context) : base(context)
        {
        }
    }
}