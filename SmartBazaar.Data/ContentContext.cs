namespace SmartBazaar.Data
{
    using SmartBazaar.Data.Entities;
    using System.Data.Entity;

    public partial class ContentContext : DbContext
    {
        public ContentContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Catalog_Brands> Catalog_Brands { get; set; }
        public virtual DbSet<Catalog_Brands_Lang> Catalog_Brands_Lang { get; set; }
        public virtual DbSet<Catalog_Campaigns> Catalog_Campaigns { get; set; }
        public virtual DbSet<Catalog_Campaigns_Destinations> Catalog_Campaigns_Destinations { get; set; }
        public virtual DbSet<Catalog_Campaigns_Sources> Catalog_Campaigns_Sources { get; set; }
        public virtual DbSet<Catalog_Categories> Catalog_Categories { get; set; }
        public virtual DbSet<Catalog_Categories_Lang> Catalog_Categories_Lang { get; set; }
        public virtual DbSet<Catalog_Product_Comments> Catalog_Product_Comments { get; set; }
        public virtual DbSet<Catalog_Product_Images> Catalog_Product_Images { get; set; }
        public virtual DbSet<Catalog_Products> Catalog_Products { get; set; }
        public virtual DbSet<Catalog_Products_Lang> Catalog_Products_Lang { get; set; }
        public virtual DbSet<Catalog_Products_Properties> Catalog_Products_Properties { get; set; }
        public virtual DbSet<Catalog_Products_Properties_Lang> Catalog_Products_Properties_Lang { get; set; }
        public virtual DbSet<Catalog_Products_Relations> Catalog_Products_Relations { get; set; }
        public virtual DbSet<Customer_Addresses> Customer_Addresses { get; set; }
        public virtual DbSet<Customer_Entities> Customer_Entities { get; set; }
        public virtual DbSet<Customer_Groups> Customer_Groups { get; set; }
        public virtual DbSet<HtmlBlocks> HtmlBlocks { get; set; }
        public virtual DbSet<HtmlBlocks_Lang> HtmlBlocks_Lang { get; set; }
        public virtual DbSet<Lang_Books> Lang_Books { get; set; }
        public virtual DbSet<Lang_Dictionary> Lang_Dictionary { get; set; }
        public virtual DbSet<Order_Heads> Order_Heads { get; set; }
        public virtual DbSet<Order_Lines> Order_Lines { get; set; }
        public virtual DbSet<Pages> Pages { get; set; }
        public virtual DbSet<Pages_Lang> Pages_Lang { get; set; }
        public virtual DbSet<Params> Params { get; set; }
        public virtual DbSet<Params_Groups> Params_Groups { get; set; }
        public virtual DbSet<Payment_Entities> Payment_Entities { get; set; }
        public virtual DbSet<Payment_Installment> Payment_Installment { get; set; }
        public virtual DbSet<Payment_Types> Payment_Types { get; set; }
        public virtual DbSet<Payment_Types_Lang> Payment_Types_Lang { get; set; }
        public virtual DbSet<Pos_Settings> Pos_Settings { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Shipment_Types> Shipment_Types { get; set; }
        public virtual DbSet<Shipment_Types_Lang> Shipment_Types_Lang { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<Tax_Products> Tax_Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog_Brands>()
                .Property(e => e.ExternalRef1)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Brands>()
                .Property(e => e.ExternalRef2)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Brands>()
                .Property(e => e.ExternalRef3)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Brands>()
                .HasMany(e => e.Catalog_Brands_Lang)
                .WithRequired(e => e.Catalog_Brands)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Brands>()
                .HasMany(e => e.Catalog_Products)
                .WithOptional(e => e.Catalog_Brands)
                .HasForeignKey(e => e.BrandId);

            modelBuilder.Entity<Catalog_Brands_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Campaigns>()
                .Property(e => e.SliderUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Campaigns>()
                .HasMany(e => e.Catalog_Campaigns_Destinations)
                .WithRequired(e => e.Catalog_Campaigns)
                .HasForeignKey(e => e.CampaignId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Campaigns>()
                .HasMany(e => e.Order_Lines)
                .WithOptional(e => e.Catalog_Campaigns)
                .HasForeignKey(e => e.CampaignId);

            modelBuilder.Entity<Catalog_Campaigns>()
                .HasMany(e => e.Catalog_Campaigns_Sources)
                .WithRequired(e => e.Catalog_Campaigns)
                .HasForeignKey(e => e.CampaignId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Categories>()
                .Property(e => e.ExternalRef1)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Categories>()
                .Property(e => e.ExternalRef2)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Categories>()
                .Property(e => e.ExternalRef3)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Categories>()
                .HasMany(e => e.Catalog_Categories_Lang)
                .WithRequired(e => e.Catalog_Categories)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Categories>()
                .HasMany(e => e.Catalog_Products)
                .WithRequired(e => e.Catalog_Categories)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Categories_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Product_Images>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.Price1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.Price2)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.Price3)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.Price4)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.Price5)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.SKU)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.MPN)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products>()
                .Property(e => e.MetaUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Campaigns_Destinations)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Campaigns_Sources)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Product_Comments)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Product_Images)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Products_Lang)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Products_Relations)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Order_Lines)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products>()
                .HasMany(e => e.Catalog_Products_Properties)
                .WithRequired(e => e.Catalog_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products_Lang>()
                .Property(e => e.MetaUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog_Products_Properties>()
                .HasMany(e => e.Catalog_Products_Properties_Lang)
                .WithRequired(e => e.Catalog_Products_Properties)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog_Products_Properties_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Addresses>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Addresses>()
                .HasMany(e => e.Order_Heads)
                .WithOptional(e => e.Customer_Addresses)
                .HasForeignKey(e => e.InvoiceAddressId);

            modelBuilder.Entity<Customer_Addresses>()
                .HasMany(e => e.Order_Heads1)
                .WithOptional(e => e.Customer_Addresses1)
                .HasForeignKey(e => e.ShipAddressId);

            modelBuilder.Entity<Customer_Entities>()
                .Property(e => e.TaxNr)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Entities>()
                .Property(e => e.ContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_Entities>()
                .HasMany(e => e.Customer_Addresses)
                .WithRequired(e => e.Customer_Entities)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer_Entities>()
                .HasMany(e => e.Order_Heads)
                .WithRequired(e => e.Customer_Entities)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer_Groups>()
                .HasMany(e => e.Customer_Entities)
                .WithOptional(e => e.Customer_Groups)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<HtmlBlocks>()
                .HasMany(e => e.HtmlBlocks_Lang)
                .WithRequired(e => e.HtmlBlocks)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HtmlBlocks_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lang_Books>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Lang_Books>()
                .HasMany(e => e.Lang_Dictionary)
                .WithRequired(e => e.Lang_Books)
                .HasForeignKey(e => e.BookId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_Heads>()
                .Property(e => e.OrderTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Heads>()
                .Property(e => e.TaxTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Heads>()
                .Property(e => e.ShipCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Heads>()
                .Property(e => e.GrandTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Heads>()
                .Property(e => e.PaymentFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Heads>()
                .Property(e => e.InstallmentFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Heads>()
                .HasMany(e => e.Order_Lines)
                .WithRequired(e => e.Order_Heads)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_Heads>()
                .HasMany(e => e.Payment_Entities)
                .WithRequired(e => e.Order_Heads)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_Lines>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Lines>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pages>()
                .HasMany(e => e.Pages_Lang)
                .WithRequired(e => e.Pages)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pages_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Params_Groups>()
                .HasMany(e => e.Params)
                .WithRequired(e => e.Params_Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment_Entities>()
                .Property(e => e.OrderPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Entities>()
                .Property(e => e.PaymentFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Entities>()
                .Property(e => e.InstallmentFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Entities>()
                .Property(e => e.ShipmentFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Entities>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Installment>()
                .HasMany(e => e.Payment_Entities)
                .WithOptional(e => e.Payment_Installment)
                .HasForeignKey(e => e.InstallmentId);

            modelBuilder.Entity<Payment_Types>()
                .Property(e => e.ProcessFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Payment_Types>()
                .Property(e => e.PosType)
                .IsUnicode(false);

            modelBuilder.Entity<Payment_Types>()
                .HasMany(e => e.Payment_Entities)
                .WithRequired(e => e.Payment_Types)
                .HasForeignKey(e => e.PaymentTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment_Types>()
                .HasMany(e => e.Payment_Installment)
                .WithRequired(e => e.Payment_Types)
                .HasForeignKey(e => e.PaymentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment_Types>()
                .HasMany(e => e.Payment_Types_Lang)
                .WithRequired(e => e.Payment_Types)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment_Types_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Settings>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<Shipment_Types>()
                .HasMany(e => e.Order_Heads)
                .WithRequired(e => e.Shipment_Types)
                .HasForeignKey(e => e.ShipmentTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment_Types>()
                .HasMany(e => e.Shipment_Types_Lang)
                .WithRequired(e => e.Shipment_Types)
                .HasForeignKey(e => e.RefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment_Types_Lang>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Tax_Products>()
                .HasMany(e => e.Catalog_Products)
                .WithRequired(e => e.Tax_Products)
                .HasForeignKey(e => e.TaxGroup)
                .WillCascadeOnDelete(false);
        }
    }
}
