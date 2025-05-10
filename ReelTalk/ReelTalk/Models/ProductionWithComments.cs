using ReelTalk.Data;

namespace ReelTalk.Models
{
    public class ProductionWithComments
    {
        public Production Production { get; set; }
        public List<Comment> Comments { get; set; }

        public Comment Comment { get; set; }

        public ProductionWithComments(Production production, List<Comment> comments)
        {
            Production = production;
            Comments = comments;
        }
    }
}
