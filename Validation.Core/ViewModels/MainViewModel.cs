using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace Validation.Core.ViewModels
{
	public class MainViewModel : ValidatableViewModel
	{
		public MainViewModel()
		{
			SetValidator(() => SomeMoney, () =>
				                              {
					                              if (SomeMoney == null)
						                              return "This field is required";
					                              if (SomeMoney > 10)
						                              return "Must be less than 10";
					                              return string.Empty;
				                              });

			SetValidator(() => ForWhat, () =>
				                            {
					                            if (string.IsNullOrEmpty(ForWhat))
						                            return "This field is required";
					                            return string.Empty;
				                            });

			SetValidator(() => MoreMoney, () =>
				                              {
					                              if (string.IsNullOrEmpty(MoreMoney))
						                              return "This field is required";

					                              decimal value;
					                              if (!Decimal.TryParse(MoreMoney, out value))
						                              return "Invalid currency value";

					                              if (value > 10)
						                              return "Must be less than 10";
					                              return string.Empty;
				                              });
		}

		private decimal? _someMoney;
		public decimal? SomeMoney
		{
			get { return _someMoney; }
			set
			{
				_someMoney = value;
				RaisePropertyChanged(() => SomeMoney);
				ValidateProperty(() => SomeMoney);
			}
		}

		private string _forWhat;
		public string ForWhat
		{
			get { return _forWhat; }
			set
			{
				_forWhat = value; 
				RaisePropertyChanged(() => ForWhat);
				ValidateProperty(() => ForWhat);
			}
		}

		private string _moreMoney;
		public string MoreMoney
		{
			get { return _moreMoney; }
			set
			{
				_moreMoney = value; 
				RaisePropertyChanged(() => MoreMoney);
				ValidateProperty(() => MoreMoney);
			}
		}


		public ICommand GoCommand
		{
			get
			{
				//return new MvxCommand(() =>
				//						  {
				//							  if (this.Validate())
				//								  MvxTrace.Trace("Valid!");
				//						  },
				//					  () => IsValid);
				return new MvxCommand(() =>
				{
					if (this.Validate())
						MvxTrace.Trace("Valid!");
				});
			}
		}
	}

}
