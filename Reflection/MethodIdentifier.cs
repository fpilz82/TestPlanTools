using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Serilog;

namespace TestPlanTools.Refelection
{
    public static class MethodIdentifier
    {

        public static List<AttributedMethod> GetAllAttributedMethodsInAssemblies<T>() where T: Attribute
        {
            var methodList = new List<AttributedMethod>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(var assembly in assemblies)
            {

                foreach (var ns in assembly
                                    .GetTypes()
                                    .Select(t => t.Namespace)
                                    .Distinct())
                {
                    methodList.AddRange(GetAllAttributedMethodsInNamespace<T>(assembly, ns));
                }





            }





            return methodList;
        }
        private static List<AttributedMethod> GetAllAttributedMethodsInNamespace<T>(Assembly assembly, string nameSpace) where T: Attribute
        {
            var filename = GetAssemblyFileName(assembly);
            var types = assembly.GetTypes().Where(t => t.IsClass && t.Namespace == @nameSpace);
            var resultList = new List<AttributedMethod>();

            foreach (var type in types)
            {
                foreach (var method in type.GetMethods())
                {
                    T attribute = (T)Attribute.GetCustomAttribute(method, typeof(T));
                    
                    if (attribute != null)
                    {
                        Log.Logger.Information("Found method '{0}' in namespace '{1}'", method.Name, type.FullName);
                        resultList.Add(new AttributedMethod
                        {
                            Type = type,
                            MethodInfo = method,
                            AssemblyName = filename
                        });
                    }
                }
            }
            return resultList;
        }
        private static string GetAssemblyFileName(Assembly assembly) =>
            Path.GetFileName(assembly.CodeBase);
    }
}