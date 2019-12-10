using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wto.Library.Core.AutoMapper {
    public static class AutoMapperHelper {
        #region -- Functions --

        public static IList<Map> LoadStandardMappings(Assembly rootAssembly) {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                from type in types
                from instance in type.GetInterfaces()
                where
                    instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                    !type.IsAbstract &&
                    !type.IsInterface
                select new Map {
                    Source = type.GetInterfaces().First().GetGenericArguments().First(),
                    Destination = type
                }).ToList();

            return mapsFrom;
        }

        public static IList<IMapping> LoadCustomMappings(Assembly rootAssembly) {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (
                from type in types
                from instance in type.GetInterfaces()
                where
                    typeof(IMapping).IsAssignableFrom(type) &&
                    !type.IsAbstract &&
                    !type.IsInterface
                select (IMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }


        #endregion
    }
}