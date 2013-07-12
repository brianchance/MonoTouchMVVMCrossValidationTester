using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Validation.Core.ViewModels;

namespace Validation.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
		}
	}
}
