using System;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class FontsController:ListController
    {
        public string SelectedFontName{ get; set; }

        public FontsController ()
        {
            this.Formatter = FormatFont;
        }

        private string FormatFont (object obj)
        {
            var font = obj as UIFont;
            return font.Name;
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
//			this.View.ShowProgressHudAsync (LoadFonts, true, "Loading Fonts...", PostLoadFont);
        }

        private void PostLoadFont (List<UIFont> fonts)
        {
            this.DataSource = fonts;
            SelectFont (SelectedFontName);
            TableView.ReloadData ();
        }

        private List<UIFont> LoadFonts ()
        {
            var fonts = new List<UIFont> ();
            fonts.Add (UIFont.SystemFontOfSize (UIFont.SystemFontSize));

            foreach (var familyName in UIFont.FamilyNames)
            {
                foreach (var name in UIFont.FontNamesForFamilyName(familyName))
                {
                    var font = UIFont.FromName (name, UIFont.SystemFontSize);

                    if (!font.IsBold () && !font.IsItalic ())
                        fonts.Add (font);
                }
            }

            return fonts;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var item = DataSource [indexPath.Row];

            var cell = tableView.DequeueReusableCell (reuseId);
            if (null == cell)
                cell = new UITableViewCell (UITableViewCellStyle.Default, reuseId);
            cell.TextLabel.Text = GetItemDescription (item);
            if (indexPath.Row == SelectedIndex)
                cell.Accessory = UITableViewCellAccessory.Checkmark;
            else
                cell.Accessory = UITableViewCellAccessory.None;

            cell.TextLabel.Font = item as UIFont;
            return cell;
        }

        public void SelectFont (string fontName)
        {
            var fonts = this.DataSource as List<UIFont>;
            this.SelectedIndex = fonts.Select (f => f.Name).ToList ().IndexOf (fontName);
        }
    }
}

