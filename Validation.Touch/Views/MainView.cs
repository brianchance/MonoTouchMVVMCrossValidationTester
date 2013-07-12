using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Validation.Core.ViewModels;
using Validation.Touch.UI;

namespace Validation.Touch.Views
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

			var someMoneyField = new MyUIMoneyTextField(new RectangleF(10, 10, 300, 30))
				                 {
					                 Placeholder = "How much?",
					                 BorderStyle = UITextBorderStyle.RoundedRect,
					                 ReturnKeyType = UIReturnKeyType.Next
				                 };
			View.AddSubview(someMoneyField);

			var someMoneyErrorLabel = new MyUIErrorLabel(new RectangleF(10, 40, 300, 30));
			View.AddSubview(someMoneyErrorLabel);

			var forWhatfield = new MyUITextField(new RectangleF(10, 80, 300, 30))
				                   {
					                   Placeholder = "For What?",
					                   BorderStyle = UITextBorderStyle.RoundedRect,
					                   ReturnKeyType = UIReturnKeyType.Next
				                   };
			View.AddSubview(forWhatfield);

			var forWhatErrorLabel = new MyUIErrorLabel(new RectangleF(10, 110, 300, 30));
			View.AddSubview(forWhatErrorLabel);

			var moreMoneyField = new UITextFieldWithErrorText(new RectangleF(10, 140, 300, 30))
			{
				Placeholder = "How much more?",
				BorderStyle = UITextBorderStyle.RoundedRect,
				ReturnKeyType = UIReturnKeyType.Next
			};
			View.AddSubview(moreMoneyField);

			var moreMoneyErrorLabel = new MyUIErrorLabel(new RectangleF(10, 170, 300, 30));
			View.AddSubview(moreMoneyErrorLabel);

			var button = UIButton.FromType(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(10, 200, 300, 30);
			button.SetTitle("Go get it", UIControlState.Normal);
			View.AddSubview(button);

			var set = this.CreateBindingSet<MainView, MainViewModel>();
			set.Bind(someMoneyField).To(vm => vm.SomeMoney);
			set.Bind(someMoneyField).For(f => f.ErrorText).To(vm => vm.Errors["SomeMoney"]);
			set.Bind(someMoneyErrorLabel).To(vm => vm.Errors["SomeMoney"]);

			set.Bind(forWhatfield).To(vm => vm.ForWhat);
			set.Bind(forWhatfield).For(f => f.ErrorText).To(vm => vm.Errors["ForWhat"]);
			set.Bind(forWhatErrorLabel).To(vm => vm.Errors["ForWhat"]);

			set.Bind(moreMoneyField).To(vm => vm.MoreMoney);
			set.Bind(moreMoneyField).For(f => f.ErrorText).To(vm => vm.Errors["MoreMoney"]);
			set.Bind(moreMoneyErrorLabel).To(vm => vm.Errors["MoreMoney"]);

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