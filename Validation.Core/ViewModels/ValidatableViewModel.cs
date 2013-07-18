using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.ViewModels;

namespace Validation.Core.ViewModels
{
	public class ValidatableViewModel : MvxViewModel
	{
		public ObservableDictionary<string, string> Errors { get; set; }
		private readonly Dictionary<string, Func<string>> _validators;

		protected ValidatableViewModel()
		{
			Errors = new ObservableDictionary<string, string>();
			_validators = new Dictionary<string, Func<string>>();
		}

		public bool IsValid
		{
			get { return Errors.Count == 0; }
		}

		//uncomment to automatically fire validation when a field changes, remove the ValidateProperty calls
		//public override void RaisePropertyChanged(System.ComponentModel.PropertyChangedEventArgs changedArgs)
		//{
		//	base.RaisePropertyChanged(changedArgs);
		//	if (string.IsNullOrEmpty(changedArgs.PropertyName))
		//		Validate();
		//	else
		//		ValidateProperty(changedArgs.PropertyName);
		//}

		protected void SetValidator<T>(Expression<Func<T>> propertyExpression, Func<string> validator)
		{
			var propertyName = this.GetPropertyNameFromExpression(propertyExpression);
			_validators[propertyName] = validator;
		}

		protected bool Validate()
		{
			foreach (var validator in _validators)
			{
				ValidateProperty(validator.Key, validator.Value);
			}
			return IsValid;
		}

		private bool ValidateProperty(string propertyName, Func<string> validator)
		{
			var errorMessage = validator();
			var valid = string.IsNullOrEmpty(errorMessage);
			if (valid)
				Errors.Remove(propertyName);
			else
				Errors[propertyName] = errorMessage;
			RaisePropertyChanged(() => IsValid);
			return valid;
		}

		protected bool ValidateProperty(string propertyName)
		{
			Func<string> validator;
			if (_validators.TryGetValue(propertyName, out validator))
				return ValidateProperty(propertyName, validator);

			return true;
		}

		protected bool ValidateProperty<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = this.GetPropertyNameFromExpression(propertyExpression);
			return ValidateProperty(propertyName);
		}

		protected void RaisePropertyChangedAndValidateProperty(string propertyName)
		{
			RaisePropertyChanged(propertyName);
			ValidateProperty(propertyName);
		}

		protected void RaisePropertyChangedAndValidateProperty<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = this.GetPropertyNameFromExpression(propertyExpression);
			RaisePropertyChangedAndValidateProperty(propertyName);
		}
	}
}
