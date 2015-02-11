using System;
using System.Linq;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class TextFieldItem:DetailItem
    {
        static readonly NSString cellId = new NSString ("textFieldId");

        public override NSString CellId
        {
            get { return cellId; }
        }

        public UIKeyboardType KeyboardType{ get; set; }

        public UITextAlignment TextAlignment{ get; set; }

        public string TextFieldPlaceHolder{ get; set; }

        public bool IsShowTitle{ get; set; }

        public UIFont TitleFont{ get; set; }

        public Func<string,bool> Validator{ get; set; }

        private TextFieldDelegate textDelegate;
        private bool? validateState = null;

        public TextFieldItem ()
        {
            KeyboardType = UIKeyboardType.Default;
            TextAlignment = UITextAlignment.Right;
            NeedReloadSelf = false;
            TitleFont = DetailItem.CaptionFont;
            IsShowTitle = true;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = new TextFieldCell (CellId);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }

        protected override void DoActive (NSIndexPath indexPath, UITableViewCell cell, object data)
        {
            var textCell = cell as TextFieldCell;
            textCell.Accessory = UITableViewCellAccessory.None;
            textCell.IsShowTitle = IsShowTitle;
            textCell.TextLabel.Text = Title;

            if (null != TitleFont)
                textCell.TextLabel.Font = TitleFont;

            textCell.TextField.Text = GetValueDescription (data);
            textCell.TextField.KeyboardType = KeyboardType;
            if (string.IsNullOrEmpty (TextFieldPlaceHolder))
                textCell.TextField.Placeholder = Title;
            else
                textCell.TextField.Placeholder = TextFieldPlaceHolder;

            textCell.TextField.TextAlignment = TextAlignment;
            if (null == textDelegate)
                textDelegate = new TextFieldDelegate (this);
            textCell.TextField.Delegate = textDelegate;
            if (validateState.HasValue)
            {
                TextFieldDelegate.SetTextFieldRightView (textCell.TextField, validateState.Value);
            } else
            {
                textCell.TextField.RightView = null;
                textCell.TextField.RightViewMode = UITextFieldViewMode.Never;
            }
        }

        class TextFieldDelegate:UITextFieldDelegate
        {
            static UIImageView checkImageView;
            static UIImageView warningImageView;

            private TextFieldItem item;

            public TextFieldDelegate (TextFieldItem item)
            {
                this.item = item;
            }

            internal static UIImageView GetCheckImageView ()
            {
                if (null == checkImageView)
                {
                    checkImageView = new UIImageView (UIImage.FromBundle (""));
                }

                return checkImageView;
            }

            internal static UIImageView GetWarningImageView ()
            {
                if (null == warningImageView)
                {
                    warningImageView = new UIImageView (UIImage.FromBundle (""));
                }
                return warningImageView;
            }


            public override bool ShouldReturn (UITextField textField)
            {
                textField.ResignFirstResponder ();
                return true;
            }

            public override void EditingEnded (UITextField textField)
            {
                item.SetValue (item.detailController.DataContext, textField.Text);
                textField.Text = item.GetValueDescription (item.detailController.DataContext);			
            }

            internal static void SetTextFieldRightView (UITextField textField, bool result)
            {
                textField.RightViewMode = UITextFieldViewMode.UnlessEditing;

                if (result)
                    textField.RightView = GetCheckImageView ();
                else
                    textField.RightView = GetWarningImageView ();
            }

            public override bool ShouldEndEditing (UITextField textField)
            {
                if (item.Validator != null)
                {
                    var result = item.Validator (textField.Text);
                    SetTextFieldRightView (textField, result);
                    item.validateState = result;
                }
                return true;
            }


            public override bool ShouldClear (UITextField textField)
            {
                textField.RightView = null;
                return true;
            }

            public override bool ShouldChangeCharacters (UITextField textField, NSRange range, string replacementString)
            {				
                if (range.Length == 1 && string.IsNullOrEmpty (replacementString))
                {
                    textField.RightView = null;
                }
                return true;
            }
        }
    }
}

