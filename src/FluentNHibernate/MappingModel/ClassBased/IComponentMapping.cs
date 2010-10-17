using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.Collections;

namespace FluentNHibernate.MappingModel.ClassBased
{
    public interface IComponentMapping : IMappingBase, IMemberMapping
    {
        bool HasColumnPrefix { get; }
        string ColumnPrefix { get; set; }
        ParentMapping Parent { get; set; }
        bool Insert { get; set; }
        bool Update { get; set; }
        string Access { get; set; }
        Type ContainingEntityType { get; }
        string Name { get; set; }
        Type Type { get; }
        bool OptimisticLock { get; set; }
        bool Unique { get; }
        IEnumerable<ManyToOneMapping> References { get; }
        IEnumerable<ICollectionMapping> Collections { get; }
        IEnumerable<PropertyMapping> Properties { get; }
        IEnumerable<IComponentMapping> Components { get; }
        IEnumerable<OneToOneMapping> OneToOnes { get; }
        IEnumerable<AnyMapping> Anys { get; }
        ComponentType ComponentType { get; }
        TypeReference Class { get; set; }
        bool Lazy { get; set; }
        void AddProperty(PropertyMapping mapping);
        void AddComponent(IComponentMapping mapping);
        void AddOneToOne(OneToOneMapping mapping);
        void AddCollection(ICollectionMapping mapping);
        void AddReference(ManyToOneMapping mapping);
        void AddAny(AnyMapping mapping);
        bool HasValue(string property);
    }
}