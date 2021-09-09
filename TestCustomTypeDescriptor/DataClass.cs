using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestCustomTypeDescriptor
{
    public class DataClass : ICustomTypeDescriptor
    {
        private string _name;
        public string GetName() => _name;

        public string SetName(string value) => _name = value;

        AttributeCollection ICustomTypeDescriptor.GetAttributes() => CustomTypeDescriptor.GetAttributes();

        string ICustomTypeDescriptor.GetClassName() => CustomTypeDescriptor.GetClassName();

        string ICustomTypeDescriptor.GetComponentName() => CustomTypeDescriptor.GetComponentName();

        TypeConverter ICustomTypeDescriptor.GetConverter() => CustomTypeDescriptor.GetConverter();

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => CustomTypeDescriptor.GetDefaultEvent();

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => CustomTypeDescriptor.GetDefaultProperty();

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => CustomTypeDescriptor.GetEditor(editorBaseType);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => CustomTypeDescriptor.GetEvents();

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) => CustomTypeDescriptor.GetEvents(attributes);

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => CustomTypeDescriptor.GetProperties();

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) => CustomTypeDescriptor.GetProperties(attributes);

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => CustomTypeDescriptor.GetPropertyOwner(pd);

        private sealed class DataClassTypeDescriptor : CustomTypeDescriptor
        {
            private sealed class NameProperty : PropertyDescriptor
            {
                public static readonly NameProperty Singleton = new NameProperty();

                private NameProperty()
                    : base("Name", new Attribute[] { new RequiredAttribute() })
                {
                }

                public override Type PropertyType => typeof(string);

                public override Type ComponentType => typeof(DataClass);

                public override bool IsReadOnly => false;

                public override object GetValue(object component) => ((DataClass)component).GetName();

                public override void SetValue(object component, object value) => ((DataClass)component).SetName((string)value);

                public override bool CanResetValue(object component) => true;

                public override void ResetValue(object component) => ((DataClass)component).SetName(null);

                public override bool ShouldSerializeValue(object component) => false;
            }

            public static readonly DataClassTypeDescriptor Singleton = new DataClassTypeDescriptor();

            private DataClassTypeDescriptor()
            {
                Properties = new PropertyDescriptorCollection(new PropertyDescriptor[] { NameProperty.Singleton });
            }

            private PropertyDescriptorCollection Properties { get; }

            public override PropertyDescriptorCollection GetProperties()
            {
                return GetProperties(null);
            }

            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                bool filtering = attributes != null && attributes.Length > 0;
                if (!filtering)
                    return Properties;

                var result = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor prop in Properties)
                {
                    if (prop.Attributes.Contains(attributes))
                        result.Add(prop);
                }

                return result;
            }
        }

        private static ICustomTypeDescriptor CustomTypeDescriptor => DataClassTypeDescriptor.Singleton;
    }
}
