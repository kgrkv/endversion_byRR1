using System.ComponentModel.DataAnnotations;

namespace TestDB2.DB
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserAge { get; set; }
        public string Coment;
        public string Gender { get; set; }


    }
}