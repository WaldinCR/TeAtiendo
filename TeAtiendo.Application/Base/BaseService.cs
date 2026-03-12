using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Base
{
    public abstract class BaseService<TEntity, TDto> : IBaseService<TDto>
        where TEntity : BaseEntity, new()
    {
        protected readonly IRepository<TEntity> Repo;

        // el IUnitOfWork 
        protected readonly TeAtiendo.Persistence.Interface.IUnitOfWork Uow;

        protected BaseService(
            IRepository<TEntity> repo,
            TeAtiendo.Persistence.Interface.IUnitOfWork uow)
        {
            Repo = repo;
            Uow = uow;
        }

        protected abstract TDto ToDto(TEntity entity);
        protected abstract void ApplyDto(TDto dto, TEntity entity);

        public async Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken ct = default)
            => (await Repo.GetAllAsync(ct)).Select(ToDto).ToList();

        public async Task<TDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await Repo.GetByIdAsync(id, ct);
            return entity is null ? default : ToDto(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto, CancellationToken ct = default)
        {
            var entity = new TEntity { Id = Guid.NewGuid() };
            ApplyDto(dto, entity);

            await Repo.AddAsync(entity, ct);
            await Uow.SaveAsync(ct);

            return ToDto(entity);
        }

        public async Task<TDto?> UpdateAsync(Guid id, TDto dto, CancellationToken ct = default)
        {
            var entity = await Repo.GetByIdAsync(id, ct);
            if (entity is null) return default;

            ApplyDto(dto, entity);

            await Repo.UpdateAsync(entity, ct);
            await Uow.SaveAsync(ct);

            return ToDto(entity);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken ct = default)
        {
            await Repo.SoftDeleteAsync(id, userId, ct);
            await Uow.SaveAsync(ct);
            return true;
        }
    }
}