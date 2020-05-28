using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
//using Microsoft.AspNetCore.Razor;
//using Microsoft.AspNetCore.Razor.Language;
//using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
//using Microsoft.AspNetCore.Razor.Language.Components;
//using Microsoft.AspNetCore.Razor.Language.Extensions;
//using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace MiniAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MiniRule2 : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "XMini";

        private string logFN;

        private static DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, "XXX", "{0}", "MMF-1979", DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(rule); } }

        public override void Initialize(AnalysisContext context)
        {
            logFN = $@"C:\_Temp\Log {DateTime.Now.ToString("yyyy-MM-dd HHmmss")}.txt";
            if (!File.Exists(logFN))
                File.WriteAllText(logFN, "");
            try
            {
                context.RegisterCompilationStartAction(
                    cc =>
                    {
                        cc.RegisterSyntaxNodeAction(
                            c =>
                            {
                                try
                                {
                                    //if (c.Node.ToString().Contains(@"Views\Home\Index.cshtml") ||
                                    //    c.Node.ToString().Contains(@"_Layout.cshtml") ||
                                    //    c.Node.ToString().Contains(@"_ViewStart.cshtml"))
                                    {
                                        //throw new System.Exception("Test");
                                        var msg = c.SemanticModel.GetDeclaredSymbol(c.Node as MethodDeclarationSyntax).ToMinimalDisplayString(c.SemanticModel, c.Node.GetLocation().SourceSpan.Start);
                                        Log("All: " + msg);
                                        //Log(Environment.StackTrace);
                                        Log(c.Node.SyntaxTree.ToString());

                                        var diagnostic = Diagnostic.Create(rule, c.Node.GetLocation(), msg);
                                        c.ReportDiagnostic(diagnostic);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log($"Error {ex.GetType().Name}: {ex.Message}\n\n{ex.ToString()}");
                                }
                            }, SyntaxKind.MethodDeclaration);
                    });
            }
            catch (Exception ex)
            {
                Log($"Main Error {ex.GetType().Name}: {ex.Message}\n\n{ex.ToString()}");
            }
        }

        private void Log(string msg)
        {
            File.AppendAllText(logFN, msg + "\n");
        }
    }
}
