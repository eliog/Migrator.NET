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

using Migrator.Framework;
using Migrator.Providers;
using Migrator.Providers.SqlServer;
using System;
using System.Data;

namespace Migrator
{
	/// <summary>
	/// Handles loading Provider implementations
	/// </summary>
	public class ProviderFactory
	{
		static ProviderFactory()
		{ }

		/// <summary>
		///
		/// </summary>
		/// <param name="providerType"></param>
		/// <param name="connectionString"></param>
		/// <param name="defaultSchema"></param>
		/// <param name="scope"></param>
		/// <param name="providerName">for Example: System.Data.SqlClient</param>
		/// <returns></returns>
		public static ITransformationProvider Create(ProviderTypes providerType, string connectionString, string defaultSchema, string scope = "default", string providerName = "")
		{
			Dialect dialectInstance = DialectForProvider(providerType);

			return dialectInstance.NewProviderForDialect(connectionString, defaultSchema, scope, providerName);
		}

		public static ITransformationProvider Create(ProviderTypes providerType, IDbConnection connection, string defaultSchema, string scope = "default", string providerName = "")
		{
			Dialect dialectInstance = DialectForProvider(providerType);

			return dialectInstance.NewProviderForDialect(connection, defaultSchema, scope, providerName);
		}

		public static Dialect DialectForProvider(ProviderTypes providerType)
		{
			switch (providerType)
			{
				case ProviderTypes.SqlServer:
					return (Dialect)Activator.CreateInstance(typeof(SqlServerDialect));
				case ProviderTypes.SqlServer2005:
					return (Dialect)Activator.CreateInstance(typeof(SqlServer2005Dialect));
			}

			return null;
		}
	}
}
