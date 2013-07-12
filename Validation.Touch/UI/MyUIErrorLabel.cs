using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Validation.Touch.UI
{
	[Register("MyUIErrorLabel")]
	public class MyUIErrorLabel : UILabel 
	{
		public MyUIErrorLabel(RectangleF frame)
			: base(frame)
		{
			TextColor = UIColor.Red;
			Font = UIFont.BoldSystemFontOfSize(15);
		}

		public string ErrorText
		{
			get { return Text; }
			set
			{
				Text = value;
				Hidden = string.IsNullOrEmpty(value);
			}
		}
	}
}
