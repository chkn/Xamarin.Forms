using System;

using Xamarin.Forms.Xaml;

namespace Xamarin.Forms
{
	public class PageSegueTarget : SegueTarget
	{
		readonly Page target;
		readonly DataTemplate template;

		public virtual bool IsTemplate => template != null;

		public PageSegueTarget(Page target)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));
			this.target = target;
		}

		public PageSegueTarget(DataTemplate template)
		{
			if (template == null)
				throw new ArgumentNullException(nameof(template));
			this.template = template;
		}

		protected PageSegueTarget()
		{
		}

		internal Page ToPage(out SegueTarget instantiatedTarget)
		{
			var page = ToPage();
			instantiatedTarget = IsTemplate ? new PageSegueTarget(page) : this;
			return page;
		}

		/// <summary>
		/// Gets or creates the <see cref="Page"/> for this <see cref="SegueTarget"/>
		/// </summary>
		/// <remarks>
		/// If this instance was created directly from a <see cref="Page"/>, returns that instance.
		///  Otherwise, attempts to instantiate a new page from the <see cref="DataTemplate"/>.
		///  If <see cref="IsTemplate"/> returns <c>false</c>, this method must return the same
		///  instance every time it is called.
		/// </remarks>
		public virtual Page ToPage()
		{
			if (target != null)
				return target;

			object obj = null;
			if (template != null)
			{
				obj = template.CreateContent();
				if (obj is Page page)
					return page;
			}

			// This could be a native object that we can convert to a page..
			return obj?.ConvertTo(typeof(Page), null, null) as Page;
		}
	}
}
