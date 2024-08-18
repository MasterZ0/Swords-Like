using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Z3.Utils;

namespace Z3.GMTK2024.NgTasks
{
    public abstract class MapParameters<T> : ActionTask where T : ScriptableObject
    {
        [SerializeField] protected Parameter<T> data;

        public override string Info => $"Map Parameters {data}";

        protected override void StartAction()
        {
            //if (!Mapper.HasMap(data.GenericType, GraphRunner.ReferenceVariables.GetType()))
            //{

            //}

            PropertyInfo[] originProperties = data.Value.GetType().GetProperties();

            Dictionary<string, PropertyInfo> propertyMap = new();
            foreach (PropertyInfo destinationProperty in originProperties)
            {
                MapToAttribute mapTo = destinationProperty.GetCustomAttribute<MapToAttribute>();

                if (mapTo == null)
                {
                    propertyMap[destinationProperty.Name] = destinationProperty;
                }
                else
                {
                    propertyMap[mapTo.Parameter] = destinationProperty;
                }
            }

            List<FieldInfo> originFields = ReflectionUtils.GetAllFields(data.Value);

            Dictionary<string, FieldInfo> fieldMap = new();
            foreach (FieldInfo destinationProperty in originFields)
            {
                MapToAttribute mapTo = destinationProperty.GetCustomAttribute<MapToAttribute>();

                if (mapTo == null)
                {
                    fieldMap[destinationProperty.Name] = destinationProperty;
                }
                else
                {
                    fieldMap[mapTo.Parameter] = destinationProperty;
                }
            }

            foreach (VariableInstance variable in GraphController.ReferenceVariables.Values)
            {
                if (propertyMap.TryGetValue(variable.Name, out PropertyInfo propertyInfo) && variable.OriginalType == propertyInfo.PropertyType)
                {
                    variable.Value = propertyInfo.GetValue(data.Value);
                    continue;
                }
                else if (fieldMap.TryGetValue(variable.Name, out FieldInfo fieldInfo) && variable.OriginalType == fieldInfo.FieldType)
                {
                    variable.Value = fieldInfo.GetValue(data.Value);
                }
            }

            EndAction();
        }
    }
}
