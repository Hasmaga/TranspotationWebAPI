namespace TranspotationWebAPI.Model
{
    public class Role
    {
        public int roleId { get; set; }
        public String roleName { get; set; }

        //Contructor
        public Role(int roleId, string roleName)
        {
            this.roleId = roleId;
            this.roleName = roleName;
        }
    }
}
