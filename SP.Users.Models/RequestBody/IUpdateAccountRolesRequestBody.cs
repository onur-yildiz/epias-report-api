namespace SP.Users.Models.RequestParams
{
    public interface IUpdateAccountRolesRequestBody
    {
        string[] Roles { get; set; }
    }
}