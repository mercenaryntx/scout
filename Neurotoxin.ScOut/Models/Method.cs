﻿using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Neurotoxin.ScOut.Models
{
    public class Method : SyntaxCodePart
    {
        public MethodDeclarationSyntax Declaration { get; set; }
        public override string ToString() => FullName;
    }
}