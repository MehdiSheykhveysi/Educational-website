using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void RegisterDbSetClass<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                 .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (Type type in types)
                modelBuilder.Entity(type);
        }
        public static void SetUpDecimal(this ModelBuilder modelBuilder)
        {
            var property = modelBuilder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetProperties())
                            .Where(p => p.ClrType == typeof(decimal));
            foreach (var p in property)
            {
                p.Relational().ColumnType = "decimal(10, 2)";
            }
        }
    }
}

