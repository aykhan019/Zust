namespace Zust.Web.Entities
{
    /// <summary>
    /// Represents an Advertisement entity with its properties.
    /// </summary>
    public class Advertisement
    {
        /// <summary>
        /// Gets or sets the URL associated with the Advertisement.
        /// </summary>
        public string? AdvertisementUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image used for the Advertisement.
        /// </summary>
        public string? AdvertisementImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the logo used in the Advertisement.
        /// </summary>
        public string? LogoUrl { get; set; }
    }
}
