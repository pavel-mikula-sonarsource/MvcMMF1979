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

namespace MiniAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MiniRule : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "MiniAnalyzer";

        private string logFN;

        private static DiagnosticDescriptor rule = new DiagnosticDescriptor(DiagnosticId, "XXX", "{0}", "MMF-1979", DiagnosticSeverity.Warning, isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(rule); } }

        public override void Initialize(AnalysisContext context)
        {
            logFN = $@"C:\_Temp\Log {DateTime.Now.ToString("yyyy-MM-dd HHmmss")}.txt";
            if (!File.Exists(logFN))
                File.WriteAllText(logFN, "");

            context.RegisterCompilationStartAction(
                cc =>
                {
                    cc.RegisterSyntaxNodeAction(
                        c =>
                        {
                            if (c.Node.ToString().Contains(@"Views\Home\Index.cshtml"))
                            {
                                var msg = c.SemanticModel.GetDeclaredSymbol(c.Node as MethodDeclarationSyntax).ToMinimalDisplayString(c.SemanticModel, c.Node.GetLocation().SourceSpan.Start);
                                Log("All: " + msg);

                                var diagnostic = Diagnostic.Create(rule, c.Node.GetLocation(), msg);
                                c.ReportDiagnostic(diagnostic);
                            }
                        }, SyntaxKind.MethodDeclaration);
                });
        }

        private void Log(string msg)
        {
            File.AppendAllText(logFN, msg + "\n");
        }
    }
}
