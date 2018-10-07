using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Easeware.Remsng.Data
{
    public static class AutoMapperProfileExtension
    {
        //public static IMappingExpression<TSource, TDestination> IgnoreAllUnmapped<TSource,
        //    TDestination>(this IMappingExpression<TSource, TDestination> expression)
        //{
        //    var sourceType = typeof(TSource);
        //    var destinationType = typeof(TDestination);

        //    var existingMaps = Mapper.Configuration.GetAllTypeMaps()..First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));

        //    foreach (var property in existingMaps.GetUnmappedPropertyNames())
        //        expression.ForMember(property, opt => opt.Ignore());

        //    return expression;
        //}
    }
}
