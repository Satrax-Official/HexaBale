using HexaBale.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaBale.Models
{ /// <summary>
  /// Represents a media file for sending in a media group
  /// </summary>
    public class BleInputMedia
    {
        /// <summary>
        /// Type of media: "photo" or "video"
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Media URL or file_id
        /// </summary>
        public string Media { get; set; } = string.Empty;

        /// <summary>
        /// Optional caption
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// Parse mode for caption
        /// </summary>
        public BleParseMode? ParseMode { get; set; }

        /// <summary>
        /// Creates a photo input media
        /// </summary>
        public static BleInputMedia Photo(string media, string? caption = null, BleParseMode? parseMode = null)
        {
            return new BleInputMedia
            {
                Type = "photo",
                Media = media,
                Caption = caption,
                ParseMode = parseMode
            };
        }

        /// <summary>
        /// Creates a video input media
        /// </summary>
        public static BleInputMedia Video(string media, string? caption = null, BleParseMode? parseMode = null)
        {
            return new BleInputMedia
            {
                Type = "video",
                Media = media,
                Caption = caption,
                ParseMode = parseMode
            };
        }
    }
}
