﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Neurotoxin.ScOut.Models;

namespace Neurotoxin.ScOut.Mappers
{
    public class SourceFileMapper : ISourceFileMapper // IMapper<SyntaxTree, SourceFile>
    {
        private readonly IClassMapper _classMapper;

        public SourceFileMapper(IClassMapper classMapper)
        {
            _classMapper = classMapper;
        }

        public SourceFile Map(SyntaxTree tree, Compilation compilation)
        {
            var root = tree.GetRootAsync().GetAwaiter().GetResult();
            var model = compilation.GetSemanticModel(tree);
            return new SourceFile
            {
                Path = tree.FilePath,
                Classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>().Select(s => _classMapper.Map(s, model)).ToArray(),
            };
        }
    }

    public interface ISourceFileMapper
    {
        SourceFile Map(SyntaxTree tree, Compilation compilation);
    }
}