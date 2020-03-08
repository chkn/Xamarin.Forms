﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	public static class SegueExtensions
	{
		/// <summary>
		/// Performs the given <see cref="NavigationAction"/>.
		/// </summary>
		public static Task NavigateAsync(this INavigation nav, NavigationAction action, Page page, bool animated = true)
		{
			switch (action)
			{
				case NavigationAction.Show:
					return nav.ShowAsync(page, animated);
				case NavigationAction.Push:
					return nav.PushAsync(page, animated);
				case NavigationAction.Modal:
					return nav.PushModalAsync(page, animated);
				case NavigationAction.Pop:
					return nav.ShouldPopModal() ? nav.PopModalAsync(animated) : nav.PopAsync(animated);
				case NavigationAction.PopPushed:
					return nav.PopAsync(animated);
				case NavigationAction.PopModal:
					return nav.PopModalAsync(animated);
				case NavigationAction.PopToRoot:
					return nav.PopToRootAsync(animated);
				case NavigationAction.MainPage:
					Application.Current.MainPage = page;
					return Task.FromResult<object>(null);
			}
			throw new ArgumentException("Unknown action", nameof(action));
		}

		public static Task NavigateAsync(this INavigation nav, NavigationAction action, SegueTarget target, bool animated = true)
		{
			return SegueAsync(nav, new ValueSegue(action, animated), target);
		}

		public static Task SegueAsync(this INavigation nav, Segue segue, Page page)
		{
			return nav.SegueAsync(segue, (SegueTarget)page);
		}

		internal static Task SegueAsync(this INavigation nav, ValueSegue seg, SegueTarget target)
		{
			// If possible, call direct to save us some hand waving and allocations..
			if (nav is NavigationProxy proxy)
				return proxy.OnSegue(seg, target);

			// If it's a shell navigation, dispatch directly to that..
			if (target is UriSegueTarget shellTarget)
				return Shell.Current.GoToAsync(shellTarget.ToUri(), seg.IsAnimated);

			// If this is a simple navigation, bypass the Segue machinery...
			if (seg.Segue == null)
				return nav.NavigateAsync(seg.Action, (target as PageSegueTarget)?.ToPage(), seg.IsAnimated);

			return nav.SegueAsync(seg.Segue, target);
		}

		public static bool RequiresTarget(this NavigationAction action) => !action.IsPopping();
		internal static bool IsPopping(this NavigationAction action) => (action & NavigationAction.Pop) == NavigationAction.Pop;
		internal static bool ShouldPopModal(this INavigation nav) => nav.ModalStack.Count > 0;
	}
}
