using System;

namespace IdpGie {
	[OutputDevice ("pdf")]
	public class OutputPdfDevice : OutputDevice {
		public OutputPdfDevice (DrawTheory dt) : base (dt) {
		}

		#region implemented abstract members of OutputDevice

		public override void Run (ProgramManager manager) {
			throw new NotImplementedException ();
		}

		#endregion

	}
}
