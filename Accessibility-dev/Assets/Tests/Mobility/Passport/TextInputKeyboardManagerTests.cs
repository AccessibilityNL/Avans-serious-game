using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Mobility.Passport;
using Mobility.Passport.Utils;

namespace Tests.Mobility.Passport {
    internal class TestKeyboard : Keyboard {
        internal static IEnumerable<char> keyCodes { get; } = new List<char>() {
            'A', 'B', 'C', 'Z'
        };

        private IEnumerable<Key> keys { get; set; }

        internal TestKeyboard() {
            keys = keyCodes.Select(it => new Key(it, GetSurroundingKeysForKey));
        }

        public Key FindKey(char keyCode) {
            return keys.FirstOrDefault(it => it.keyCode == Char.ToUpper(keyCode));
        }

        public Key this[char key] {
            get {
                return FindKey(key);
            }
        }

        internal IEnumerable<Key> GetList(char c) {
            return new List<Key>() { new Key(c, (k) => new List<Key>()) };
        }

        internal IEnumerable<Key> GetSurroundingKeysForKey(Key key) {
            switch (key.keyCode) {
                case 'A': return GetList('D');
                case 'B': return GetList('E');
                case 'C': return GetList('F');
                default: return GetList('Z');
            }
        }
    }

    public class TextInputKeyboardManagerTests {
        [Test]
        public void TestNormalOutput() {
            // Arrange
            var keyboard = new TextInputKeyboardManager(new TestKeyboard());
            var expectedKeys = new List<char>() { 'A', 'B', 'C' };

            // Act
            var actualKeys = new List<char>() {
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('B'),
                keyboard.OnNewCharacter('C')
            };

            // Assert
            Assert.That(actualKeys, Is.EquivalentTo(expectedKeys));
        }

        [Test]
        public void TestChangedOutput() {
            // Arrange
            var keyboard = new TextInputKeyboardManager(new TestKeyboard());
            var expectedKeys = new List<char>() { 'A', 'A', 'B', 'E', 'C', 'C' };

            // Act
            var actualKeys = new List<char>() {
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('B'),
                keyboard.OnNewCharacter('B'),
                keyboard.OnNewCharacter('C'),
                keyboard.OnNewCharacter('C')
            };

            // Assert
            Assert.That(actualKeys, Is.EquivalentTo(expectedKeys));
        }

        [Test]
        public void TestRepeatedChangedOutput() {
            // Arrange
            var keyboard = new TextInputKeyboardManager(new TestKeyboard());
            var expectedKeys = new List<char>() { 'A', 'A', 'B', 'E', 'C', 'C', 'A', 'D', 'B' };

            // Act
            var actualKeys = new List<char>() {
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('B'),
                keyboard.OnNewCharacter('B'),
                keyboard.OnNewCharacter('C'),
                keyboard.OnNewCharacter('C'),
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('A'),
                keyboard.OnNewCharacter('B'),
            };

            // Assert
            Assert.That(actualKeys, Is.EquivalentTo(expectedKeys));
        }
    }
}
