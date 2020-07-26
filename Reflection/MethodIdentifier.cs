using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Serilog;
using Serilog.Core;
using TestPlanTools.Attributes;
using TestPlanTools.Models;

namespace TestPlanTools.Refelection
{
    public static class MethodIdentifier
    {
        private static Assembly _assembly;

        public static List<AttributedMethod> GetAllAttributedMethodsInCallingAssembly<T>() where T: Attribute
        {
            
            _assembly = Assembly.GetCallingAssembly();
            var methodList = new List<AttributedMethod>();

            foreach (var ns in _assembly
                                .GetTypes()
                                .Select(t => t.Namespace)
                                .Distinct())
            {
                methodList.AddRange(GetAllAttributedMethodsInNamespace<T>(ns));
            }
            return methodList;
        }
        private static List<AttributedMethod> GetAllAttributedMethodsInNamespace<T>(string nameSpace) where T: Attribute
        {
            var filename = GetCallingAssemblyFileName();
            var types = _assembly.GetTypes().Where(t => t.IsClass && t.Namespace == @nameSpace);
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
        private static string GetCallingAssemblyFileName() =>
            Path.GetFileName(_assembly.CodeBase);
    }
}