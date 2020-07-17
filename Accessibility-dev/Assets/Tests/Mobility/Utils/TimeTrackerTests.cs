using NUnit.Framework;
using Mobility.Utils;
using UnityEngine;

namespace Tests.Mobility.Utils {
    public class TimeTrackerTests {
        [Test]
        public void TestNoInvokeWithHalfSeconds() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, false);

            // Act
            timeTracker.Tick(0.5f);

            // Assert
            Assert.AreEqual(0, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestSingleInvokeWithHalfSeconds() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, false);

            // Act
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestDoubleInvokeWithHalfSeconds() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, false);

            // Act
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);

            // Assert
            Assert.AreEqual(2, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestNoInvoke() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(2, () => {
                numberOfTimesCallbackIsInvoked++;
            }, false);

            // Act
            timeTracker.Tick(1);

            // Assert
            Assert.AreEqual(0, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestSingleInvoke() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(2, () => {
                numberOfTimesCallbackIsInvoked++;
            }, false);

            // Act
            timeTracker.Tick(1);
            timeTracker.Tick(1);

            // Assert
            Assert.AreEqual(1, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestDoubleInvoke() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, false);

            // Act
            timeTracker.Tick(1);
            timeTracker.Tick(1);

            // Assert
            Assert.AreEqual(2, numberOfTimesCallbackIsInvoked);
        }


        [Test]
        public void TestNoInvokeWithHalfSecondsAndSingleRun() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, true);

            // Act
            timeTracker.Tick(0.5f);

            // Assert
            Assert.AreEqual(0, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestSingleInvokeWithHalfSecondsAndSingleRun() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, true);

            // Act
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestDoubleInvokeWithHalfSecondsAndSingleRun() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, true);

            // Act
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);
            timeTracker.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestNoInvokeAndSingleRun() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(2, () => {
                numberOfTimesCallbackIsInvoked++;
            }, true);

            // Act
            timeTracker.Tick(1);

            // Assert
            Assert.AreEqual(0, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestSingleInvokeAndSingleRun() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(2, () => {
                numberOfTimesCallbackIsInvoked++;
            }, true);

            // Act
            timeTracker.Tick(1);
            timeTracker.Tick(1);

            // Assert
            Assert.AreEqual(1, numberOfTimesCallbackIsInvoked);
        }

        [Test]
        public void TestDoubleInvokeAndSingleRun() {
            // Arrange
            var numberOfTimesCallbackIsInvoked = 0;
            var timeTracker = new TimeTracker(1, () => {
                numberOfTimesCallbackIsInvoked++;
            }, true);

            // Act
            timeTracker.Tick(1);
            timeTracker.Tick(1);

            // Assert
            Assert.AreEqual(1, numberOfTimesCallbackIsInvoked);
        }
    }
}
