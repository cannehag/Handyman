﻿using System;
using System.Linq;

namespace Handyman.DataContractValidator.Objects
{
    internal class ObjectValidator : IValidator
    {
        private readonly object _dataContract;

        public ObjectValidator(object dataContract)
        {
            _dataContract = dataContract;
        }

        public string GetTypeName(ValidationContext context)
        {
            return "object";
        }

        public void Validate(Type type, ValidationContext context)
        {
            var actualProperties = type.GetProperties();

            var expectedProperties = _dataContract is Type dataContractType
                ? dataContractType.GetProperties().ToDictionary(x => x.Name, x => (object)x.PropertyType)
                : _dataContract.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(_dataContract, null));

            var names = actualProperties
                .Select(x => x.Name)
                .Concat(expectedProperties.Keys)
                .OrderBy(x => x)
                .Distinct();

            foreach (var name in names)
            {
                using (context.CreateScope(name))
                {
                    var actualProperty = actualProperties.FirstOrDefault(x => x.Name == name);

                    if (actualProperty == null)
                    {
                        context.AddError("expected property not found");
                        continue;
                    }

                    var actualPropertyIsIgnored = actualProperty.GetCustomAttributes(false)
                        .Any(x => x.GetType().Name == "JsonIgnoreAttribute");

                    if (!expectedProperties.TryGetValue(name, out var expectedProperty))
                    {
                        if (actualPropertyIsIgnored)
                            continue;

                        context.AddError("unexpected property");
                        continue;
                    }

                    if (actualPropertyIsIgnored)
                    {
                        context.AddError("expected property found but decorated with ignore attribute");
                        continue;
                    }

                    context.Validate(actualProperty.PropertyType, expectedProperty);
                }
            }
        }
    }
}