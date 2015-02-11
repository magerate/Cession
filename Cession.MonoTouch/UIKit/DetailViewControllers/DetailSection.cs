namespace Cession.UIKit
{
	using System;
	using System.Collections.Generic;

	using UIKit;

	public class DetailSection
	{
		public List<DetailItem> Items{get;private set;}
		public string HeaderTitle{get;set;}
		public string FooterTitle{get;set;}

		public DetailSection ()
		{
			Items = new List<DetailItem>();
		}
	}


}

