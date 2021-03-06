﻿using System.Collections.Generic;

namespace AttributeSniffer.analyzer.metrics
{
    /// <summary>
    /// Struct to keep all the metrics supported by the system.
    /// </summary>
    public struct Metric
    {
        // Metrics
        public static Metric ATTRIBUTES_IN_CLASS
        {
            get { return new Metric("AC"); }
        }

        public static Metric UNIQUE_ATTRIBUTES_IN_CLASS
        {
            get { return new Metric("UAC"); }
        }

        public static Metric ATTRIBUTES_IN_ELEMENT_DECLARATION
        {
            get { return new Metric("AED"); }
        }

        public static Metric ARGUMENTS_IN_ATTRIBUTE
        {
            get { return new Metric("AA"); }
        }

        public static Metric LOC_IN_ATTRIBUTE_DECLARATION
        {
            get { return new Metric("LOCAD"); }
        }

        private string identifier;

        public string GetIdentifier() { return this.identifier; }

        public Metric(string identifier)
        {
            this.identifier = identifier;
        }

        public static List<Metric> GetMetrics()
        {
            return new List<Metric>
            {
                ATTRIBUTES_IN_CLASS,
                UNIQUE_ATTRIBUTES_IN_CLASS,
                ATTRIBUTES_IN_ELEMENT_DECLARATION,
                ARGUMENTS_IN_ATTRIBUTE,
                LOC_IN_ATTRIBUTE_DECLARATION
            };
        }

        public static Metric GetMetric(string identifier)
        {
            return GetMetrics().Find(metric => identifier.Equals(metric.GetIdentifier()));
        }
    }
}
