using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace TestCustomTypeDescriptor
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var data = new DataClass();

            Assert.Null(data.GetName());

            Assert.Single(TypeDescriptor.GetProperties(data));
            Assert.Single(TypeDescriptor.GetProperties(data, new Attribute[] { new RequiredAttribute() }));


            var validationContext = new ValidationContext(data, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(data, validationContext, validationResults, validateAllProperties: true);
            Assert.Single(validationResults);
        }
    }
}
