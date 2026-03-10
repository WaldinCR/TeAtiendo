using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeAtiendo.Application.Base
{
    public interface IBaseService<TDto, TSaveDto, TUpdateDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto?> GetByIdAsync(Guid id);

        Task<TDto> AddAsync(TSaveDto dto);

        Task UpdateAsync(Guid id, TUpdateDto dto);

        Task DeleteAsync(Guid id);
    }
}