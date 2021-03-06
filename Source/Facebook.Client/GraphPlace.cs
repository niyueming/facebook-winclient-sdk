﻿namespace Facebook.Client
{
    using System;
    using System.Collections.Generic;
    using Facebook.Client.Controls;

    /// <summary>
    /// Provides a strongly-typed representation of a Facebook Place as defined by the Graph API.
    /// </summary>
    /// <remarks>
    /// The GraphPlace class represents the most commonly used properties of a Facebook Place object.
    /// </remarks>
    public class GraphPlace : GraphObject
    {
        private Uri pictureUrl;

        /// <summary>
        /// Initializes a new instance of the GraphPlace class.
        /// </summary>
        public GraphPlace()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphPlace class from a dynamic object returned by the Facebook API.
        /// </summary>
        /// <param name="place">The dynamic object representing the Facebook place.</param>
        public GraphPlace(dynamic place)
            : base((IDictionary<string, object>)place)
        {
            if (place == null)
            {
                throw new ArgumentNullException("place");
            }

            this.Id = place.id;
            this.Name = place.name;
            dynamic location = place.location;
            this.Location = (location != null) ? new GraphLocation(location) : null;
            var picture = place.picture;
            if (picture != null)
            {
                Uri.TryCreate(picture.data.url, UriKind.Absolute, out this.pictureUrl);
            }
        }

        /// <summary>
        /// Gets or sets the ID of the place.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the place.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location of the place.
        /// </summary>
        public GraphLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the URL of the place's profile picture.
        /// </summary>
        public Uri ProfilePictureUrl
        {
            get
            {
                if (this.pictureUrl == null)
                {
                    this.pictureUrl = new Uri(ProfilePicture.GetBlankProfilePictureUrl(true), UriKind.RelativeOrAbsolute);
                }

                return this.pictureUrl;
            }

            set
            {
                this.pictureUrl = value;
            }
        }
    }
}
