using NUnit.Framework;
using Mobility.Movement.Utils;

namespace Tests.Mobility.Movement.Utils {
    public class VariableSpeedTests {
        [Test]
        public void TestSpeedSwitchesBetweenLowAndHigh() {
            // Arrange
            var variableSpeed = new VariableSpeed(1, 0.5f, 2);

            // Act
            var speed1 = variableSpeed.current;
            variableSpeed.UpdateSpeed();
            var speed2 = variableSpeed.current;
            variableSpeed.UpdateSpeed();
            var speed3 = variableSpeed.current;
            variableSpeed.UpdateSpeed();
            var speed4 = variableSpeed.current;

            // Assert
            Assert.That(speed1, Is.InRange(0.5f, 1));
            Assert.That(speed2, Is.InRange(1, 2));
            Assert.That(speed3, Is.InRange(0.5f, 1));
            Assert.That(speed4, Is.InRange(1, 2));
        }
    }
}
