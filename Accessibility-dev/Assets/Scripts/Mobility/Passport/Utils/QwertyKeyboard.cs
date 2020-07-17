using System;
using System.Collections.Generic;
using System.Linq;

namespace Mobility.Passport.Utils {
    public delegate IEnumerable<Key> GetSurroundingKeys(Key key);

    public class Key {
        public char keyCode { get; private set; }

        private GetSurroundingKeys getSurroundingKeys { get; set; }

        public IEnumerable<Key> surroundingKeys {
            get {
                return getSurroundingKeys(this);
            }
        }

        public Key(char keyCode, GetSurroundingKeys getSurroundingKeys) {
            this.keyCode = keyCode;
            this.getSurroundingKeys = getSurroundingKeys;
        }

        public override int GetHashCode() {
            return keyCode.GetHashCode();
        }

        public override bool Equals(object obj) {
            return this.keyCode == (obj as Key).keyCode;
        }
    }

    internal class KeyLink {
        internal Key key1 { get; private set; }

        internal Key key2 { get; private set; }

        internal KeyLink(Key key1, Key key2) {
            this.key1 = key1;
            this.key2 = key2;
        }

        internal bool Includes(Key key) {
            return key1.keyCode == key.keyCode || key2.keyCode == key.keyCode;
        }

        internal Key GetLinkedKey(Key key) {
            return key1.keyCode == key.keyCode ? key2 : key2.keyCode == key.keyCode ? key1 : null;
        }
    }

    public interface Keyboard {
        Key FindKey(char keyCode);

        Key this[char key] { get; }
    }

    public class QwertyKeyboard : Keyboard {
        public static IEnumerable<char> keyCodes { get; } = new List<char>() {
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
            'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L',
            'Z', 'X', 'C', 'V', 'B', 'N', 'M'
        };

        private IEnumerable<Key> keys { get; set; }

        private List<KeyLink> links { get; } = new List<KeyLink>();

        public QwertyKeyboard() {
            keys = keyCodes.Select(it => new Key(it, GetSurroundingKeysForKey));

            AddFirstRowLinks();
            AddFirstAndSecondRowLinks();
            AddSecondRowLinks();
            AddSecondAndThirdRowLinks();
            AddThirdRowLinks();
        }

        private void AddFirstRowLinks() {
            links.Add(CreateLink('Q', 'W'));
            links.Add(CreateLink('W', 'E'));
            links.Add(CreateLink('E', 'R'));
            links.Add(CreateLink('R', 'T'));
            links.Add(CreateLink('T', 'Y'));
            links.Add(CreateLink('Y', 'U'));
            links.Add(CreateLink('U', 'I'));
            links.Add(CreateLink('I', 'O'));
            links.Add(CreateLink('O', 'P'));
        }

        private void AddFirstAndSecondRowLinks() {
            links.Add(CreateLink('Q', 'A'));
            links.Add(CreateLink('W', 'A'));
            links.Add(CreateLink('W', 'S'));
            links.Add(CreateLink('E', 'S'));
            links.Add(CreateLink('E', 'D'));
            links.Add(CreateLink('R', 'D'));
            links.Add(CreateLink('R', 'F'));
            links.Add(CreateLink('T', 'F'));
            links.Add(CreateLink('T', 'G'));
            links.Add(CreateLink('Y', 'G'));
            links.Add(CreateLink('Y', 'H'));
            links.Add(CreateLink('U', 'H'));
            links.Add(CreateLink('U', 'J'));
            links.Add(CreateLink('I', 'J'));
            links.Add(CreateLink('I', 'K'));
            links.Add(CreateLink('O', 'K'));
            links.Add(CreateLink('O', 'L'));
            links.Add(CreateLink('P', 'L'));
        }

        private void AddSecondRowLinks() {
            links.Add(CreateLink('A', 'S'));
            links.Add(CreateLink('S', 'D'));
            links.Add(CreateLink('D', 'F'));
            links.Add(CreateLink('F', 'G'));
            links.Add(CreateLink('G', 'H'));
            links.Add(CreateLink('H', 'J'));
            links.Add(CreateLink('J', 'K'));
            links.Add(CreateLink('K', 'L'));
        }

        private void AddSecondAndThirdRowLinks() {
            links.Add(CreateLink('A', 'Z'));
            links.Add(CreateLink('S', 'Z'));
            links.Add(CreateLink('S', 'X'));
            links.Add(CreateLink('D', 'X'));
            links.Add(CreateLink('D', 'C'));
            links.Add(CreateLink('F', 'C'));
            links.Add(CreateLink('F', 'V'));
            links.Add(CreateLink('G', 'V'));
            links.Add(CreateLink('G', 'B'));
            links.Add(CreateLink('H', 'B'));
            links.Add(CreateLink('H', 'N'));
            links.Add(CreateLink('J', 'N'));
            links.Add(CreateLink('J', 'M'));
            links.Add(CreateLink('K', 'M'));
        }

        private void AddThirdRowLinks() {
            links.Add(CreateLink('Z', 'X'));
            links.Add(CreateLink('X', 'C'));
            links.Add(CreateLink('C', 'V'));
            links.Add(CreateLink('V', 'B'));
            links.Add(CreateLink('B', 'N'));
            links.Add(CreateLink('N', 'M'));
        }

        private KeyLink CreateLink(char key1, char key2) {
            return new KeyLink(FindKey(key1), FindKey(key2));
        }

        public Key FindKey(char keyCode) {
            return keys.FirstOrDefault(it => it.keyCode == Char.ToUpper(keyCode));
        }

        public Key this[char key] {
            get {
                return FindKey(key);
            }
        }

        internal IEnumerable<Key> GetSurroundingKeysForKey(Key key) {
            return links.FindAll(it => it.Includes(key)).Select(it => it.GetLinkedKey(key));
        }
    }
}
