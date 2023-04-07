using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestRunner
{
    class Program
    {
        static void Report(MethodInfo method, string prefix, string message)
        {
            Console.WriteLine($"{prefix}{method.DeclaringType.Name} {method.Name} - {message}");
        }

        static void Run(MethodInfo testMethod)
        {
            object target = Activator.CreateInstance(testMethod.DeclaringType);
            try
            {
                testMethod.Invoke(target, new object[0]);
                Report(testMethod, "    ", "OK");
            }
            catch (TargetInvocationException ex) when (ex.InnerException != null)
            {
                Exception source = ex.InnerException;
                Report(testMethod, "*** ", $"{source.GetType().Name}: {source.Message}");
            }
            catch (TargetInvocationException ex)
            {
                Report(testMethod, "*** ", $"Failed to run test: {ex.Message}");
            }
        }

        static bool NameEndsWithTests(Type type) =>
            type.Name.EndsWith("Tests");

        static bool HasParameterlessConstructor(Type type) =>
            type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Any(ctor => ctor.GetParameters().Length == 0);

        static IEnumerable<Type> GetTestTypes(string assemblyPath) =>
            Assembly.LoadFrom(assemblyPath)
                .DefinedTypes
                .Where(type => HasParameterlessConstructor(type) && NameEndsWithTests(type));

        static IEnumerable<MethodInfo> GetTestMethods(string assemblyPath) =>
            GetTestTypes(assemblyPath)
                .SelectMany(type =>
                    type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(method =>
                            method.ReturnType == typeof (void) &&
                            !method.IsGenericMethod &&
                            method.GetParameters().Length == 0));

        static void RunTests(string assemblyPath)
        {
            Console.WriteLine(new string('_', 80));
            Console.WriteLine("TEST REPORT");
            foreach (MethodInfo testMethod in GetTestMethods(assemblyPath))
                Run(testMethod);
            Console.WriteLine(new string('_', 80));
        }

        static void Main(string[] args)
        {
            RunTests(args[0]);
        }
    }
}
