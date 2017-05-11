using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAssistant.Core.Model {
    class DbContextConfiguration : DbConfiguration {
        public DbContextConfiguration() {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", GetProviderServices());
            SetProviderFactoryResolver(new SQLiteProviderFactoryResolver());
        }
        static System.Data.Entity.Core.Common.DbProviderServices GetProviderServices() {
            return (System.Data.Entity.Core.Common.DbProviderServices)
                    System.Data.SQLite.EF6.SQLiteProviderFactory.Instance.GetService(
                        typeof(System.Data.Entity.Core.Common.DbProviderServices));
        }
    }
    class SQLiteProviderFactoryResolver : IDbProviderFactoryResolver {
        DbProviderFactory IDbProviderFactoryResolver.ResolveProviderFactory(DbConnection connection) {
            if (connection is SQLiteConnection)
                return SQLiteFactory.Instance;
            if (connection is System.Data.Entity.Core.EntityClient.EntityConnection) {
                return System.Data.Entity.Core.EntityClient.EntityProviderFactory.Instance;
            }
            return null;
        }
    }



[DbConfigurationType(typeof(DbContextConfiguration))]
    public class DmContext : DbContext {
        public DmContext() : base() { }
        public DmContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
        public DmContext(DbConnection connection) : base(connection, true) { }

        public DbSet<DmFile> Files { get; set; }
        //public List<Filter> Filters { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<DmPeople> Peoples { get; set; }
        public DbSet<DmKeyword> Keywords { get; set; }
        public DbSet<DmCategory> Categories { get; set; }
        public DbSet<DmCollection> Collections { get; set; }
        public DbSet<DmGenre> Genres { get; set; }
        public DbSet<DmAutor> Autors { get; set; }
        public DbSet<DmColorLabel> ColorLabels { get; set; }
        public DbSet<MediaFormat> MediaFormat { get; set; }
        public DbSet<DbPropertiesModel> Properties { get; set; }
        public DbSet<DmStorageVolume> Volumes { get; set; }

        public DbSet<DmTag> Tags { get; set; }

        public DbSet<DmTagNode> TagNodes { get; set; }
        public DbSet<DmTagNodeReversed> TagNodesReversed { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore(new Type[] { typeof(System.Drawing.Image) });

            modelBuilder.Configurations.Add(new DmPeopleMapping());
            modelBuilder.Configurations.Add(new DmKeywordMapping());
            modelBuilder.Configurations.Add(new DmGenreMapping());
            modelBuilder.Configurations.Add(new DmCategoryMapping());
            modelBuilder.Configurations.Add(new DmAutorMapping());
            modelBuilder.Configurations.Add(new DmCollectionMapping());
            modelBuilder.Configurations.Add(new DmTagNodeMapping());
            modelBuilder.Configurations.Add(new DmTagNodeReversedMapping());
        }

        public void AddTag(IDmKeyword tagReference) {
            switch(tagReference.Type) {
                case TagType.Autor:
                    Autors.Add((DmAutor)tagReference);
                    break;
                case TagType.Category:
                    Categories.Add((DmCategory)tagReference);
                    break;
                case TagType.Genre:
                    Genres.Add((DmGenre)tagReference);
                    break;
                case TagType.People:
                    Peoples.Add((DmPeople)tagReference);
                    break;
                case TagType.Tag:
                    Keywords.Add((DmKeyword)tagReference);
                    break;
                case TagType.Collection:
                    Collections.Add((DmCollection)tagReference);
                    break;
            }
        }
    }
}
