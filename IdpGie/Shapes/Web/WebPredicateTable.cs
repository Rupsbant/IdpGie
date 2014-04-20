//
//  WebPredicateTable.cs
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
using System.Collections.Generic;
using System.Xml.Serialization;
using IdpGie.Shapes.Pages;
using Gtk;

//TODO: interface
namespace IdpGie.Shapes.Web {
	/// <summary>
	/// A webshape that displays a table that contains the values of the predicates.
	/// </summary>
	[XmlRoot("predicatetable")]
	[WebShape("predicatetable")]
	public class WebPredicateTable : WebShapeBase {
		/// <summary>
		/// Gets or sets the query that should be resolved using the interactive lua shell.
		/// </summary>
		/// <value>The query to resolve by the interactive lua shell.</value>
		[XmlAttribute("query")]
		public string Query {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the columns of the resulting table.
		/// </summary>
		/// <value>The columns of the resulting table.</value>
		[XmlArray("columns")]
		[XmlArrayItem("column")]
		public List<WebPredicateTableColumn> Columns {
			get;
			set;
		}

		/// <summary>
		/// Determines whether a user can check the table rows. In that case the rows each have a unique identifier
		/// and the table contains a special column for checkboxes).
		/// </summary>
		/// <value><c>true</c> if the user can check rows; otherwise, <c>false</c>.</value>
		[XmlAttribute("check")]
		public bool CanCheck {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether the user can refresh the table.
		/// </summary>
		/// <value><c>true</c> if the user can refresh the table; otherwise, <c>false</c>.</value>
		[XmlAttribute("refresh")]
		public bool CanRefresh {
			get;
			set;
		}
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="IdpGie.Shapes.Web.WebPredicateTable"/> class.
		/// </summary>
		/// <remarks>
		/// <para>The default constructor is inteded for XML deserialization as well.</para>
		/// </remarks>
		public WebPredicateTable () {
		}
		#endregion
		#region IWebShape implementation
		/// <summary>
		/// Translates the given <see cref="IWebShape"/> into a relevant <see cref="IQueryWebPage"/> instance.
		/// </summary>
		/// <returns>
		/// An <see cref="IQueryWebPage"/> instances that can produce dynamic content.
		/// </returns>
		public override IQueryWebPage GetQueryPage () {
			return null;
		}

		/// <summary>
		/// Gets a page piece that performs the bridge between the webpage and the <see cref="IQueryWebPage"/>\
		/// generated by the <see cref="GetQueryPage()"/> method.
		/// </summary>
		/// <returns>A page piece to inject in the generated page to bind with the generated <see cref="IQueryWebPage"/>.</returns>
		public override IWebPagePiece GetPagePiece () {
			return new QueryLandingWebPagePiece (null);
		}
		#endregion
	}
}

