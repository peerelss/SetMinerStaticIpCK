namespace SetMinerStaticIpCK.Models;

public class MinerIpConfig(
    int lineNo,
    string currentIp,
    string maCAddress,
    string targetIp,
    string setStatus,
    string minerMode)
{
    public int LineNo { get; set; } = lineNo;
    public string CurrentIp { get; set; } = currentIp;
    public string MaCAddress { get; set; } = maCAddress;
    public string TargetIp { get; set; } = targetIp;
    public string SetStatus { get; set; } = setStatus;
    public string MinerMode { get; set; } = minerMode;
}