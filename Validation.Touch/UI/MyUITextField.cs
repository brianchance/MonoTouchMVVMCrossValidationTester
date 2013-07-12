using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Validation.Touch.UI
{
	[Register("MyUITextField")]
	public class MyUITextField : UITextField
	{
		public MyUITextField(RectangleF frame):base(frame)
		{
		}

		public event EventHandler ErrorTextChanged;

		private string _errorText;
		public string ErrorText
		{
			get { return _errorText; }
			set
			{
				_errorText = value; 
				if (ErrorTextChanged != null) 
					ErrorTextChanged(this, new EventArgs());

				if (!string.IsNullOrEmpty(_errorText))
				{
					Layer.BorderColor = UIColor.Red.CGColor;
					Layer.BorderWidth = 3;
					Layer.CornerRadius = 5;
				}
				else
				{
					Layer.BorderColor = UIColor.Clear.CGColor;
					Layer.BorderWidth = 0;
					Layer.CornerRadius = 0;
				}
			}
		}

	}
}
