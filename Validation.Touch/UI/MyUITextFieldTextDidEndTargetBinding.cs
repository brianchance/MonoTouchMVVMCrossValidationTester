using System.Reflection;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Bindings.Target;

namespace Validation.Touch.UI
{
    public class MyUITextFieldTextDidEndTargetBinding : MvxPropertyInfoTargetBinding<MyUITextField>
    {
		public MyUITextFieldTextDidEndTargetBinding(object target, PropertyInfo targetPropertyInfo)
            : base(target, targetPropertyInfo)
        {
            var editText = View;
            if (editText == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Error,
									  "Error - UITextField is null in MyUITextFieldTextDidEndTargetBinding");
            }
            else
            {
	            editText.EditingDidEnd += HandleEditTextValueChanged;
            }
        }

        private void HandleEditTextValueChanged(object sender, System.EventArgs e)
        {
            var view = View;
            if (view == null)
                return;
            FireValueChanged(view.Text);
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.TwoWay; }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
            {
                var editText = View;
                if (editText != null)
                {
					editText.EditingDidEnd -= HandleEditTextValueChanged;
                }
            }
        }
    }
}