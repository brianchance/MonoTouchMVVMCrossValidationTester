using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Validation.Core.ViewModels;
using Validation.Touch.UI;

namespace Validation.Touch
{
	[Register("MainView")]
	public class MainView : MvxViewController
	{
		public MainView()
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		public new MainViewModel ViewModel
		{
			get { return (MainViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.White;

			var moneyField = new MyUIMoneyTextField(new RectangleF(10, 10, 300, 30))
				                 {
					                 Placeholder = "How much?",
					                 BorderStyle = UITextBorderStyle.RoundedRect,
					                 ReturnKeyType = UIReturnKeyType.Next
				                 };
			View.AddSubview(moneyField);

			var moneyErrorLabel = new MyUIErrorLabel(new RectangleF(10, 40, 300, 30));
			View.AddSubview(moneyErrorLabel);

			var forWhatfield = new MyUITextField(new RectangleF(10, 80, 300, 30))
				                   {
					                   Placeholder = "For What?",
					                   BorderStyle = UITextBorderStyle.RoundedRect,
					                   ReturnKeyType = UIReturnKeyType.Next
				                   };
			View.AddSubview(forWhatfield);

			var forWhatErrorLabel = new MyUIErrorLabel(new RectangleF(10, 110, 300, 30));
			View.AddSubview(forWhatErrorLabel);

			var button = UIButton.FromType(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(10, 150, 300, 30);
			button.SetTitle("Go get it", UIControlState.Normal);
			View.AddSubview(button);

			var set = this.CreateBindingSet<MainView, MainViewModel>();
			set.Bind(moneyField).To(vm => vm.SomeMoney);
			set.Bind(moneyField).For(f => f.ErrorText).To(vm => vm.Errors["SomeMoney"]);
			set.Bind(moneyErrorLabel).To(vm => vm.Errors["SomeMoney"]);

			set.Bind(forWhatfield).To(vm => vm.ForWhat);
			set.Bind(forWhatfield).For(f => f.ErrorText).To(vm => vm.Errors["ForWhat"]);
			set.Bind(forWhatErrorLabel).To(vm => vm.Errors["ForWhat"]);

			//set.Bind(button).To(vm => vm.GoCommand);
			set.Apply();

			button.TouchUpInside += (sender, args) =>
			{
				if (View.EndEditing(true))
					ViewModel.GoCommand.Execute(null);
			};

			var tap = new UITapGestureRecognizer(() =>
				                                     {
					                                     View.EndEditing(true);
				                                     });
			//tap.CancelsTouchesInView = false;
			View.AddGestureRecognizer(tap);
		}
	}
}