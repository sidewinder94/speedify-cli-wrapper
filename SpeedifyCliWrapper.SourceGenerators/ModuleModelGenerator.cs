using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpeedifyCliWrapper.SourceGenerators.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpeedifyCliWrapper.SourceGenerators
{
    [Generator]
    class ModuleModelGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var attributeSymbol = context.Compilation.GetTypeByMetadataName(typeof(RelatedModelAttribute).FullName);

            var classWithAttributes = context.Compilation.SyntaxTrees.Where(st => st.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                    .Any(p => p.DescendantNodes().OfType<AttributeSyntax>().Any()));

            foreach (SyntaxTree tree in classWithAttributes)
            {
                var semanticModel = context.Compilation.GetSemanticModel(tree);
                
                foreach(var declaredClass in tree
                    .GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Where(cd => cd.DescendantNodes().OfType<AttributeSyntax>().Any()))
                {
                    var nodes = declaredClass
                    .DescendantNodes()
                    .OfType<AttributeSyntax>()
                    .FirstOrDefault(a => a.DescendantTokens().Any(dt => dt.IsKind(SyntaxKind.IdentifierToken) && semanticModel.GetTypeInfo(dt.Parent).Type.Name == attributeSymbol.Name))
                    ?.DescendantTokens()
                    ?.Where(dt => dt.IsKind(SyntaxKind.IdentifierToken))
                    ?.ToList();

                    if(nodes == null)
                    {
                        continue;
                    }

                    var relatedClass = semanticModel.GetTypeInfo(nodes.Last().Parent);

                    var generatedClass = this.GenerateClass(relatedClass);

                    foreach(MethodDeclarationSyntax classMethod in declaredClass.Members.Where(m => m.IsKind(SyntaxKind.MethodDeclaration)).OfType<MethodDeclarationSyntax>())
                    {
#if GEN_DEBUG
                        Debugger.Launch();
#endif
                        var method = this.GenerateMethod(classMethod, generatedClass);
                    }
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Nothing to do here
        }

        private string GenerateMethod(MethodDeclarationSyntax methodDeclaration, StringBuilder builder)
        {
            return String.Empty;
        }

        private StringBuilder GenerateClass(TypeInfo relatedClass)
        {
            var sb = new StringBuilder();

            sb.Append(@"
using System;
using System.Collections.Generic;
using SpeedifyCliWrapper.Common;

namespace SpeedifyCliWrapper.ReturnTypes
{
    public partial class " + relatedClass.Type.Name);

            sb.Append(@"
{");

            return sb;
        }
    }
}
