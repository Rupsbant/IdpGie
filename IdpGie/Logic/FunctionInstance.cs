//
//  FunctionInstance.cs
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
using System.Linq;
using System.Collections.Generic;
using IdpGie.Utils;

namespace IdpGie.Logic {
	public class FunctionInstance : Term, IFunctionInstance {

		#region IFunctionInstance implementation

		public TermType Type {
			get {
				return this.Function.OutputType;
			}
		}

		public IFunction Function {
			get {
				return (IFunction)this.Header;
			}
		}

		public virtual object Value {
			get {
				return this;
			}
		}

		#endregion

		public FunctionInstance (IFunction func, List<IFunctionInstance> terms) : base (func, terms) {
		}

		public FunctionInstance (IFunction func, params IFunctionInstance[] terms) : base (func, terms.ToList ()) {
		}

		#region IFunctionInstance implementation

		public bool CanConvert (TermType type) {
			return TypeSystem.CanConvert (this.Type, type);
		}

		public object ConvertedValue (TermType target) {
			return this.Value;
		}

		#endregion

		public override int GetHashCode () {
			int hash = this.Header.GetHashCode (), spill;
			foreach (IFunctionInstance ifi in this.Terms) {
				spill = (hash >> 0x15) & 0x0fff;
				hash <<= 0x0b;
				hash |= spill;
				hash ^= ifi.GetHashCode ();
			}
			return hash;
		}

		public override bool Equals (object obj) {
			if (obj is IFunctionInstance) {
				IFunctionInstance ifi = (IFunctionInstance)obj;
				return Object.Equals (this.Function, ifi.Function) && this.Terms.AllDual (ifi.Terms, (x, y) => x.Equals (y));
			}
			return false;
		}
	}
}

