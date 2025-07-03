namespace FRONTEND.Data
{
    public class Album
    {
        #region PROPS
        public string? Name { get; set; }
        public string? Artist {get; set;}
        public string? PrimaryColour { get; set; }
        public float Rating { get; set; }
        public int Release { get; set; }
        public float Length { get; set; }
        public string? ImgSrc { get; set; }
        public string? ImgSrcBack { get; set; }
        public List<Track>? TrackList { get; set; }
        #endregion
    
        #region METHODS
        public string GetStars(double rating)
        {
            int fullStars = (int)rating;
            bool halfStar = rating - fullStars >= 0.5;
            int emptyStars = 5 - fullStars - (halfStar ? 1 : 0);

            return new string('★', fullStars) +
               (halfStar ? "½" : "") +
               new string('☆', emptyStars);
        }
        #endregion
    }
}
