using Abyat.Bl.Contracts.Base;
using Abyat.Bl.Contracts.Events;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Events.Args;
using Abyat.Domains.Contracts;
using AutoMapper;
using System.Linq.Expressions;
using static Abyat.Bl.Enums.ServicesEnums;
using static Abyat.Core.Enums.Status.Status;

namespace Abyat.Bl.Services.Base;

public abstract class BaseService<Tb, Dto> : IBaseService<Tb, Dto> where Tb : BaseTable
{
    readonly ITableQryRepo<Tb> _repoQuery;
    readonly ITableCmdRepo<Tb> _repoCommand;
    readonly IMapper _mapper;
    readonly IUserQry _userServiceQuery;
    readonly IUnitOfWork _UnitOfWork;
    private readonly ICrudPublisher _publisher;

    public BaseService(
        ITableQryRepo<Tb> repoQry,
        ITableCmdRepo<Tb> repoCmd,
        IMapper mapper,
        IUserQry userServiceQry,
        ICrudPublisher publisher)
    {
        _repoQuery = repoQry;
        _repoCommand = repoCmd;
        _mapper = mapper;
        _userServiceQuery = userServiceQry;
        _publisher = publisher;
    }

    public BaseService(
        IUnitOfWork unitofwork,
        ITableQryRepo<Tb> repoQry,
        IMapper mapper,
        IUserQry userServiceQry,
        ICrudPublisher publisher)
    {
        _UnitOfWork = unitofwork;
        _repoQuery = repoQry;
        _repoCommand = unitofwork.Repository<Tb>();
        _mapper = mapper;
        _userServiceQuery = userServiceQry;
        _publisher = publisher;
    }

    public async virtual Task<List<Dto>> GetAllAsync() => _mapper.Map<List<Tb>, List<Dto>>(await _repoQuery.GetAllAsync());

    public async virtual Task<Dto> GetByIdAsync(Guid id) => _mapper.Map<Tb, Dto>(await _repoQuery.FindAsync(id));

    public async virtual Task<(bool success, Guid id)> AddAsync(Dto entity, bool fireEvent = true)
    {
        var dbObject = _mapper.Map<Dto, Tb>(entity);
        var UserId = _userServiceQuery.GetLoggedInUserId();
        dbObject.CreatedBy = UserId;
        dbObject.CreatedAt = DateTime.Now;
        dbObject.CurrentState = enCurrentState.Active;
        var result = await _repoCommand.AddAsync(dbObject);

        if (result.success && fireEvent)
        {
            _publisher.Publish(new EntityCreatedEventArgs(dbObject, UserId));
        }

        return (result.success, result.newId);
    }

    public async virtual Task<bool> UpdateAsync(Dto entity, bool fireEvent = true)
    {
        var dbObject = _mapper.Map<Dto, Tb>(entity);
        var oldObj = dbObject;
        var userId = _userServiceQuery.GetLoggedInUserId();
        dbObject.UpdatedBy = userId;
        dbObject.UpdatedAt = DateTime.Now;

        var result = await _repoCommand.UpdateAsync(dbObject);

        if (result && fireEvent)
        {
            _publisher.Publish(new EntityUpdatedEventArgs(dbObject, userId, oldObj));
        }

        return result;
    }

    private async Task<bool> ChangeStatusAsync(Guid id, enCurrentState status = enCurrentState.Active, bool fireEvent = true)
    {
        Tb? entity = await _repoQuery.FindAsync(id);

        if (entity == null) return false;

        enCurrentState previousState = entity.CurrentState;

        entity.CurrentState = status;
        var userId = _userServiceQuery.GetLoggedInUserId();
        entity.UpdatedBy = userId;

        bool success = await _repoCommand.UpdateAsync(entity);

        if (success && fireEvent)
        {
            _publisher.Publish(new EntityStatusChangedEventArgs(entity, userId, previousState, status));
        }
        return success;
    }

    public async virtual Task<bool> ActivateAsync(Guid id, bool fireEvent = true) => await ChangeStatusAsync(id, enCurrentState.Active, fireEvent);

    public async virtual Task<bool> DeactivateAsync(Guid id, bool fireEvent = true) => await ChangeStatusAsync(id, enCurrentState.InActive, fireEvent);

    public async virtual Task<bool> DeleteAsync(Guid id, enDeleteType deleteType = enDeleteType.HardDelete, bool fireEvent = true)
    {
        if (deleteType == enDeleteType.SoftDelete)
        {
            return await ChangeStatusAsync(id, enCurrentState.Deleted, fireEvent);
        }
        else
        {
            return await _repoCommand.HardDeleteAsync(id);
        }
    }

    public async virtual Task<int> CountAsync(CancellationToken cancellationToken) => await _repoQuery.CountAsync(e => e.CurrentState > 0, cancellationToken);

    public async virtual Task<bool> IsExistsAsync(Expression<Func<Tb, bool>> filter, CancellationToken cancellationToken) => await _repoQuery.IsExistsAsync(filter, cancellationToken);

    public async virtual Task<bool> IsExistsAsync(Guid Id, CancellationToken cancellationToken) => await _repoQuery.IsExistsAsync(Id, cancellationToken);

}