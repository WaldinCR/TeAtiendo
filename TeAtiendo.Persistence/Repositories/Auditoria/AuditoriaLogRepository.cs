using System;
using TeAtiendo.Domain.Entities.Auditoria;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Auditoria
{
    public class AuditoriaLogRepository : BaseRepository<AuditoriaLog>, IAuditoriaLogRepository
    {
        public AuditoriaLogRepository(TeAtiendoContext context) : base(context) { }
    }
}