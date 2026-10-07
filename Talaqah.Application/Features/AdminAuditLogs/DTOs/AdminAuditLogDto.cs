namespace Talaqah.Application.Features.AdminAuditLogs.DTOs;

public class AdminAuditLogDto
{
    public int Id { get; set; }

    // Admin information
    public int AdminId { get; set; }
    public string AdminName { get; set; } = string.Empty;
     
    // Action information
    public string ActionType { get; set; } = string.Empty;

    // Target entity
    public string TableName { get; set; } = string.Empty;
    public int? RecordId { get; set; }

     // Changes
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

     // Request information
    public string? IpAddress { get; set; }

     // Time
    public DateTime Timestamp { get; set; }
}