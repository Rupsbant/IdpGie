//
//  INavbarContent.cs
//
//  Author:
//       Willem Van Onsem <Willem.VanOnsem@cs.kuleuven.be>
//
//  Copyright (c) 2014 Willem Van Onsem
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using IdpGie.Abstract;
using System.Collections.Generic;

namespace IdpGie.OutputDevices.Web {

	/// <summary>
	/// An interface that represents the navbar: a bar on top of every webpage.
	/// </summary>
	public interface INavbar : IName {

		/// <summary>
		/// Gets the list of pages that should be listed in the navbar.
		/// </summary>
		/// <value>
		/// The list of pages that should be listed in the navbar.
		/// </value>
		IList<IWebPage> Pages {
			get;
		}

		/// <summary>
		/// Gets the default web page.
		/// </summary>
		/// <value>
		/// The default web page.
		/// </value>
		/// <remarks>
		/// <para>
		/// If no default web page is provided, <c>null</c> is returned.
		/// </para>
		/// </remarks>
		IWebPage DefaultPage {
			get;
		}

		/// <summary>
		/// Gets the web page associated with the given reference.
		/// </summary>
		/// <returns>
		/// The web page associated with the given reference.
		/// </returns>
		/// <param name='href'>
		/// The given reference.
		/// </param>
		/// <remarks>
		/// <para>
		/// If null is provided, or the root, the default page element is returned.
		/// </para>
		/// </remarks>
		IWebPage GetPage (string href);

	}
}
