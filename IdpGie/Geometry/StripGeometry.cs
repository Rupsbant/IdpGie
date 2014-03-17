using System;
using System.Text.RegularExpressions;
using IdpGie.Abstract;
using IdpGie.Utils;

namespace IdpGie.Geometry {
	public class StripGeometry : CloneableBase<IStripGeometry>, IStripGeometry {
		public const string identifier_width = @"w";
		public const string identifier_heigh = @"h";
		public const int DefaultWidth = 0x05, DefaultHeight = 0x05;
		public const int MinWidth = 0x01, MinHeight = 0x01;
		private int width = DefaultWidth, height = DefaultHeight;
		private static readonly Regex regex = RegexDevelopment.IntegerRegex / identifier_width + "[^0-9+-.]+" + RegexDevelopment.IntegerRegex / identifier_heigh;

		public int Width {
			get {
				return width;
			}
			set {
				width = Math.Max (MinWidth, value);
			}
		}

		public int Height {
			get {
				return height;
			}
			set {
				height = Math.Max (MinHeight, value);
			}
		}

		public StripGeometry (int width = DefaultWidth, int height = DefaultHeight) {
			this.Width = width;
			this.Height = height;
		}

		public static StripGeometry Parse (string text) {
			Match match = regex.Match (text);
			if (match.Success) {
				int w = int.Parse (match.Groups [identifier_width].Value);
				int h = int.Parse (match.Groups [identifier_heigh].Value);
				return new StripGeometry (w, h);
			} else {
				throw new FormatException (string.Format ("The geometry \"{0}\" does not meet the format criteria.", text));
			}
		}

		public override string ToString () {
			return string.Format ("{0} x {1}", this.Width, this.Height);
		}

		public void Collapse (int size) {
			if (size < this.width) {
				this.width = size;
				this.height = 0x01;
			} else {
				this.height = (size + width - 0x01) / width;
			}
		}

		public IStripGeometry CollapseClone (int size) {
			IStripGeometry g = this.Clone ();
			g.Collapse (size);
			return g;
		}

		#region CloneableBase implementation

		public override IStripGeometry Clone () {
			return new StripGeometry (this.width, this.height);
		}

		#endregion

	}
}

