using AspWebsite.Models;

namespace AspWebsite.Datas
{
    public class TodoModel : MongoModel
    {
        public string UserId { get; set; } = "";
        public string Title { get; set; } = "";
        public DateTime Due { get; set; }
        public bool IsImportant { get; set; }
    }
}
