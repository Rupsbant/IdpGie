//
//  ProgramManager.cs
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
using IdpGie.Parser;
using Mono.Unix;
using System.IO;
using Gtk;
using OpenTK.Graphics;
using Mono.Options;
using IdpGie.Utils;
using System.Text;

namespace IdpGie {
	public class ProgramManager : IDisposable {
		private TopWindow tw;
		private string idpFile = null;
		private string idpdFile = null;
		private string aspFile = null;
		private string hookFile = null;
		private string theory = "T";
		private string structure = "S";
		private string aspContent = null, hookContent = null;

		public bool Interactive {
			get {
				return this.IdpFile != null && this.AspFile != null;
			}
		}

		public IEnumerable<string> Files {
			get {
				yield return this.idpFile;
				yield return this.idpdFile;
				yield return this.aspFile;
				yield return this.hookFile;
			}
		}

		public string IdpFile {
			get {
				return this.idpFile;
			}
			set {
				this.idpFile = StringUtils.NonEmptyOrNull (value);
			}
		}

		public string HookFile {
			get {
				return this.hookFile;
			}
			set {
				this.HookFile = StringUtils.NonEmptyOrNull (value);
				this.hookContent = null;
			}
		}

		public string Theory {
			get {
				return this.theory;
			}
			set {
				this.theory = StringUtils.EffectiveOrDefault (value, this.theory);
			}
		}

		public string Structure {
			get {
				return this.structure;
			}
			set {
				this.structure = StringUtils.EffectiveOrDefault (value, this.structure);
			}
		}

		public string IdpdFile {
			get {
				return this.idpdFile;
			}
			set {
				this.idpdFile = StringUtils.NonEmptyOrNull (value);
			}
		}

		public bool ShowHelp {
			get;
			set;
		}

		public string AspFile {
			get {
				return this.aspFile;
			}
			set {
				this.aspFile = StringUtils.NonEmptyOrNull (value);
				this.aspContent = null;
			}
		}

		public string AspContent {
			get {
				return this.generateAspContent ();
			}
		}

		public string HookContent {
			get {
				return this.generateHookContent ();
			}
		}

		public ProgramManager () {
		}

		private string generateAspContent () {
			if (this.aspContent == null && this.aspFile != null) {
				using (FileStream fs = File.Open (this.aspFile, FileMode.Open, FileAccess.Read)) {
					using (TextReader tr = new StreamReader (fs)) {
						this.aspContent = tr.ReadToEnd ();
					}
				}
			}
			return this.aspContent;
		}

		private string generateHookContent () {
			if (this.hookContent == null) {
				if (this.hookFile != null) {
					using (FileStream fs = File.Open (this.hookFile, FileMode.Open, FileAccess.Read)) {
						using (TextReader tr = new StreamReader (fs)) {
							this.hookContent = tr.ReadToEnd ();
						}
					}
				} else {
					this.hookContent = string.Empty;
				}
			}
			return this.hookContent;
		}

		public void CreateWindow () {
			tw = new TopWindow ();
		}

		public void ShowWindow () {
			Application.Run ();
		}

		public void CheckConsistency () {
			if (this.ShowHelp) {
				return;
			}
			if (this.idpFile == null && this.aspFile == null && this.idpdFile == null) {
				throw new IdpGieException ("No files are given.");
			} else if (this.idpdFile == null) {
				if (this.idpFile == null) {
					throw new IdpGieException ("Interactive mode but the .idp file is missing.");
				} else if (this.aspFile == null) {
					throw new IdpGieException ("Interactive mode but the .asp file is missing.");
				}
			}
			foreach (string file in this.Files) {
				if (file != null && !File.Exists (file)) {
					throw new IdpGieException ("Cannot find file \"{0}\".", file);
				}
			}
		}

		public void OpenFile () {
        
		}

		#region IDisposable implementation

		public void Dispose () {
			if (tw != null) {
				tw.Dispose ();
			}
		}

		#endregion

		public void OpenTab<T> (DrawTheory dt, T widget) where T : Widget, IMediaObject {
			this.tw.CreateTab<T> (dt, widget);
		}

		public Stream GetIdpStream () {
			if (this.Interactive) {
				return File.Open (this.idpFile, FileMode.Open, FileAccess.ReadWrite);
			} else {
				throw new IdpGieException ("Cannot alter the Idp file, running the program in noninteractive mode.");
			}
		}

		public static int Main (string[] args) {
			//GraphicsContext.ShareContexts = false;
			Catalog.Init ("IdpGie", "./locale");
			using (ProgramManager manager = new ProgramManager ()) {
				OptionSet options = new OptionSet () { {
						"a|asp=",
						"Feed the system an .asp file in order to convert an idp model into drawing instructions.",
						x => manager.AspFile = x
					}, {
						"i|idp=",
						"Feed the system an .idp file. The program must be given an .asp file as well in order to paint something.",
						x => manager.IdpFile = x
					},
					{ "d|idpd=",  "Feed the system a .idpd file. Limited interactive mode is enabled.", x => manager.IdpdFile = x },
					{ "H|idph=",  "Feed the system a .idph file that contains the defined hooks.", x => manager.HookFile = x },
					{ "t|theory=",  "The theory to use in the .idp file, only for interactive mode.", x => manager.Theory = x },
					{ "s|structure=","The structure to use in the .idp file, only for interactive mode.",x => manager.Structure = x },
					{ "h|?|help", "Show this help manual and exit.",   x => manager.ShowHelp = (x != null) },
				};
				try {
					options.Parse (args);
					manager.CheckConsistency ();

					if (manager.ShowHelp) {
						Console.WriteLine ("Usage: idpgie [OPTIONS]+");
						Console.WriteLine ("IDP-GIE is a Graphical Interactive Environment for the IDP system.");
						Console.WriteLine ();
						Console.WriteLine ("Options:");
						options.WriteOptionDescriptions (Console.Out);
					} else {
						Application.Init ("IdpGie", ref args);
						Gdk.Threads.Init ();
						IContentChangeableStream strm;
						string filename;
						manager.CreateWindow ();
						if (manager.Interactive) {
							strm = new IdpInteractiveStream (manager.IdpFile, manager.Theory, manager.Structure, manager.AspContent, manager.HookContent);
							filename = manager.IdpFile;
						} else {
							strm = new ContentChangeableStream (new FileStream (manager.IdpdFile, FileMode.Open));
							filename = manager.IdpdFile;
						}
						DrawTheory dt = new DrawTheory (filename, strm);
						//pars.Result.Execute ();
						OutputDevice dev = dt.GetOutputDevice ();
						dev.Run (manager);
						manager.ShowWindow ();
					}
				} catch (Exception e) {
					Console.Write ("idpgie: ");
					Console.WriteLine (e.Message);
					Console.WriteLine (e.StackTrace);
					Console.WriteLine ("Try `idpgie --help' for more information.");
				}
			}
			return 0x00;
		}
	}
}