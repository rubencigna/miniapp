using Microsoft.Data.SqlClient;

namespace Services;

public class CustomerService
{
    public static bool Create(CustomerModel data)
    {
        bool result = false;
        using (SqlConnection con = new())
        {
            var bc = IDBService.CreateConnection(".","mapdb");
            con.ConnectionString = bc.ConnectionString;
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "SP_CREATE_CUSTOMER";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var paramPhone = cmd.CreateParameter();
            paramPhone.ParameterName = @"@phone";
            paramPhone.DbType = System.Data.DbType.AnsiStringFixedLength;
            paramPhone.Direction = System.Data.ParameterDirection.Input;
            paramPhone.Value = data.Phone;
            paramPhone.Size = 12;
            cmd.Parameters.Add(paramPhone);
            cmd.Parameters.Add("@custName", System.Data.SqlDbType.Char, 30).Value = data.Name;
            cmd.Parameters.Add("@email", System.Data.SqlDbType.Char, 20).Value = data.Email;
            var isOK = cmd.ExecuteNonQuery();
            if (isOK > 0)
            {
                result = true;
            }
            cmd.Dispose();
        }
        return result;
    }
    public static List<CustomerModel> Read(object data)
    {
        var result = new List<CustomerModel>();
        using (SqlConnection con = new())
        {
            var bc =IDBService.CreateConnection(".","mapdb");
            con.ConnectionString = bc.ConnectionString;
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "SP_SELECT_CUSTOMER";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                result.Add(new CustomerModel(rs["EMAIL"].ToString()!, rs["NAME"].ToString()!, rs["PHONE"].ToString()!));
            }
            cmd.Dispose();
        }
        return result;
    }
    public static bool Update(object data)
    {
        bool result = false;
        return result;
    }
    public static bool Delete(object data)
    {
        bool result = false;

        return result;
    }


}