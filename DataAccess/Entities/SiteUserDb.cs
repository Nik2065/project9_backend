using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("site_users")]
    public class SiteUserDb
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email_as_login")]
        public string EmailAsLogin { get; set; }

        [Column("password")]
        public string Password{ get; set; }

        [Column("created")]
        public DateTime Created { get; set; }


        [Column("site_role_id")]
        public int SiteRoleId { get; set; }
    }
}
