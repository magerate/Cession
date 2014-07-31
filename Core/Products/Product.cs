namespace Cession.Products
{
	public abstract class Product
	{
		public string Id{ get; set; }
		public string Name{ get; set; }
		public double SalesPrice{ get; set; }
		public double CostPrice{ get; set; }

		public ProductCategory Category{ get; private set; }

		protected Product(ProductCategory category)
		{
			this.Category = category;
		}
	}
}

