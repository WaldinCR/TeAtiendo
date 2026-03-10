using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeAtiendo.Application.Base
{
    public abstract class BaseService<TDto, TSaveDto, TUpdateDto>
        : IBaseService<TDto, TSaveDto, TUpdateDto>
    {
        public abstract Task<IEnumerable<TDto>> GetAllAsync();

        public abstract Task<TDto?> GetByIdAsync(Guid id);

        public abstract Task<TDto> AddAsync(TSaveDto dto);
        
        public abstract Task UpdateAsync(Guid id, TUpdateDto dto);

        public abstract Task DeleteAsync(Guid id);
    }
}