﻿using EleCho.GoCqHttpSdk.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class CqPostPluginCodeGen
    {
        private static Type[] GetAllCqPostContextTypes()
        {
            Type baseType = typeof(CqPostContext);

            Assembly asm = baseType.Assembly;
            return asm.GetTypes()
                .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();
        }

        public static string Generate()
        {
            List<(string baseName, string fieldName, string methodName, string asyncMethodName)> allTypesInfo = GetAllCqPostContextTypes()
                .Select(t => t.Name["Cq".Length..^("PostContext".Length)])
                .Select(baseName => (baseName, $"{char.ToLower(baseName[0])}{baseName[1..]}PC", $"On{baseName}", $"On{baseName}Async"))
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using EleCho.GoCqHttpSdk.Post;");
            sb.AppendLine("using System.Threading.Tasks;");

            sb.AppendLine("namespace EleCho.GoCqHttpSdk");
            sb.AppendLine("{");
            sb.AppendLine("    public class CqPostPlugin");
            sb.AppendLine("    {");
            sb.AppendLine("        public async Task Execute(CqPostContext context, Func<Task> next)");
            sb.AppendLine("        {");
            sb.AppendLine("            switch (context)");
            sb.AppendLine("            {");
            
            foreach (var info in allTypesInfo)
            {
                sb.AppendLine($"                case Cq{info.baseName}PostContext {info.fieldName}:");
                sb.AppendLine($"                    {info.methodName}({info.fieldName});");
                sb.AppendLine($"                    await {info.asyncMethodName}({info.fieldName});");
                sb.AppendLine($"                    break;");
            }
            
            sb.AppendLine("            }");
            sb.AppendLine();
            sb.AppendLine("            await next()");
            sb.AppendLine("        }");

            sb.AppendLine();

            foreach (var info in allTypesInfo)
                sb.AppendLine($"        public virtual void {info.methodName}(Cq{info.baseName}PostContext context) {{ }}");

            sb.AppendLine();

            foreach (var info in allTypesInfo)
                sb.AppendLine($"        public virtual  Task {info.methodName}(Cq{info.baseName}PostContext context) => Task.CompletedTask");

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}