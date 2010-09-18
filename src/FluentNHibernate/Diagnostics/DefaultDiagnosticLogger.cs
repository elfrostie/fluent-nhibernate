﻿using System;
using System.Collections.Generic;

namespace FluentNHibernate.Diagnostics
{
    public class DefaultDiagnosticLogger : IDiagnosticLogger
    {
        readonly IDiagnosticMessageDespatcher despatcher;
        readonly List<ScannedSource> scannedSources = new List<ScannedSource>();
        readonly List<Type> classMaps = new List<Type>();
        readonly List<Type> conventions = new List<Type>();
        readonly List<SkippedAutomappingType> automappingSkippedTypes = new List<SkippedAutomappingType>();
        bool isDirty;

        public DefaultDiagnosticLogger(IDiagnosticMessageDespatcher despatcher)
        {
            this.despatcher = despatcher;
        }

        public void Flush()
        {
            if (!isDirty) return;

            var results = BuildResults();

            despatcher.Publish(results);
        }

        DiagnosticResults BuildResults()
        {
            return new DiagnosticResults(scannedSources, classMaps, conventions, automappingSkippedTypes);
        }

        public void FluentMappingDiscovered(Type type)
        {
            isDirty = true;
            classMaps.Add(type);
        }

        public void ConventionDiscovered(Type type)
        {
            isDirty = true;
            conventions.Add(type);
        }

        public void LoadedFluentMappingsFromSource(ITypeSource source)
        {
            isDirty = true;
            scannedSources.Add(new ScannedSource
            {
                Identifier = source.GetIdentifier(),
                Phase = ScanPhase.FluentMappings
            });
        }

        public void LoadedConventionsFromSource(ITypeSource source)
        {
            isDirty = true;
            scannedSources.Add(new ScannedSource
            {
                Identifier = source.GetIdentifier(),
                Phase = ScanPhase.Conventions
            });
        }

        public void AutomappingSkippedType(Type type, string reason)
        {
            isDirty = true;
            automappingSkippedTypes.Add(new SkippedAutomappingType
            {
                Type = type,
                Reason = reason
            });
        }
    }
}