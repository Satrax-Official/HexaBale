# Changelog

All notable changes to HexaBale will be documented in this file.

## [1.3.0] - 2026-05-01

### Added
- `GetChatAdministratorsAsync` - Get all administrators in a chat/channel
- `GetChatMembersCountAsync` - Get total member count of a chat
- `GetChatMembersAsync` - Paginated list of chat members
- `LeaveChatAsync` - Bot can leave a chat
- `replyToMessageId` parameter to all send methods (Photo, Video, Document, Audio, Voice, Animation, Location, Venue, Contact, Poll, Dice, MediaGroup)
- `disableNotification` parameter to all media send methods

## [1.2.0] - 2026-04-26

### Added
- `BleReplyKeyboardMarkup` - Custom reply keyboard that replaces user's keyboard
- `BleKeyboardButton` - Button model for reply keyboards
- `BleReplyKeyboardRemove` - Remove custom keyboard
- `BleForceReply` - Force user to reply to message
- `SendReplyKeyboardAsync` - Send message with reply keyboard
- `RemoveReplyKeyboardAsync` - Remove reply keyboard
- `ForceReplyAsync` - Force reply from user


## [1.1.0] - 2026-04-23

### Added
- `SendMediaGroupAsync` - Send albums (multiple photos/videos)
- `SendMediaGroupWithKeyboardAsync` - Send album with inline keyboard
- `BleInputMedia` - Input media model for albums
- Package icon for NuGet gallery


## [1.0.0] - 2026-04-22

### Added
- Initial release
- `HexaBaleClient` with all core methods
- Inline keyboard support
- Webhook and polling methods
- Callback query handling
- Edit, delete, forward, copy methods
- Core models and enums
- Exception hierarchy
- Extension methods
- Dependency injection support

