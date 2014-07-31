namespace Cession.Assignments
{
	using System;

	using Cession.Products;
	using Cession.Modeling;

	public abstract class Assignment
	{
		public Product Product{ get; private set; }
		public Diagram Diagram{ get; private set; }

		protected Assignment (Product product,Diagram diagram)
		{
			if (null == product)
				throw new ArgumentNullException ();

			if (null == diagram)
				throw new ArgumentNullException ();

			this.Product = product;
			this.Diagram = diagram;
		}
	}
}

