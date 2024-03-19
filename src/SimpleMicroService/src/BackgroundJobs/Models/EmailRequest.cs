namespace BackgroundJobs.Models;

public class EmailRequest
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public DateTime? SendDate { get; set; }
    public bool SendNow { get; set; } = true;
}
