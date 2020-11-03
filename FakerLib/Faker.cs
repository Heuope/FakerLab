using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakerLib
{
    public class Faker
    {
        private IGeneratorContext _context;
        private Stack<Type> _types;

        public Faker(IGeneratorContext generatorContext)
        {
            _context = generatorContext;
            _types = new Stack<Type>();
        }

        public T Create<T>()
        {
           return (T)Create(typeof(T));
        }

        public object Create(Type t)
        {
            if (_types.Contains(t))
            {                
                return null;
            }

            _types.Push(t);

            var temp = _context.Generate(t);

            if (temp != null)
            {
                _types.Pop();
                return temp;
            }    

            var constructorInfos = t.GetConstructors();          
            var propertyInfos = t.GetProperties();
            var fieldInfos = t.GetFields();

            constructorInfos = constructorInfos.OrderByDescending(x => x.GetParameters().Count()).ToArray();

            var paramList = new List<object>();
            var obj = new object();           

            foreach (var constructor in constructorInfos)
            {
                foreach (var param in constructor.GetParameters())
                {
                    paramList.Add(Create(param.ParameterType));
                }
                try
                {
                    obj = Activator.CreateInstance(t, paramList.ToArray());
                    break;
                }
                catch (Exception)
                {
                    paramList.Clear();
                }
            }
            if (obj == null)
            {
                try
                {
                    obj = Activator.CreateInstance(t);
                }
                catch (Exception)
                {
                    return null;
                }
            }          

            foreach (var field in fieldInfos)
            {
                var fieldTemp = Create(field.FieldType);

                if (fieldTemp != null)
                {                    
                    try
                    {
                        field.SetValue(obj, fieldTemp);
                    }
                    catch (Exception)
                    {
                        field.SetValue(obj, default);
                    }                    
                }
                else
                {
                    field.SetValue(obj, default);
                }               
            }
            foreach (var property in propertyInfos)
            {
                var propertyTemp = Create(property.PropertyType);

                if (propertyTemp != null)
                {
                    try
                    {
                        property.SetValue(obj, propertyTemp);
                    }
                    catch (Exception)
                    {
                        property.SetValue(obj, default);
                    }
                }
                else
                {
                    property.SetValue(obj, default);
                }
            }

            _types.Pop();
            return obj;
        }
    }
}
