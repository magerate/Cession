namespace Cession.UIKit
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;


	public abstract class DetailItem
	{
		public static readonly UIFont CaptionFont = UIFont.SystemFontOfSize (18);
		public static readonly UIFont BoldCaptionFont = UIFont.FromName (".HelveticaNeueInterface-MediumP4",18);

		public abstract NSString CellId{ get; }

		public string Title{get; set;}

		public string Property{ get; set; }

		public Func<object,object> Getter{get;set;}
		public ValueConverter Converter{ get; set; }
		public Func<object,string> Formatter{get;set;}
		public Action<object,object> Setter{get;set;}

		public float? Height{ get; set; }

		public Action<DetailItem> DidChangeAction{ get; set; }
		public Action<DetailItem> WillChangeAction{ get; set; }

		public object Tag{get;set;}

		public List<DetailItem> AffectedItems{ get; private set; }

		protected bool NeedReloadSelf{ get; set; }

		public bool ReloadRowsWhenValueChanged{ get; set; }


		protected DetailViewController detailController;
		protected NSIndexPath indexPath;

		public NSIndexPath IndexPath
		{
			get{ return indexPath; }
		}

		protected DetailItem()
		{
			AffectedItems = new List<DetailItem> ();

			NeedReloadSelf = true;
			ReloadRowsWhenValueChanged = true;
		}

		public abstract UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath);

		public void Active (NSIndexPath indexPath, UITableViewCell cell, DetailViewController detailViewController)
		{

			this.indexPath = indexPath;
			this.detailController = detailViewController;
			this.DoActive (indexPath, cell, detailViewController.DataContext);
		}

		protected abstract void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data);

		public virtual void WillDisplay(NSIndexPath indexPath, UITableViewCell cell, object data)
		{
		}

		public object GetValue(object target)
		{
			object value = null;

			if (Property != null && target != null) {
				var type = target.GetType ();
				var pi = type.GetProperty (Property);
				if (null != pi)
					value = pi.GetValue (target);
				else
					throw new InvalidOperationException ("Property doesn't exists!");
			} else if (null != Getter) {
				try {
					value = Getter.Invoke(target);
				} catch (Exception) {
					return null;
				}
			}

			if (null != Converter)
				return Converter.Convert (value);
			return value;
		}

		public string GetValueDescription(object target)
		{
			var value = GetValue (target);

			return DoGetValueDescription (value);
		}


		private string DoGetValueDescription(object value)
		{
			if(null == value)
				return string.Empty;

			if(Formatter == null)
				return value.ToString();

			return Formatter.Invoke(value);
		}

		public void SetValue(object target,object value)
		{
			if (GetValue (target) == value)
				return;

			if (null != Converter) {
				var convertedValue = Converter.ConvertBack (value);
				if (convertedValue.Item1)
					value = convertedValue.Item2;
				else
					return;
			}

			if (Property != null && target != null) {
				var type = target.GetType ();
				var pi = type.GetProperty (Property);
				if (null != pi) {
					try {
						ChangeValue((t,v) => pi.SetValue(t,v),target,value);

					} catch (Exception) {
						
					}
					return;
				}
				else
					throw new InvalidOperationException ("Property doesn't exists!");
			}

			if(null != Setter)
			{
				ChangeValue (Setter, target, value);
			}
		}

		private void ChangeValue(Action<object,object> action,object target,object value)
		{
			if (null != WillChangeAction)
				WillChangeAction.Invoke (this);

			if(null != detailController.ValueWillChangeAction)
				detailController.ValueWillChangeAction.Invoke(this);

			action.Invoke (target,value);

			if(null != detailController.ValueDidChangeAction)
				detailController.ValueDidChangeAction.Invoke(this);

			if (null != DidChangeAction)
				DidChangeAction.Invoke (this);

			ReloadRows ();
		}

		public virtual void Select(DetailViewController controller,NSIndexPath indexPath)
		{
		}

		public virtual void AccessoryButtonTapped(DetailViewController controller,NSIndexPath indexPath)
		{

		}

		private NSIndexPath[] GetRowsToReload()
		{
			var indices = new List<NSIndexPath> ();
			if(NeedReloadSelf)
				indices.Add (indexPath);

			indices.AddRange (AffectedItems.Select(i => i.indexPath));
			return indices.ToArray ();
		}

		private void ReloadRows()
		{
			if (!ReloadRowsWhenValueChanged)
				return;

			var indexPaths = GetRowsToReload ();
			if(indexPath.Length > 0)
				detailController.TableView.ReloadRows(indexPaths, UITableViewRowAnimation.Automatic);
		}
	}
}

