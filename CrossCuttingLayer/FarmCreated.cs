namespace CrossCuttingLayer;

public class FarmCreated
{
    public string Id { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public string TaskDescription { get; set; }
    public bool IsCompleted { get; set; }
}