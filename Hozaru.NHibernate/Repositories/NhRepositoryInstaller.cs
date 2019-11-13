﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hozaru.Core.Domain.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.NHibernate.Repositories
{
    internal class NhRepositoryInstaller : IWindsorInstaller
    {
        private readonly ISessionFactory _sessionFactory;

        public NhRepositoryInstaller(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(() => _sessionFactory).LifeStyle.Singleton,
                Component.For(typeof(IRepository<>)).ImplementedBy(typeof(NhRepositoryBase<>)).LifestyleTransient(),
                Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(NhRepositoryBase<,>)).LifestyleTransient()
                );
        }
    }
}
