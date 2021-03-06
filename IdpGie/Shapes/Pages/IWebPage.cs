//
//  INavbarPage.cs
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
using System.IO;

namespace IdpGie.Shapes.Pages {

	/// <summary>
	/// An interface that specified a web page. A web page has a name, reference and constructive content.
	/// </summary>
	public interface IWebPage : INameHref, IWebPagePiece {

		/// <summary>
		/// Gets or sets the navbar to which the <see cref="IWebPage"/> belongs.
		/// </summary>
		/// <value>
		/// The navbar to which the <see cref="IWebPage"/> belongs.
		/// </value>
		INavbar Navbar {
			get;
			set;
		}

	}

}

