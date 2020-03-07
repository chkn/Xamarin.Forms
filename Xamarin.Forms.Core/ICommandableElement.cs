using System.Windows.Input;
using System.ComponentModel;

namespace Xamarin.Forms
{
	// implementing classes must be castable to Element
	[EditorBrowsable(EditorBrowsableState.Never)]
	interface ICommandableElement
	{
		ICommand Command { get; set; }
		object CommandParameter { get; set; }
	}
}
