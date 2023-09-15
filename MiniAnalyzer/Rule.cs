using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;


namespace MiniAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MiniRule2 : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "XMini";

        private string fnPrefix;

        private static DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, "XXX", "{0}", "MMF-1979", DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(c =>
            {
                var message = c.Options.AdditionalFiles.Select(x => x.Path + ", " + x.GetText()?.ToString()).FirstOrDefault() ?? "N/A";
                //c.SemanticModel.GetNullableContext(42);


                c.ReportDiagnostic(Diagnostic.Create(rule, c.Node.GetLocation(), message));
            }, SyntaxKind.ClassDeclaration);


            //fnPrefix = $@"C:\_Temp\Log {DateTime.Now:yyyy-MM-dd HHmmss}";
            //try
            //{
            //    context.RegisterCompilationStartAction(
            //        cc =>
            //        {
            //            cc.RegisterSyntaxNodeAction(
            //                c =>
            //                {
            //                    try
            //                    {
            //                        if (c.Node.ToString().Contains(@"Views\Home\Index.cshtml") ||
            //                            c.Node.ToString().Contains(@"_Layout.cshtml") ||
            //                            c.Node.ToString().Contains(@"_ViewStart.cshtml")
            //                            && !c.Node.ToString().Contains("PrecompiledSample"))
            //                        {
            //                            var msg = c.SemanticModel.GetDeclaredSymbol(c.Node as MethodDeclarationSyntax).ToMinimalDisplayString(c.SemanticModel, c.Node.GetLocation().SourceSpan.Start);
            //                            Log(c.Node.SyntaxTree.ToString(), msg);

            //                            //var diagnostic = Diagnostic.Create(rule, c.Node.GetLocation(), msg);
            //                            //c.ReportDiagnostic(diagnostic);
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        Log($"Error {ex.GetType().Name}: {ex.Message}\n\n{ex}", "Error");
            //                    }
            //                }, SyntaxKind.MethodDeclaration);
            //        });
            //}
            //catch (Exception ex)
            //{
            //    Log($"Main Error {ex.GetType().Name}: {ex.Message}\n\n{ex.ToString()}", "Error");
            //}
        }

        private void Log(string msg, string fnInfix)
        {
            foreach(var c in Path.GetInvalidFileNameChars())
            {
                fnInfix = fnInfix.Replace(c, '-');
            }
            for (var i = 0; i < 100; i++)
            {
                var fn = $"{fnPrefix} {fnInfix} {i:00}.txt";
                if (!File.Exists(fn))
                {
                    File.WriteAllText(fn, msg);
                    return;
                }

            }
        }
    }
}
