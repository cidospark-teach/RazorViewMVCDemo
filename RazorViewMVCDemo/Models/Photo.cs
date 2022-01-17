using System;

namespace RazorViewMVCDemo.Models
{
    public class Photo
    {
        public string PhotoId { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
    }
}