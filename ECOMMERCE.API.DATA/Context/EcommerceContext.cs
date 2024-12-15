using ECOMMERCE.API.DATA.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.DATA.Context
{
    public partial class EcommerceContext(DbContextOptions<EcommerceContext> options) : DbContext(options)
    {
        #region [ TABLE ]

        public virtual DbSet<TB_USUARIOS> TB_USUARIOS { get; set; }
        public virtual DbSet<TB_CATEGORIAS> TB_CATEGORIAS { get; set; }
        public virtual DbSet<TB_CLIENTES> TB_CLIENTES { get; set; }
        public virtual DbSet<TB_PRODUTOS> TB_PRODUTOS { get; set; }
        public virtual DbSet<TB_STATUS_PEDIDOS> TB_STATUS_PEDIDOS { get; set; }
        public virtual DbSet<TB_PEDIDOS> TB_PEDIDOS { get; set; }
        public virtual DbSet<TB_ITENS_PEDIDO> TB_ITENS_PEDIDO { get; set; }
        public virtual DbSet<TB_FATURAMENTOS> TB_FATURAMENTOS { get; set; }

        #endregion [ TABLE ]

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_USUARIOS>(entity =>
            {
                entity.HasKey(e => e.USUARIO_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.USUARIO_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TB_PRODUTOS>(entity =>
            {
                entity.HasKey(e => e.PRODUTO_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.PRODUTO_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TB_PEDIDOS>(entity =>
            {
                entity.HasKey(e => e.PEDIDO_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.PEDIDO_ID).ValueGeneratedOnAdd();
            });
            
            modelBuilder.Entity<TB_ITENS_PEDIDO>(entity =>
            {
                entity.HasKey(e => e.ITEM_PEDIDO_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.ITEM_PEDIDO_ID).ValueGeneratedOnAdd();
            }); 
            
            modelBuilder.Entity<TB_FATURAMENTOS>(entity =>
            {
                entity.HasKey(e => e.FATURAMENTO_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.FATURAMENTO_ID).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
