using System;
using System.IO;
using System.Threading;
using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace CobolWow.Tools
{
   public class VanillaPlugin
   {
      public virtual void Unload()
      {

      }
   }

   public class ScriptCompiler
   {
      public VanillaPlugin Plugin { get { return instance; } }
      private Type type;
      private VanillaPlugin instance;

      public String Name;

      public bool Compile(string filepath)
      {
         Name = Path.GetFileNameWithoutExtension(filepath);
         Thread.Sleep(100);
         string code = File.ReadAllText(filepath);

         CSharpCodeProvider codeProvider = new CSharpCodeProvider(new Dictionary<String, String> { { "CompilerVersion", "v3.5" } });
         CompilerParameters parameters = new CompilerParameters();
         parameters.GenerateInMemory = true;
         parameters.GenerateExecutable = false;
         parameters.IncludeDebugInformation = true;

         //Add references used in scripts
         parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location); //Current application ('using milkshake;')
         parameters.ReferencedAssemblies.Add("System.dll");
         parameters.ReferencedAssemblies.Add("System.Core.dll");
         parameters.ReferencedAssemblies.Add("System.Data.dll");
         parameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
         parameters.ReferencedAssemblies.Add("System.Xml.dll");
         parameters.ReferencedAssemblies.Add("System.Xml.Linq.dll");

         CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, code);
         if (results.Errors.HasErrors)
         {
            string error = "Error in script: " + Name;

            foreach (CompilerError e in results.Errors)
            {
               error += "\n" + e;
            }

            Logger.Log(LogType.Error, error);
            return false;
         }
         //Successful Compile
         Logger.Log(LogType.Debug, "Script Loaded: " + Name);

         type = results.CompiledAssembly.GetTypes()[0];

         //Instansiate script class.
         try
         {
            if (type.BaseType == typeof(VanillaPlugin))
            {
               instance = Activator.CreateInstance(type) as VanillaPlugin;
            }
            else
            {
               Logger.Log(LogType.Error, "Warning! " + Name + " isn't VanillaPlugin");
               return false;
            }

         }
         catch (Exception)
         {
            Logger.Log(LogType.Error, "Error instantiating " + Name);
            return false;
         }

         return true;
      }

      public object RunMethod(string methodName, object[] args = null)
      {
         return type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, instance, args);
         //return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
      }

      public object RunStaticMethod(string methodName, object[] args = null)
      {
         return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
      }
   }
}
