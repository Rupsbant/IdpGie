//
//  Flyweight.cs
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

namespace IdpGie.Abstract {

    public class HardFlyweight<TKey,TValue> : IFlyWeight<TKey,TValue> {

        private readonly Dictionary<TKey,TValue> cache = new Dictionary<TKey, TValue> ();
        private readonly Func<TKey,TValue> generator;

        #region IFlyWeight implementation
        public Func<TKey, TValue> Generator {
            get {
                return this.generator;
            }
        }

        public bool IsWeak {
            get {
                return false;
            }
        }

        public bool DisposeSupport {
            get {
                return false;
            }
        }
        #endregion

        public HardFlyweight (Func<TKey,TValue> generator) {
            this.generator = generator;
        }

        #region IFlyWeight implementation
        public TValue GetOrCreate (TKey key) {
            TValue res;
            if (!this.cache.TryGetValue (key, out res)) {
                res = this.generator (key);
                this.cache.Add (key, res);
            }
            return res;
        }

        public bool Contains (TKey key) {
            return this.cache.ContainsKey (key);
        }
        #endregion

    }
}

