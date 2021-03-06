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
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Cairo;
using OpenTK;
using IdpGie.Logic;
using IdpGie.Shapes.Modifiers;
using IdpGie.UserInterface;

namespace IdpGie.Shapes {
	public abstract class Shape<TShapeState> : IShape where TShapeState : IShapeState, new() {
		private readonly IFunctionInstance name;
		private readonly TShapeState state = new TShapeState ();
		private IdpGie.Geometry.Point3 textOffset = new IdpGie.Geometry.Point3 (0.0d, 0.0d);

		#region IIdpdObject implementation

		public IFunctionInstance Name {
			get {
				return this.name;
			}
		}

		public double Time {
			get {
				return this.state.Time;
			}
			set {
				this.state.SetTime (value);
			}
		}

		#endregion

		public IdpGie.Geometry.Point3 TextOffset {
			get {
				return this.textOffset;
			}
			protected set {
				this.textOffset = value;
			}
		}

		#region IShape implementation

		IShapeState IShape.State {
			get {
				return this.State;
			}
		}
		#endregion

		#region IZIndix implementation

		public double ZIndex {
			get {
				return this.state.ZIndex;
			}
		}

		#endregion

		#region IIdpdObject implementation

		public TShapeState State {
			get {
				return this.state;
			}
		}

		#endregion

		public virtual string HtmlTag {
			get {
				return null;
			}
		}

		public virtual IEnumerable<KeyValueEntry> HtmlAttributes {
			get {
				yield break;
			}
		}

		protected Shape (IFunctionInstance name) {
			this.name = name;
		}

		protected virtual void InnerPaintObject (Context ctx) {
			cairoFillStroke (ctx);
			cairoShowText (ctx);
		}

		protected virtual void InnerWriteTikz (StringBuilder sb) {

		}

		#region IIdpdObject implementation

		public virtual void PaintObject (Context ctx) {
			if (this.state.Visible) {
				ctx.Save ();
				ctx.Transform (this.state.GetElement<Matrix> ("CairoTransformations"));
				this.InnerPaintObject (ctx);
				ctx.Restore ();
			}
		}

		public virtual void WriteTikz (StringBuilder builder) {
			builder.AppendFormat (@"\begin{0}[xshift={1} cm,yshift={2} cm]", "{scope}", this.State.GetElement<double> ("Xpos"), this.State.GetElement<double> ("Ypos"));
			this.InnerWriteTikz (builder);
			builder.Append (@"\end{scope}");
		}

		public virtual void WriteXhtml (Html32TextWriter writer) {
			if (this.HtmlTag != null && this.HtmlTag != string.Empty) {
				foreach (KeyValueEntry kve in this.HtmlAttributes) {
					writer.AddAttribute (kve.Key, kve.Value);
				}
				writer.RenderBeginTag (this.HtmlTag);
				writer.RenderEndTag ();
			}
		}

		public virtual void Render (FrameEventArgs e) {

		}

		public void AddModifier (ShapeStateModifier modifier) {
			this.State.AddModifier (modifier);
		}

		public void AddModifier (double time, Action<IShapeState> modifier) {
			this.State.AddModifier (time, modifier);
		}

		#endregion

		private void cairoFillStroke (Context ctx) {
			ctx.SetFill (this.state.GetElement<Color> ("InnerColor"));
			ctx.FillPreserve ();
			ctx.SetFill (this.state.GetElement<Color> ("EdgeColor"));
			ctx.Stroke ();
		}

		private void cairoShowText (Context ctx) {
			string text = this.state.GetElement<string> ("Text");
			if (text != null && text != string.Empty) {
				TextExtents te = ctx.TextExtents (text);
				ctx.MoveTo (-0.5d * te.XAdvance + this.textOffset.X, 0.5d * (te.Height + 0.5d * te.YBearing) + this.textOffset.Y);
				ctx.ShowText (text);
			}
		}

		public override string ToString () {
			return string.Format ("{0}[{1}]", this.GetType ().Name, this.Name);
		}

		#region IIdpdTransformable implementation

		public void SetXPos (double xpos) {
			this.state.SetXPos (xpos);
		}

		public void SetYPos (double ypos) {
			this.state.SetYPos (ypos);
		}

		public void SetZPos (double zpos) {
			this.state.SetZPos (zpos);
		}

		#endregion

	}
}

