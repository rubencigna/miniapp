namespace Services;

/// <summary>
/// Frontend of Mini-Web-Application using .NET 8 webapp MVC
/// Author  : Ruben Djo Radja
/// Created : Nov 25 2023 
/// </summary>
public interface IAPIService
{
    /// <summary>
    /// List all backend endpoints here
    /// </summary>
    public static string BaseAddress = "http://localhost:5105";

    // customer endpoint
    public static string CustomerEndpointCreate = "/Api/Customer/Create.do";
    public static string CustomerEndpointGetAll = "Api/Customer/GetAll.do";

    // auth endpoint
    public static string AuthEndpointValidate="Api/Auth/Validate.do";
    // user endpoint
    public static string UserEndpointCreate="/Api/User/Create";
     public static string UserEndpointRead="/Api/User/Read";
      public static string UserEndpointUpdate="/Api/User/Update";
       public static string UserEndpointDelete="/Api/User/Delete";
    

}