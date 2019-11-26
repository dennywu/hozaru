using Hozaru.Core;
using Hozaru.Core.Dependency;
using Hozaru.Core.Domain.Entities;
using Hozaru.Core.Domain.Entities.Auditing;
using Hozaru.Core.Runtime.Session;
using Hozaru.Core.Timing;
using NHibernate;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Interceptors
{
    internal class HozaruNHibernateInterceptor : EmptyInterceptor
    {
        //public IEntityChangedEventHelper EntityChangedEventHelper { get; set; }

        private readonly IIocManager _iocManager;
        private readonly Lazy<IHozaruSession> _hozaruSession;
        private readonly Lazy<IGuidGenerator> _guidGenerator;

        public HozaruNHibernateInterceptor(IIocManager iocManager)
        {
            _iocManager = iocManager;
            _hozaruSession =
                new Lazy<IHozaruSession>(
                    () => _iocManager.IsRegistered(typeof(IHozaruSession))
                        ? _iocManager.Resolve<IHozaruSession>()
                        : NullHozaruSession.Instance
                    );
            _guidGenerator =
                new Lazy<IGuidGenerator>(
                    () => _iocManager.IsRegistered(typeof(IGuidGenerator))
                        ? _iocManager.Resolve<IGuidGenerator>()
                        : SequentialGuidGenerator.Instance
                    );
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //Set Id for Guids
            if (entity is IEntity<Guid>)
            {
                var guidEntity = entity as IEntity<Guid>;
                if (guidEntity.IsTransient())
                {
                    guidEntity.Id = _guidGenerator.Value.Create();
                }
            }

            //Set CreationTime for new entity
            if (entity is IHasCreationTime)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreationTime")
                    {
                        state[i] = (entity as IHasCreationTime).CreationTime = Clock.Now;
                    }
                }
            }

            //Set CreatorUserId for new entity
            if (entity is ICreationAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreatorUserId")
                    {
                        state[i] = (entity as ICreationAudited).CreatorUserId = _hozaruSession.Value.UserId;
                    }
                }
            }

            //Set TenantId for new entity
            if (entity is IMustHaveTenant)
            {
                var currentTenantId = _hozaruSession.Value.GetTenantId();
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "TenantId")
                    {
                        state[i] = (entity as IMustHaveTenant).TenantId = currentTenantId;
                    }
                }
            }

            if (entity is IModificationAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "LastModificationTime")
                    {
                        state[i] = (entity as IModificationAudited).LastModificationTime = Clock.Now;
                    }
                    else if (propertyNames[i] == "LastModifierUserId")
                    {
                        state[i] = (entity as IModificationAudited).LastModifierUserId = _hozaruSession.Value.UserId;
                    }
                }
            }

            //if (entity is IMayHaveTenant)
            //{
            //    var currentTenantId = _hozaruSession.Value.TenantId;
            //    for (var i = 0; i < propertyNames.Length; i++)
            //    {
            //        if (propertyNames[i] == "TenantId")
            //        {
            //            state[i] = (entity as IMayHaveTenant).TenantId = currentTenantId;
            //        }
            //    }
            //}

            //EntityChangedEventHelper.TriggerEntityCreatedEvent(entity);

            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            //TODO@Halil: Implement this when tested well (Issue #49)
            ////Prevent changing CreationTime on update 
            //if (entity is IHasCreationTime)
            //{
            //    for (var i = 0; i < propertyNames.Length; i++)
            //    {
            //        if (propertyNames[i] == "CreationTime" && previousState[i] != currentState[i])
            //        {
            //            throw new HozaruException(string.Format("Can not change CreationTime on a modified entity {0}", entity.GetType().FullName));
            //        }
            //    }
            //}

            //Prevent changing CreatorUserId on update
            //if (entity is ICreationAudited)
            //{
            //    for (var i = 0; i < propertyNames.Length; i++)
            //    {
            //        if (propertyNames[i] == "CreatorUserId" && previousState[i] != currentState[i])
            //        {
            //            throw new HozaruException(string.Format("Can not change CreatorUserId on a modified entity {0}", entity.GetType().FullName));
            //        }
            //    }
            //}

            //Set modification audits
            if (entity is IModificationAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "LastModificationTime")
                    {
                        currentState[i] = (entity as IModificationAudited).LastModificationTime = Clock.Now;
                    }
                    else if (propertyNames[i] == "LastModifierUserId")
                    {
                        currentState[i] = (entity as IModificationAudited).LastModifierUserId = _hozaruSession.Value.UserId;
                    }
                }
            }

            //Set deletion audits
            if (entity is IDeletionAudited && (entity as IDeletionAudited).IsDeleted)
            {
                //Is deleted before? Normally, a deleted entity should not be updated later but I preferred to check it.
                var previousIsDeleted = false;
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "IsDeleted")
                    {
                        previousIsDeleted = (bool)previousState[i];
                        break;
                    }
                }

                if (!previousIsDeleted)
                {
                    for (var i = 0; i < propertyNames.Length; i++)
                    {
                        if (propertyNames[i] == "DeletionTime")
                        {
                            currentState[i] = (entity as IDeletionAudited).DeletionTime = Clock.Now;
                        }
                        else if (propertyNames[i] == "DeleterUserId")
                        {
                            currentState[i] = (entity as IDeletionAudited).DeleterUserId = _hozaruSession.Value.UserId;
                        }
                    }
                }
            }

            //if (entity is ISoftDelete && entity.As<ISoftDelete>().IsDeleted)
            //{
            //    EntityChangedEventHelper.TriggerEntityDeletedEvent(entity);
            //}
            //else
            //{
            //    EntityChangedEventHelper.TriggerEntityUpdatedEvent(entity);
            //}

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //EntityChangedEventHelper.TriggerEntityDeletedEvent(entity);

            base.OnDelete(entity, id, state, propertyNames, types);
        }
    }
}
