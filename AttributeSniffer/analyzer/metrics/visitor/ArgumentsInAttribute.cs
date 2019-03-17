﻿using System.Collections.Generic;
using AttributeSniffer.analyzer.model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AttributeSniffer.analyzer.metrics.visitor
{
    /// <summary>
    /// Visit a compilation unit to extract the AA metric.
    /// </summary>
    class ArgumentsInAttribute : CSharpSyntaxWalker, MetricCollector
    {
        private SemanticModel SemanticModel { get; set; }
        private int NumberOfArguments { get; set; }
        private AttributeSyntax VisitedAttribute { get; set; }
        private List<MetricResult> ResultsByElement { get; set; } = new List<MetricResult>();

        public override void VisitAttribute(AttributeSyntax node)
        {
            this.VisitedAttribute = node;

            NumberOfArguments = 0;
            if (node.ArgumentList != null)
            {
                NumberOfArguments = node.ArgumentList.Arguments.Count;
            }

            ResultsByElement.Add(GetResult());
        }

        public void SetSemanticModel(SemanticModel semanticModel)
        {
            this.SemanticModel = semanticModel;
        }

        public void SetResult(List<MetricResult> metricResults)
        {
            metricResults.AddRange(ResultsByElement);
        }

        private MetricResult GetResult()
        {
            ISymbol parentElementSymbol = SemanticModel.GetDeclaredSymbol(VisitedAttribute.Parent.Parent);
            string parentIdentifier = parentElementSymbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);

            string elementType = "Attribute";
            string elementIdentifier = string.Format("{0}.{1}", parentIdentifier, VisitedAttribute.Name.ToString());
            string metricName = Metric.ARGUMENTS_IN_ATTRIBUTE.GetIdentifier();
            return new MetricResult(elementIdentifier, elementType, metricName, NumberOfArguments);
        }
    }
}
