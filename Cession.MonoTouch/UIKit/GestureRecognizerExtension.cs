﻿using System;

using UIKit;

namespace Cession.UIKit
{
    public static class GestureRecognizerExtension
    {
        public static bool IsDone (this UIGestureRecognizer gestureRecognizer)
        {
            return gestureRecognizer.State == UIGestureRecognizerState.Ended ||
                gestureRecognizer.State == UIGestureRecognizerState.Cancelled;
        }

        public static bool IsBegan (this UIGestureRecognizer gestureRecognizer)
        {
            return gestureRecognizer.State == UIGestureRecognizerState.Began;
        }
    }
}

