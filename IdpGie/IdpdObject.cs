//
//  IdpdObject.cs
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
using System.Text;
using Cairo;

namespace IdpGie {

    public abstract class IdpdObject : IZIndex {

        private readonly IFunctionInstance name;
        private readonly IdpdObjectTimeState state = new IdpdObjectTimeState ();

        public IFunctionInstance Name {
            get {
                return this.name;
            }
        }

        public virtual double Time {
            set {
            }
        }

        #region IZIndix implementation
        public double ZIndex {
            get {
                return this.state.Zpos;
            }
        }
        #endregion

        public IdpdObjectTimeState State {
            get {
                return this.state;
            }
        }

        protected IdpdObject (IFunctionInstance name) {
            this.name = name;
        }

        protected virtual void InnerPaintObject (Context ctx) {
            this.CairoFillStroke (ctx);
        }

        public virtual void PaintObject (Context ctx) {
            ctx.Save ();
            //TODO: translate
            this.InnerPaintObject (ctx);
            ctx.Restore ();
        }

        protected void CairoFillStroke (Context ctx) {
            ctx.FillPreserve ();
            ctx.Stroke ();
        }

        public virtual void WriteTikz (StringBuilder builder) {

        }

        public override string ToString () {
            return string.Format ("{0}[{1}]", this.GetType ().Name, this.Name);
        }

    }

}

