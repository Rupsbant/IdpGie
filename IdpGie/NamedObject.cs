//
//  NamedObject.cs
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
using System;

namespace IdpGie {
	[NamedObjectEnum]
	public enum NamedObject : ulong {

		#region Generic   (0x0000000000000000 family)

		[NamedObject]
		Unknown = 0x0000000000000000,
		#endregion

		#region Keys      (0x0100000000000000 family)

		#endregion

		#region Mouse

		#endregion

		#region ModeNames (0x0300000000000000 family)

		[NamedObject]
		Opengl = 0x0300000000000001,
		[NamedObject]
		Cairo = 0x0300000000000002,
		[NamedObject]
		Latex = 0x0000000000000003,
		[NamedObject]
		Print = 0x0000000000000004

		#endregion

	}
}

