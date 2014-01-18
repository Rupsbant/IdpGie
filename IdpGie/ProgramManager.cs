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
using IdpGie.Parser;
using Mono.Unix;
using System.IO;
using Gtk;
using OpenTK.Graphics;

namespace IdpGie {
	public class ProgramManager : IDisposable {
		private TopWindow tw;
		private string idpFile;

		public bool Interactive {
			get {
				return this.idpFile != null;
			}
		}

		public string IdpFile {
			get {
				return this.idpFile;
			}
			set {
				if (value == string.Empty) {
					this.idpFile = null;
				} else {
					this.idpFile = value;
				}
			}
		}

		public ProgramManager () {
		}

		public void CreateWindow () {
			tw = new TopWindow ();
		}

		public void ShowWindow () {
			Application.Run ();
		}

		public void OpenFile () {
        
		}

		#region IDisposable implementation

		public void Dispose () {
			tw.Dispose ();
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
			GraphicsContext.ShareContexts = false;
			Catalog.Init ("IdpGie", "./locale");
			Application.Init ("IdpGie", ref args);
			Gdk.Threads.Init ();
			using (ProgramManager manager = new ProgramManager ()) {
				manager.CreateWindow ();
				DirectoryInfo dirInfo = new DirectoryInfo (".");
				foreach (string name in args) {
					FileInfo[] fInfo = dirInfo.GetFiles (name);
					foreach (FileInfo info in fInfo) {
						try {
							using (FileStream file = new FileStream (info.FullName, FileMode.Open)) {
								Lexer scnr = new Lexer (file);
								IdpParser pars = new IdpParser (info.Name, scnr);
								pars.Parse ();
								if (pars.Result != null) {
									pars.Result.Execute (manager);
								}
							}
						} catch (IOException) {
							Console.Error.WriteLine ("File \"{0}\" not found.", info.Name);
						}
					}
				}
				manager.ShowWindow ();
			}
			return 0x00;
		}
	}
}