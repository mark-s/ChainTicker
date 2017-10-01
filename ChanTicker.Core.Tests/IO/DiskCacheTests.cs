using System;
using ChanTicker.Core.IO;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace ChanTicker.Core.Tests.IO
{
    [TestFixture]
    public class DiskCacheTests
    {
        private IFileIOService _fileIO;
        private DiskCache _diskCache;

        [SetUp]
        public virtual void SetUp()
        {
            _fileIO = A.Fake<IFileIOService>();
            _diskCache = new DiskCache(_fileIO);
        }


        [Test]
        public void IsStale_OutsideAgeDays_Stale()
        {
            // Arrange
            var file3DaysOld = DateTime.Now.AddDays(-3);
            var cacheLimit2Days = TimeSpan.FromDays(2);

            A.CallTo(() => _fileIO.FileExists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => _fileIO.GetFileSaveTime(ChainTickerFolder.Cache, A<string>.Ignored)).Returns(file3DaysOld);
            
            // Act
            var isStale = _diskCache.IsStale(ChainTickerFolder.Cache, "ignored", cacheLimit2Days);

            // Assert
            isStale.ShouldBeTrue();
        }



        [Test]
        public void IsStale_WithinAgeDays_NotStale()
        {
            // Arrange
            var file3DaysOld = DateTime.Now.AddDays(-3);
            var cacheLimit4Days = TimeSpan.FromDays(4);

            A.CallTo(() => _fileIO.FileExists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => _fileIO.GetFileSaveTime(ChainTickerFolder.Cache, A<string>.Ignored)).Returns(file3DaysOld);

            // Act
            var isStale = _diskCache.IsStale(ChainTickerFolder.Cache, "ignored", cacheLimit4Days);

            // Assert
            isStale.ShouldBeFalse();
        }

        [Test]
        public void IsStale_OutsideAgeHours_Stale()
        {
            // Arrange
            var file3HoursOld = DateTime.Now.AddHours(-3);
            var cacheLimit2Hours = TimeSpan.FromHours(2);

            A.CallTo(() => _fileIO.FileExists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => _fileIO.GetFileSaveTime(ChainTickerFolder.Cache, A<string>.Ignored)).Returns(file3HoursOld);

            // Act
            var isStale = _diskCache.IsStale(ChainTickerFolder.Cache, "ignored", cacheLimit2Hours);

            // Assert
            isStale.ShouldBeTrue();
        }



        [Test]
        public void IsStale_WithinAgeHours_NotStale()
        {
            // Arrange
            var file3HoursOld = DateTime.Now.AddHours(-3);
            var cacheLimit4Hours = TimeSpan.FromHours(4);

            A.CallTo(() => _fileIO.FileExists(A<string>.Ignored)).Returns(true);
            A.CallTo(() => _fileIO.GetFileSaveTime(ChainTickerFolder.Cache, A<string>.Ignored)).Returns(file3HoursOld);

            // Act
            var isStale = _diskCache.IsStale(ChainTickerFolder.Cache, "ignored", cacheLimit4Hours);

            // Assert
            isStale.ShouldBeFalse();
        }






    }
}
