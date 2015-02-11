using System;

using UIKit;
using Foundation;

namespace Cession.UIKit
{
    public class SwitchCell:UITableViewCell
    {
        public UISwitch Switch { get; private set; }

        public Action<bool> SwitchValueChangedAction{ get; set; }


        public SwitchCell (NSString reuseId) : base (UITableViewCellStyle.Default, reuseId)
        {
            Switch = new UISwitch ();
            Switch.ValueChanged += (sender, e) =>
            {
                if (null != SwitchValueChangedAction)
                    SwitchValueChangedAction (Switch.On);
            };

            AccessoryView = Switch;
        }
    }
}

