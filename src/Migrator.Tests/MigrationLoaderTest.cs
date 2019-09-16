using Migrator.Framework;
using Migrator.Framework.Loggers;
using NSubstitute;
using NUnit.Framework;
using System.Reflection;

namespace Migrator.Tests
{
	[TestFixture]
	public class MigrationLoaderTest
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			SetUpCurrentVersion(0, false);
		}

		#endregion Setup/Teardown

		private MigrationLoader _migrationLoader;

		private void SetUpCurrentVersion(int version, bool assertRollbackIsCalled)
		{
			var providerMock = Substitute.For<ITransformationProvider>();

			providerMock.Logger.Returns(new Logger(false));
			if (assertRollbackIsCalled)
				providerMock.Received().Rollback();
			else
				providerMock.DidNotReceive().Rollback();

			_migrationLoader = new MigrationLoader((ITransformationProvider)providerMock, Assembly.GetExecutingAssembly(), true);
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.FirstMigration));
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.SecondMigration));
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.ThirdMigration));
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.ForthMigration));
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.BadMigration));
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.SixthMigration));
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.NonIgnoredMigration));
		}

		[Test]
		public void CheckForDuplicatedVersion()
		{
			_migrationLoader.MigrationsTypes.Add(typeof(MigratorTest.FirstMigration));
			Assert.Throws<DuplicatedVersionException>(() => _migrationLoader.CheckForDuplicatedVersion());
		}

		[Test]
		public void LastVersion()
		{
			Assert.AreEqual(7, _migrationLoader.LastVersion);
		}

		[Test]
		public void NullIfNoMigrationForVersion()
		{
			Assert.IsNull(_migrationLoader.GetMigration(99999999));
		}

		[Test]
		public void ZeroIfNoMigrations()
		{
			_migrationLoader.MigrationsTypes.Clear();
			Assert.AreEqual(0, _migrationLoader.LastVersion);
		}
	}
}
