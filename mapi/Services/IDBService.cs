using Microsoft.Data.SqlClient;

namespace Services;
/// <summary>
/// Backend Service for Mini Application using .NET 8 minimal api (native AOT)
/// Author : Ruben Djo Radja
/// Created: Nov 25 2023
/// </summary>
public interface IDBService
{
    public static SqlConnectionStringBuilder CreateConnection(string ds, string ic)
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = ds,
            InitialCatalog = ic,
            IntegratedSecurity = true,
            ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled,
            Encrypt = false,
        };
        return builder;
    }
}