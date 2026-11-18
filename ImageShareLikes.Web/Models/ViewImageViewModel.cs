using ImageShareLikes.Data;

namespace ImageShareLikes.Web.Models
{
    public class ViewImageViewModel
    {
        public Image Image { get; set; }
        public List<int> LikedAlready { get; set; }
    }
}
