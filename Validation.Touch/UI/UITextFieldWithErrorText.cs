using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Validation.Touch.UI
{
	//This class is for providing feedback when binding normally
	[Register("UITextFieldWithErrorText")]
	public class UITextFieldWithErrorText : UITextField
	{
		public UITextFieldWithErrorText(RectangleF frame)
			: base(frame)
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
