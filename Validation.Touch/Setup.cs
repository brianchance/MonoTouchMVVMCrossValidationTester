using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Binding.Touch;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using Validation.Touch.UI;

namespace Validation.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories(registry);
			registry.RegisterPropertyInfoBindingFactory(typeof(MyUITextFieldTextDidEndTargetBinding), typeof(MyUITextField), "Text");
		}

		protected override void FillBindingNames(IMvxBindingNameRegistry registry)
		{
			base.FillBindingNames(registry);
			registry.AddOrOverwrite<MyUITextField>(f => f.Text);
			registry.AddOrOverwrite<MyUIErrorLabel>(f => f.ErrorText);
		}
	}

}
