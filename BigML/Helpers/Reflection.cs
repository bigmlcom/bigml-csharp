using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace BigML
{
    public static class Reflection
    {
        /// <summary>
        /// Compile lambda expression to an assembly
        /// </summary>
        public static Func<S, T> CompileToAssembly<S, T>(this LambdaExpression expression, IEnumerable<PropertyInfo> inputs, string assemblyName, string directoryName)
        {
            var domain = AppDomain.CurrentDomain;
            var assembly = new AssemblyName("Assembly");
            var assemblyBuilder = domain.DefineDynamicAssembly(assembly, AssemblyBuilderAccess.RunAndSave, directoryName);
            var module = assemblyBuilder.DefineDynamicModule("Module", assemblyName);
            var typeBuilder = module.DefineType("BigML", TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed);
            var methodBuilder = typeBuilder.DefineMethod("Predict", MethodAttributes.Public | MethodAttributes.Static, typeof(T), new[] { typeof(S) });
            expression.CompileToMethod(methodBuilder);
            var type = typeBuilder.CreateType();
            assemblyBuilder.Save(assemblyName);
            var predict = type.GetMethod("Predict");

            return delegate(S s)
            {
                var args = inputs.GetValue(s);
                return (T)Convert.ChangeType(predict.Invoke(null, args.ToArray()), typeof(T));
            };
        }

        /// <summary>
        /// Get values of given list of properties of a value.
        /// </summary>
        /// <param name="enumToName">Convert enums to name. Default true, since enums are categorical</param>
        public static IEnumerable<object> GetValue<T>(this IEnumerable<PropertyInfo> properties, T obj, object[] index = null, bool enumToName = true)
        {
            return from property in properties
                   let value = property.GetValue(obj, index)
                   select enumToName && value.GetType().IsEnum ? value.ToString() : value;
        }
    }
}