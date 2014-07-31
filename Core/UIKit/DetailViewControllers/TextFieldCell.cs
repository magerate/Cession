namespace Cession.UIKit
{
	using System;
	using System.Drawing;

	using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	public class TextFieldCell:UITableViewCell
	{
		public static readonly float TextHeight = 30;
		public float Margin{ get; set;} 

		private bool alwaysEditable = true;

		public UITextField TextField{get;private set;}
		public bool IsShowTitle{get;set;}

		public bool AlwaysEditable
		{
			get{ return alwaysEditable; }
			set{
				alwaysEditable = value;
				if (alwaysEditable)
					TextField.Enabled = true;
				else
					TextField.Enabled = false;
			}
		}

		public TextFieldCell (NSString reuseId):base(UITableViewCellStyle.Default,reuseId)
		{
			Initialize ();
		}

		public TextFieldCell(IntPtr ptr):base(ptr)
		{
			Initialize ();
		}

		private void Initialize()
		{
			TextField = new UITextField();
			TextField.BorderStyle = UITextBorderStyle.None;
			TextField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
			TextField.TextAlignment = UITextAlignment.Right;

			ContentView.AddSubview(TextField);
			SelectionStyle = UITableViewCellSelectionStyle.None;

			IsShowTitle = true;

			Margin = 6;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			RectangleF frame;
			if (IsShowTitle && !string.IsNullOrEmpty (TextLabel.Text)) {
				var titleFrame = TextLabel.Frame;
				var str = new NSString (TextLabel.Text);
				var size = str.StringSize (TextLabel.Font);
				titleFrame.Width = size.Width + Margin * 2;
				TextLabel.Frame = titleFrame;
				frame = new RectangleF (Margin + titleFrame.Right,
					(ContentView.Bounds.Height - TextHeight) / 2 + 4,
					ContentView.Bounds.Width - titleFrame.Width - 4 * Margin,
					TextHeight);
			} else {
				TextLabel.Frame = RectangleF.Empty;
				frame = new RectangleF (Margin, (ContentView.Bounds.Height - TextHeight) / 2 + 4,
					ContentView.Bounds.Width - 2 * Margin, TextHeight);
			}
			TextField.Frame = frame;
		}

		public override void WillTransitionToState (UITableViewCellState mask)
		{
			base.WillTransitionToState (mask);

			if (alwaysEditable)
				return;

			if (mask == UITableViewCellState.ShowingEditControlMask)
				TextField.Enabled = true;
			else if (mask == UITableViewCellState.DefaultMask)
				TextField.Enabled = false;
		}

		public override void PrepareForReuse ()
		{
			base.PrepareForReuse ();
			this.TextField.RightView = null;
			this.TextField.RightViewMode = UITextFieldViewMode.Never;
		}
	}
}

