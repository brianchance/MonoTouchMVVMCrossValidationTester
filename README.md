MonoTouch+MVVMCross ValidationTester
=========================

Project to explore the various options for validating models, storing errors and providing feedback with MonoTouch using MVVMCross binding.

The default binding for MVVMCross pushes changes to the view model as they change (i.e. keyup) which causes problems when binding to decimals (and free form dates I am guessing). The issue is when typing "1.", the value is converted to 1 and reformats the text in the input. If you type in "abc", the value fails to convert and is not pushed to the view model (not normally an issue, but pesky QA folks would prefer an error message, and I tend to agree it would be nice, if they put in 2/30/1013 that should be an error).

This has a few possible workarounds.
* Bind to strings instead of decimals, then do type checking as a validation.
* Switch the binding to EditingDidEnd (blur). This allows handling ShouldEndEdit to do basic type checking (and where you can provide feedback).
* You can, with both of these use, ShouldChangeCharacters to restrict what the user enters (helping with the "abc" issue, but difficult with date parsing).

In addition I wanted to have controls display visual indicators when there was an error.

###Stuff in the code:
* For validation there is a ValidatableViewModel that provides basic functionality. 
  * Inherited ViewModels provide validators, then call ValidateProperty as needed. 
  * The Validate method validates the entire view model and should probably be checked before sending data to the server.
  * As an alternative to calling ValidateProperty manually, it can happen automatically by overridding the RaisePropertyChanged event - see code.
* Errors are stored using an ObservableDictionary
  * You will need at least mvvmcross 3.0.9-beta8 which has a fix for INotifiyCollectionChanged.
  * The implementation in this project has been modified to not throw an error when accessing a non-existant key.
* For controls, there are:
  * MyUIErrorLabel - bind ErrorText to `Errors["propertyName"]` (Setup.cs shows how to make ErrorText the default for labels)
  * MyUITextField - provides ErrorText and a binding target (Setup.cs makes Text the default binding for this control)
  * MyUITextFieldTextDidEndTargetBinding - changes binding to EditingDidEnd (Setup.cs registers it)
  * MyUIMoneyTextField - shows off restricting input with ShouldChangeCharacters and validating/providing feedback using ShouldEndEditing
  * UITextFieldWithErrorText - used to just add an ErrorText to standard UITextField binding to a string provides feedback.

###Notes:
* Using EditingDidEnd instead of EditingChanged has some complications
  * Clicking a button does not fire the end edit for the control (similar to clicking a submit button in html). The MainView checks View.EndEditing before manually calling the Command. This might be some setting on UITextField I am not aware of.
  * The DidEnd does not seem as responsive, you do not get feedback as you type (especially when correcting errors).

  
###What I will probably do going forward:
* Bind to strings, the responsive feedback provides a better UX.
* Use the custom error label and text field with error text, works great to provide feedback. They could easily be customized to show a red-x or green checkmark.
* Use a custom currency field to restrict characters with ShouldChangeCharacters.

###A note about FluentValidation
I would have preferred to use FluentValidation, but could not get the PCL working with MonoTouch. I reference it, but it crashes when trying to start the debugger in the iOS simulator.
I can only guess it is calling code not available. It uses Profile36, which has WP8. Cannot wait for Xamarin to get PCL done.

