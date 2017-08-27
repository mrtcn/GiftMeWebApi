using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gift.Core.Model;
using Gift.Core.Utilities.Extensions;
using Gift.Data.Entities.SupplementaryModels;
using Gift.Data.Models;
using Gift.Framework.Repository;

namespace Gift.Core.BaseServices
{
    public interface IBaseService<TEntity> : IDependency where TEntity : class, IEntity {
        IQueryable<TEntity> Entities { get; }
        TEntity Get(int id);
        TEntity CreateOrUpdate(IEntityParams entity);
        RemoveResultStatus Remove(int id, bool permanent = false, bool checkRelations = false);
        bool? CheckUserProperty(IEntityParams entity);
    }
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntity {
        private readonly IRepository<TEntity> _repository;

        protected BaseService(IRepository<TEntity> repository) {
            _repository = repository;
        }

        public virtual IQueryable<TEntity> Entities => _repository.Table();    

        public virtual TEntity Get(int id) {
            var entity = _repository.Get(id);
            return entity;
        }

        public virtual TEntity CreateOrUpdate(IEntityParams entityParams) {            
            return entityParams.Id == 0 ? Create(entityParams) : Update(entityParams);
        }

        protected virtual void OnSaveChanging(TEntity entity, IEntityParams entityParams) {
            
        }

        protected virtual void OnSaveChanged(TEntity entity, IEntityParams entityParams) {

        }

        private TEntity Create(IEntityParams entityParams) {

            var entity = Mapper.Map<IEntityParams, TEntity>(entityParams);
            var tracingFieldsModel = entity as TracingDateModel;
            tracingFieldsModel?.FillTracingFields(ActionTypes.Create);            
            OnSaveChanging(entity, entityParams);
            
            _repository.Create(entity);
            _repository.SaveChanges();

            entityParams.Id = entity.Id;

            OnSaveChanged(entity, entityParams);
            return entity;
        }

        private TEntity Update(IEntityParams entityParams) {

            var entity = _repository.Get(entityParams.Id);
            var tracingFieldsModel = entity as TracingDateModel;
            tracingFieldsModel?.FillTracingFields(ActionTypes.Update);

            Mapper.Map(entityParams, entity, entityParams.GetType(), typeof(TEntity));

            OnSaveChanging(entity, entityParams);

            _repository.Update(entity);
            _repository.SaveChanges();

            OnSaveChanged(entity, entityParams);

            return entity;
        }

        public virtual RemoveResultStatus Remove(int id, bool permanent = false, bool checkRelations = false) {

            OnRemoving(id);

            TEntity entity = _repository.Get(id);

            if (checkRelations) {
                foreach (var propertyInfo in entity.GetType().GetProperties()
                    .Where(x => x.PropertyType.IsGenericType
                    && x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))) {

                    var entities = propertyInfo.GetValue(entity, null);

                    if(entities == null)
                        continue; 

                    var hasStatusRecords = entities as IEnumerable<IHasStatus>;

                    if (hasStatusRecords == null) {
                        return RemoveResultStatus.HasRelatedEntities;
                    } else {
                        if (hasStatusRecords.Count(x => x.Status != Status.Deleted) > 0)
                            return RemoveResultStatus.HasRelatedEntities;
                    }
                }
            }

            var statusEntity = entity as IHasStatus;

            if (statusEntity != null && !permanent) {
                var tracingFieldsModel = entity as TracingDateModel;

                tracingFieldsModel.FillTracingFields(ActionTypes.Remove);

                statusEntity.Status = Status.Deleted;
                _repository.Update(entity);

            }else if (permanent) {
                _repository.Delete(id);
            }
            
            _repository.SaveChanges();

            OnRemoved(id);
            return RemoveResultStatus.Success;
        }

        protected virtual void OnRemoving(int id) {
            
        }

        protected virtual void OnRemoved(int id) {

        }

        public bool? CheckUserProperty(IEntityParams entityParams)
        {
            var idEntity = (IEntity)entityParams;
            var currentUserEntity = (IUserId)entityParams;

            var entity = _repository.Get(idEntity?.Id ?? 0);

            var userEntity = (IUserId) entity;

            if (userEntity == null)
                return null;

            if ( (currentUserEntity?.UserId ?? 0) != (userEntity?.UserId ?? 0))
            {
                return false;
            }
            return true;
        }

    }
}