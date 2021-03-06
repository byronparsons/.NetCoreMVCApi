﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Repository.DataShaping
{
    public class DataShaper<T> : IDataShaper<T>
    {
        public PropertyInfo[] Properties{ get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);

            return FetchData(entities, requiredProperties);
        }

        public ShapedEntity ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);

            return FetchDataForEntity(entity, requiredProperties);
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(p => 
                                    p.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property == null) continue;

                    requiredProperties.Add(property);
                }
            }
            else
                requiredProperties = Properties.ToList();

            return requiredProperties;
        }

        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();

            shapedData.AddRange(from entity in entities
                                let shapedDataObj = FetchDataForEntity(entity, requiredProperties)
                                select shapedDataObj);
            return shapedData;

            //foreach (var entity in entities)
            //{
            //    var shapedDataObj1 = FetchDataForEntity(entity, requiredProperties);
            //    shapedData.Add(shapedDataObj1);
            //}
        }

        private ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObj = new ShapedEntity();

            foreach (var property in requiredProperties)
            {
                var objPropertyValue = property.GetValue(entity);
                shapedObj.Entity.TryAdd(property.Name, objPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObj.Id = (Guid)objectProperty.GetValue(entity);

            return shapedObj;
        }
    }
}
