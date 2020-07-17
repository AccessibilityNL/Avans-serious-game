using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Mobility.Passport.Utils;

namespace Tests.Mobility.Passport.Utils {
    public class QwertyKeyboardTests {
        [Test]
        public void TestKeyReferencesForQKey() {
            // Arrange
            var keyboard = new QwertyKeyboard();
            var key = keyboard.FindKey('Q');
            var expectedKeys = new List<char>() { 'W', 'A' };

            // Act
            var surroundingKeys = key.surroundingKeys;

            // Assert
            Assert.That(surroundingKeys.Select(it => it.keyCode), Is.EquivalentTo(expectedKeys));
        }

        [Test]
        public void TestKeyReferencesForGKey() {
            // Arrange
            var keyboard = new QwertyKeyboard();
            var key = keyboard.FindKey('G');
            var expectedKeys = new List<char>() { 'T', 'Y', 'H', 'B', 'V', 'F' };

            // Act
            var surroundingKeys = key.surroundingKeys;

            // Assert
            Assert.That(surroundingKeys.Select(it => it.keyCode), Is.EquivalentTo(expectedKeys));
        }
    }
}
