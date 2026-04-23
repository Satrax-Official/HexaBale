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
        [JsonPropertyName("invite_link")]
        public string? InviteLink { get; set; }

        [JsonPropertyName("creator")]
        public BleUser? Creator { get; set; }

        [JsonPropertyName("creates_join_request")]
        public bool? CreatesJoinRequest { get; set; }

        [JsonPropertyName("is_primary")]
        public bool? IsPrimary { get; set; }

        [JsonPropertyName("is_revoked")]
        public bool? IsRevoked { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("expire_date")]
        public int? ExpireDate { get; set; }

        [JsonPropertyName("member_limit")]
        public int? MemberLimit { get; set; }

        [JsonPropertyName("pending_join_request_count")]
        public int? PendingJoinRequestCount { get; set; }
    }
}
