using Migrator.Framework;
using Migrator.Providers;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;

namespace Migrator.Tests
{
	[TestFixture]
	public class ProviderFactoryTest
	{
		[Test]
		public void CanGetDialectsForProvider()
		{
			foreach (ProviderTypes provider in Enum.GetValues(typeof(ProviderTypes)).Cast<ProviderTypes>().Where(x => x != ProviderTypes.none))
			{
				Assert.IsNotNull(ProviderFactory.DialectForProvider(provider));
			}
			Assert.IsNull(ProviderFactory.DialectForProvider(ProviderTypes.none));
		}

		[Test]
		[Category("SqlServer2005")]
		public void CanLoad_SqlServer2005Provider()
		{
			ITransformationProvider provider = ProviderFactory.Create(ProviderTypes.SqlServer2005,
																	  ConfigurationManager.AppSettings[
																																	"SqlServer2005ConnectionString"], null);
			Assert.IsNotNull(provider);
		}

		[Test]
		[Category("SqlServer")]
		public void CanLoad_SqlServerProvider()
		{
			ITransformationProvider provider = ProviderFactory.Create(ProviderTypes.SqlServer,
																	  ConfigurationManager.AppSettings[
																																	"SqlServerConnectionString"], null);
			Assert.IsNotNull(provider);
		}
	}
}
