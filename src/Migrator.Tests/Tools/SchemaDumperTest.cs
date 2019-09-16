#region License

//The contents of this file are subject to the Mozilla Public License
//Version 1.1 (the "License"); you may not use this file except in
//compliance with the License. You may obtain a copy of the License at
//http://www.mozilla.org/MPL/
//Software distributed under the License is distributed on an "AS IS"
//basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
//License for the specific language governing rights and limitations
//under the License.

#endregion License

using Migrator.Providers;
using Migrator.Tools;
using NUnit.Framework;
using System;
using System.Configuration;

namespace Migrator.Tests.Tools
{
	[TestFixture, Category("SqlServer2005")]
	public class SchemaDumperSqlServerTest
	{
		[Test]
		public void Dump()
		{
			string constr = ConfigurationManager.AppSettings["SqlServerConnectionString"];

			if (constr == null)
				throw new ArgumentNullException("SqlServerConnectionString", "No config file");

			SchemaDumper dumper = new SchemaDumper(ProviderTypes.SqlServer, constr, "");
			string output = dumper.Dump();

			Assert.IsNotNull(output);
		}
	}
}
