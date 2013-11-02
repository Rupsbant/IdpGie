//
//  CairoWidget.cs
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
using Gtk;
using Cairo;

namespace IdpGie {

    public class CairoFrameWidget : CairoMediaWidget {

        private DrawTheory theory;
        public const double Offset = 10.0d;
        public const double Offset2 = Offset + BlueprintStyle.Thickness;
        public const double Offset3 = Offset2 + BlueprintStyle.Thickness;
        public const double Offset4 = Offset3 + BlueprintStyle.Thickness;
        public const double LineDelta = 10.0d;

        public DrawTheory Theory {
            get {
                return this.theory;
            }
            set {
                this.theory = value;
            }
        }

        public CairoFrameWidget (DrawTheory theory) {
            this.Theory = theory;
            this.AddEvents ((int)(Gdk.EventMask.PointerMotionMask | Gdk.EventMask.ButtonPressMask | Gdk.EventMask.ButtonReleaseMask));
        }


        protected override void PaintWidget (Context ctx, int w, int h) {
            this.paintBackground (ctx, w, h);
            ctx.Translate (Offset2, Offset2);
            ctx.Scale (1.0d, -1.0d);
            ctx.Save ();
            ctx.Color = new Color (0.0d, 0.0d, 0.0d);
            foreach (IIdpdObject obj in this.theory.Objects ().OrderBy (ZIndexComparator.Instance)) {
                obj.PaintObject (ctx);
            }
            ctx.Restore ();
        }

        private void paintBackground (Context ctx, int w, int h) {
            ctx.Color = BlueprintStyle.BluePrint;
            ctx.Rectangle (0.0d, 0.0d, w, h);
            ctx.Fill ();
            ctx.Color = BlueprintStyle.SoftWhite;
            ctx.Rectangle (Offset, Offset, w - 2 * Offset, h - 2 * Offset);
            ctx.ClosePath ();
            ctx.Rectangle (Offset2, Offset2, w - 2 * Offset2, h - 2 * Offset2);
            ctx.ClosePath ();
            ctx.Fill ();
            double W = w - 2 * Offset2;
            int n = (int)Math.Round ((double)W / LineDelta);
            double dx = (double)W / n;
            ctx.LineWidth = 0.5d;
            for (int i = 1; i < n; i++) {
                ctx.MoveTo (dx * i + Offset2, Offset2);
                ctx.RelLineTo (0.0d, h - 2 * Offset2);
                ctx.Stroke ();
            }
            double H = h - 2 * Offset2;
            n = (int)Math.Round ((double)H / LineDelta);
            double dy = (double)H / n;
            for (int i = 1; i < n; i++) {
                ctx.MoveTo (Offset2, dy * i + Offset2);
                ctx.RelLineTo (w - 2.0d * Offset2, 0.0d);
                ctx.Stroke ();
            }
        }

        protected override void OnSizeRequested (ref Gtk.Requisition requisition) {
            requisition.Height = 34;
            requisition.Width = 100;
        }

    }

}

