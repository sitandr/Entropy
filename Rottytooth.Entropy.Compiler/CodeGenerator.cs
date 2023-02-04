using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.IO;

namespace Rottytooth.Entropy
{
    internal class CodeGenerator
    {
        internal static void Compile(string cSharpCode, string outputFileName)
        {
            /*CSharpCodeProvider codeProvider = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider();

            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("Rottytooth.Entropy.dll");
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = outputFileName;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, cSharpCode);

            if (results.Errors.Count > 0)
            {
                foreach (System.CodeDom.Compiler.CompilerError CompErr in results.Errors)
                {
                    Console.Error.WriteLine(
                        "Line number " + CompErr.Line + 
                         ", Error Number: " + CompErr.ErrorNumber + 
                         ", '" + CompErr.ErrorText + ";\n");
                }
            }*/

            var syntaxTree = CSharpSyntaxTree.ParseText(cSharpCode);
            Console.WriteLine(typeof(object).Assembly.Location);

            CSharpCompilation compilation = CSharpCompilation.Create(
                "output.exe",
                new[] { syntaxTree },
                new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                        MetadataReference.CreateFromFile("Rottytooth.Entropy.dll"),
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location + "/../netstandard.dll"),
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location + "/../System.Runtime.dll"),
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location + "/../System.Console.dll")
                },

                new CSharpCompilationOptions(OutputKind.ConsoleApplication, optimizationLevel: OptimizationLevel.Release));
            
            using (var stream = new MemoryStream())
            {
                    var emitResult = compilation.Emit(stream);
                    if (!emitResult.Success)
                    {
                        foreach (var err in emitResult.Diagnostics)
                        {
                            Console.Error.WriteLine(err);
                        }
                    }
                else
                {
                    using (FileStream file = File.Create(outputFileName))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(file);
                    }
                    File.WriteAllText(outputFileName.Substring(outputFileName.Length-4, 4) +".runtimeconfig.json", @$"{{
                        ""runtimeOptions"": {{
                            ""tfm"": ""net{Environment.Version.Major}.{Environment.Version.Minor}"",
                            ""framework"": {{
                                ""name"": ""Microsoft.NETCore.App"",
                                ""version"": ""{Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}""
                            }}
                        }}
                    }}");
                }
            }

        }
    }
}
