//
//  Box2d.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2013 Willem Van Onsem
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

namespace IdpGie.Geometry {

	/// <summary>
	/// A two dimensional box that contains all the points with an X-coordinate between <see cref="Box2d.Xmin"/> and <see cref="Box2d.Xmax"/> and an Y-coordinate between <see cref="Box2d.Ymin"/> and <see cref="Box2d.Ymax"/>.
	/// </summary>
	public class Box2d : IBox2d {

		private double xmin;
		private double xmax;
		private double ymin;
		private double ymax;

		#region IBox2d implementation
		/// <summary>
		///  Gets or sets the minimum value for the X-coordinate a point should have to belong to the box. 
		/// </summary>
		/// <value>
		///  The minimum value for the X-coordinate a point should have to belong to the box. 
		/// </value>
		public double Xmin {
			get {
				return this.xmin;
			}
			set {
				this.xmin = value;
			}
		}

		/// <summary>
		///  Gets or sets the maximum value for the X-coordinate a point should have to belong to the box. 
		/// </summary>
		/// <value>
		///  The maximum value for the X-coordinate a point should have to belong to the box. 
		/// </value>
		public double Xmax {
			get {
				return this.xmax;
			}
			set {
				this.xmax = value;
			}
		}

		/// <summary>
		///  Gets or sets the minimum value for the Y-coordinate a point should have to belong to the box. 
		/// </summary>
		/// <value>
		///  The minimum value for the Y-coordinate a point should have to belong to the box. 
		/// </value>
		public double Ymin {
			get {
				return this.ymin;
			}
			set {
				this.ymin = value;
			}
		}

		/// <summary>
		///  Gets or sets the maximum value for the Y-coordinate a point should have to belong to the box. 
		/// </summary>
		/// <value>
		///  The maximum value for the Y-coordinate a point should have to belong to the box. 
		/// </value>
		public double Ymax {
			get {
				return this.ymax;
			}
			set {
				this.ymax = value;
			}
		}
		#endregion

        #region IGeometry2d implementation
		/// <summary>
		///  Gets the surrounding box of the space. 
		/// </summary>
		/// <value>
		///  The surrounding box of the space. 
		/// </value>
		public Box2d SurroundingBox {
			get {
				return this;
			}
		}
        #endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="IdpGie.Geometry.Box2d"/> class with a given minimum x, maximum x, minimum y and maximum y.
		/// </summary>
		/// <param name='xmin'>
		/// The minimum value for the X-coordinate a point should have to belong to the box.
		/// </param>
		/// <param name='xmax'>
		/// The maximum value for the X-coordinate a point should have to belong to the box.
		/// </param>
		/// <param name='ymin'>
		/// The minimum value for the Y-coordinate a point should have to belong to the box.
		/// </param>
		/// <param name='ymax'>
		/// The maximum value for the Y-coordinate a point should have to belong to the box.
		/// </param>
		public Box2d (double xmin, double xmax, double ymin, double ymax) {
			this.Xmin = xmin;
			this.Xmax = xmax;
			this.Ymin = ymin;
			this.Ymax = ymax;
		}

        #region IGeometry2d implementation
		/// <summary>
		///  Checks if the given space contains the given point. 
		/// </summary>
		/// <param name='pt'>
		///  The point to check for. 
		/// </param>
		public bool Contains (Point3 pt) {
			return xmin <= pt.X && pt.X <= xmax && ymin <= pt.Y && pt.Y <= ymax;
		}
        #endregion

	}
}

