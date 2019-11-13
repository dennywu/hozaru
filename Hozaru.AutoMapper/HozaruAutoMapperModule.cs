﻿using Castle.Core.Logging;
using Hozaru.Core;
using Hozaru.Core.Modules;
using Hozaru.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Hozaru.AutoMapper
{
    [DependsOn(typeof(HozaruKernelModule))]
    public class HozaruAutoMapperModule : HozaruModule
    {
        public ILogger Logger { get; set; }

        private readonly ITypeFinder _typeFinder;

        private static bool _createdMappingsBefore;
        private static readonly object _syncObj = new object();

        public HozaruAutoMapperModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void PostInitialize()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            lock (_syncObj)
            {
                //We should prevent duplicate mapping in an application, since AutoMapper is static.
                if (_createdMappingsBefore)
                {
                    return;
                }

                FindAndAutoMapTypes();
                CreateOtherMappings();

                _createdMappingsBefore = true;
            }
        }

        private void FindAndAutoMapTypes()
        {
            var types = _typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapAttribute)) ||
                type.IsDefined(typeof(AutoMapFromAttribute)) ||
                type.IsDefined(typeof(AutoMapToAttribute))
                );

            Logger.DebugFormat("Found {0} classes defines auto mapping attributes", types.Length);
            foreach (var type in types)
            {
                Logger.Debug(type.FullName);
                AutoMapperHelper.CreateMap(type);
            }
        }

        private void CreateOtherMappings()
        {
            //var localizationManager = IocManager.Resolve<ILocalizationManager>();
            //Mapper.CreateMap<LocalizableString, string>().ConvertUsing(ls => localizationManager.GetString(ls));
        }
    }
}
