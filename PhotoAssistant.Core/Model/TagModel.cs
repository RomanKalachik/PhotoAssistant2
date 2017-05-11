using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    public class DmTag : ISupportId {
        public DmTag() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public string Value { get; set; }
        public Color Color { get; set; }
        public TagType Type { get; set; }
        public long AddCount { get; set; }
        public long TimeStamp { get; set; }

        public override string ToString() {
            return Value;
        }
    }

    public class DmTagNode {
        public DmTagNode() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public TagType Type { get; set; }
        public DmTag Tag { get; set; }
        public Guid ParentId { get; set; }
        DmTagNode parent;
        [ForeignKey("ParentId")]
        public DmTagNode Parent {
            get { return parent; }
            set {
                parent = value;
                ParentId = Parent == null ? Guid.Empty : Parent.Id;
            }
        }
        [NotMapped]
        public string Text { get { return Tag.Value; } }

        public virtual ICollection<DmTagNode> Children { get; set; }

        public override string ToString() {
            return Tag.Value;
        }

        public List<DmTagNode> GetPath() {
            List<DmTagNode> res = new List<DmTagNode>();
            DmTagNode node = this;
            while(node != null) {
                res.Add(node);
                node = node.Parent;
            }
            return res;
        }
    }

    public class DmTagNodeReversed {
        public DmTagNodeReversed() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public TagType Type { get; set; }
        public DmTag Tag { get; set; }
        public Guid ParentId { get; set; }
        DmTagNodeReversed parent;
        [ForeignKey("ParentId")]
        public DmTagNodeReversed Parent {
            get { return parent; }
            set {
                parent = value;
                ParentId = Parent == null ? Guid.Empty : Parent.Id;
            }
        }

        public virtual ICollection<DmTagNodeReversed> Children { get; set; }

        public DmTagNodeReversed GetRootNode() {
            DmTagNodeReversed node = this;
            while(node.Parent != null)
                node = node.Parent;
            return node;
        }

        public List<DmTagNodeReversed> GetPath() {
            List<DmTagNodeReversed> res = new List<DmTagNodeReversed>();
            DmTagNodeReversed node = this;
            while(node != null) {
                res.Add(node);
                node = node.Parent;
            }
            return res;
        }
        public override string ToString() {
            return Tag.Value;
        }
    }

    public interface IDmKeyword {
        Guid Id { get; set; }
        Guid FileId { get; set; }
        DmFile File { get; set; }
        DmTag Tag { get; set; }
        TagType Type { get; }
        object RemovedTag { get; set; }
    }

    public class DmKeyword : IDmKeyword, ISupportId {
        public DmKeyword() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public virtual DmFile File { get; set; }

        public virtual DmTag Tag { get; set; }

        public TagType Type { get { return TagType.Tag; } }

        [NotMapped]
        public object RemovedTag { get; set; }
    }

    public class DmPeople : IDmKeyword, ISupportId {
        public DmPeople() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public virtual DmFile File { get; set; }

        public virtual DmTag Tag { get; set; }

        public TagType Type { get { return TagType.People; } }

        [NotMapped]
        public object RemovedTag { get; set; }
    }

    public class DmCategory : IDmKeyword, ISupportId {
        public DmCategory() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public virtual DmFile File { get; set; }

        public virtual DmTag Tag { get; set; }

        public TagType Type { get { return TagType.Category; } }

        [NotMapped]
        public object RemovedTag { get; set; }
    }

    public class DmGenre : IDmKeyword, ISupportId {
        public DmGenre() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public virtual DmFile File { get; set; }

        public virtual DmTag Tag { get; set; }

        public TagType Type { get { return TagType.Genre; } }

        [NotMapped]
        public object RemovedTag { get; set; }
    }

    public class DmAutor : IDmKeyword, ISupportId {
        public DmAutor() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public virtual DmFile File { get; set; }

        public virtual DmTag Tag { get; set; }

        public TagType Type { get { return TagType.Autor; } }

        [NotMapped]
        public object RemovedTag { get; set; }
    }

    public class DmCollection : IDmKeyword, ISupportId {
        public DmCollection() {
            Id = Guid.NewGuid();
        }

        [Index(IsClustered = true, IsUnique = true)]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public virtual DmFile File { get; set; }

        public virtual DmTag Tag { get; set; }

        public TagType Type { get { return TagType.Collection; } }

        [NotMapped]
        public object RemovedTag { get; set; }
    }

    public enum TagType { Tag, People, Category, Genre, Autor, Collection }

    public class DmAutorMapping : EntityTypeConfiguration<DmAutor> {
        public DmAutorMapping() {
            HasKey((p) => p.Id);

            HasRequired((p) => p.File).WithMany((f) => f.Autors).HasForeignKey((t) => t.FileId).WillCascadeOnDelete(false);
        }
    }

    public class DmKeywordMapping : EntityTypeConfiguration<DmKeyword> {
        public DmKeywordMapping() {
            HasKey((p) => p.Id);

            HasRequired((p) => p.File).WithMany((f) => f.Keywords).HasForeignKey((t) => t.FileId).WillCascadeOnDelete(false);
        }
    }

    public class DmCategoryMapping : EntityTypeConfiguration<DmCategory> {
        public DmCategoryMapping() {
            HasKey((p) => p.Id);

            HasRequired((p) => p.File).WithMany((f) => f.Categories).HasForeignKey((t) => t.FileId).WillCascadeOnDelete(false);
        }
    }

    public class DmGenreMapping : EntityTypeConfiguration<DmGenre> {
        public DmGenreMapping() {
            HasKey((p) => p.Id);

            HasRequired((p) => p.File).WithMany((f) => f.Genres).HasForeignKey((t) => t.FileId).WillCascadeOnDelete(false);
        }
    }

    public class DmPeopleMapping : EntityTypeConfiguration<DmPeople> {
        public DmPeopleMapping() {
            HasKey((p) => p.Id);

            HasRequired((p) => p.File).WithMany((f) => f.Peoples).HasForeignKey((t) => t.FileId).WillCascadeOnDelete(false);
        }
    }

    public class DmCollectionMapping : EntityTypeConfiguration<DmCollection> {
        public DmCollectionMapping() {
            HasKey((p) => p.Id);
            HasRequired((p) => p.File).WithMany((f) => f.Collections).HasForeignKey((t) => t.FileId).WillCascadeOnDelete(false);
        }
    }

    public class DmTagNodeMapping : EntityTypeConfiguration<DmTagNode> {
        public DmTagNodeMapping() {
            HasKey(n => n.Id);
            HasRequired(n => n.Parent).WithMany(p => p.Children).HasForeignKey(n => n.ParentId).WillCascadeOnDelete(false);
        }
    }

    public class DmTagNodeReversedMapping : EntityTypeConfiguration<DmTagNodeReversed> {
        public DmTagNodeReversedMapping() {
            HasKey(n => n.Id);
            HasRequired(n => n.Parent).WithMany(p => p.Children).HasForeignKey(n => n.ParentId).WillCascadeOnDelete(false);
        }
    }
}
