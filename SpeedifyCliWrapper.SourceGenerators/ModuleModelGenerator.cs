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

#if GEN_DEBUG
            Debugger.Launch();
#endif

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
                    .DescendantTokens()
                    .Where(dt => dt.IsKind(SyntaxKind.IdentifierToken))
                    .ToList();

                    var relatedClass = semanticModel.GetTypeInfo(nodes.Last().Parent);
                    var dcoratedClass = semanticModel.GetTypeInfo(declaredClass);
                    var classMethods = declaredClass.Members.Where(m => m.IsKind(SyntaxKind.MethodDeclaration)).ToList();

                    
                }
            }


        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Nothing to do here
        }
    }
}
