using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Cirrious.CrossCore.Platform;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Validation.Touch.UI
{
	[Register("MyUIMoneyTextField")]
	public class MyUIMoneyTextField : MyUITextField
	{
		public MyUIMoneyTextField(RectangleF frame):base(frame)
		{
			Delegate = new MyUIMoneyTextFieldDelegate();
		}
	}

	public class MyUIMoneyTextFieldDelegate : UITextFieldDelegate
	{
		private bool IsOk(string text)
		{
			//var regex = new Regex("^([0-9]+)?([\\.\\,]([0-9]{1,2})?)?$");
			//var ok = regex.IsMatch(newText);

			decimal d;
			var ok = Decimal.TryParse(text, out d);
			return ok;
		}

		//public override bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
		//{
		//	if (string.IsNullOrEmpty(replacementString))
		//		return true;

		//	var newText = (textField.Text ?? "").Remove(range.Location, range.Length)
		//					 .Insert(range.Location, replacementString);
		//	//MvxTrace.Trace(
		//	//	"ShouldChangeCharacters, before: {0}, replace: {1}, newtext: {2}",
		//	//	this.Text, replacementString, newText);

		//	return IsOk(newText);
		//}

		public override bool ShouldEndEditing(UITextField textField)
		{
			var ok = IsOk(textField.Text);
			if (!ok && textField is MyUIMoneyTextField)
				(textField as MyUIMoneyTextField).ErrorText = "Invalid currency value";
			return ok;
		}
	}
}