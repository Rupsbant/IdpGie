//
//  DrawTheory.cs
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

namespace IdpGie {

    public class DrawTheory {

        private readonly List<ITheoryItem> elements;
        private readonly Dictionary<IFunctionInstance,IdpdObject> objects = new Dictionary<IFunctionInstance, IdpdObject> ();

        private DrawTheoryMode mode = DrawTheoryMode.Cairo;

        public DrawTheoryMode Mode {
            get {
                return this.mode;
            }
            set {
                this.mode = value;
            }
        }

        public IdpdObject this [IFunctionInstance key] {
            get {
                return this.objects [key];
            }
        }

        public double Time {
            set {
                foreach (IdpdObject obj in objects.Values) {
                    obj.Time = value;
                }
            }
        }

        public List<ITheoryItem> Elements {
            get {
                return this.elements;
            }
        }

        public DrawTheory (List<ITheoryItem> elements) {
            if (elements != null) {
                this.elements = elements;
            } else {
                this.elements = new List<ITheoryItem> ();
            }
        }

        public string ToFullString () {
            StringBuilder sb = new StringBuilder ();
            foreach (ITheoryItem atm in elements) {
                sb.AppendFormat ("{0}.", atm);
                sb.AppendLine ();
            }
            return sb.ToString ();
        }

        internal void AddIdpdObject (IdpdObject obj) {
            this.objects.Add (obj.Name, obj);
            Console.WriteLine ("Added {0} now {1}", obj, this.objects.Count);
        }

        public IEnumerable<IdpdObject> Objects () {
            return this.objects.Values;
        }

        public void Execute () {
            //TODO: Tp operator
            elements.Sort (PriorityComparator.Instance);
            foreach (ITheoryItem item in elements) {
                item.Execute (this);
            }
            switch (this.Mode) {
            case DrawTheoryMode.Cairo:
                using (IdpdOutputDevice device = new IdpdCairoOutputDevice(this)) {
                    device.Run ();
                }
                break;
            case DrawTheoryMode.OpenGL:
                using (IdpdOutputDevice device = new IdpdOpenGLOutputDevice(this)) {
                    device.Run ();
                }
                break;
            case DrawTheoryMode.LaTeX:
                using (IdpdOutputDevice device = new IdpdLaTeXOutputDevice(this)) {
                    device.Run ();
                }
                break;
            case DrawTheoryMode.Print:
                using (IdpdOutputDevice device = new IdpdPrintOutputDevice(this)) {
                    device.Run ();
                }
                break;
            }
        }

    }
}

