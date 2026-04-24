using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HexaBale.Models
{
    /// <summary>
    /// Represents a chat invite link.
    /// </summary>
    public class BleChatInviteLink
    {
        /// <summary>
        /// The invite link URL.
        /// </summary>
        [JsonPropertyName("invite_link")]
        public string? InviteLink { get; set; }

        /// <summary>
        /// Creator of the invite link.
        /// </summary>
        [JsonPropertyName("creator")]
        public BleUser? Creator { get; set; }

        /// <summary>
        /// True if users joining via this link need to be approved by chat admins.
        /// </summary>
        [JsonPropertyName("creates_join_request")]
        public bool? CreatesJoinRequest { get; set; }

        /// <summary>
        /// True if this is the primary invite link for the chat.
        /// </summary>
        [JsonPropertyName("is_primary")]
        public bool? IsPrimary { get; set; }

        /// <summary>
        /// True if the invite link has been revoked.
        /// </summary>
        [JsonPropertyName("is_revoked")]
        public bool? IsRevoked { get; set; }

        /// <summary>
        /// Name of the invite link.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Expiration date of the invite link as Unix timestamp.
        /// </summary>
        [JsonPropertyName("expire_date")]
        public int? ExpireDate { get; set; }

        /// <summary>
        /// Maximum number of users that can join using this link.
        /// </summary>
        [JsonPropertyName("member_limit")]
        public int? MemberLimit { get; set; }

        /// <summary>
        /// Number of pending join requests for this link.
        /// </summary>
        [JsonPropertyName("pending_join_request_count")]
        public int? PendingJoinRequestCount { get; set; }
    }
}