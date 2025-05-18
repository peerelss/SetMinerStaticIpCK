namespace SetMinerStaticIpCK.Models;

using System.ComponentModel;

public class MinerIpConfig : INotifyPropertyChanged
{
    private int _lineNo;

    public int LineNo
    {
        get => _lineNo;
        set
        {
            if (_lineNo != value)
            {
                _lineNo = value;
                OnPropertyChanged(nameof(LineNo));
            }
        }
    }

    private string _currentIp;

    public string CurrentIp
    {
        get => _currentIp;
        set
        {
            if (_currentIp != value)
            {
                _currentIp = value;
                OnPropertyChanged(nameof(CurrentIp));
            }
        }
    }

    private string _maCAddress;

    public string MaCAddress
    {
        get => _maCAddress;
        set
        {
            if (_maCAddress != value)
            {
                _maCAddress = value;
                OnPropertyChanged(nameof(MaCAddress));
            }
        }
    }

    private string _targetIp;

    public string TargetIp
    {
        get => _targetIp;
        set
        {
            if (_targetIp != value)
            {
                _targetIp = value;
                OnPropertyChanged(nameof(TargetIp));
            }
        }
    }

    private string _setStatus;

    public string SetStatus
    {
        get => _setStatus;
        set
        {
            if (_setStatus != value)
            {
                _setStatus = value;
                OnPropertyChanged(nameof(SetStatus));
            }
        }
    }

    private string _minerMode;

    public string MinerMode
    {
        get => _minerMode;
        set
        {
            if (_minerMode != value)
            {
                _minerMode = value;
                OnPropertyChanged(nameof(MinerMode));
            }
        }
    }

    public MinerIpConfig(int lineNo, string currentIp, string maCAddress, string targetIp, string setStatus,
        string minerMode)
    {
        _lineNo = lineNo;
        _currentIp = currentIp;
        _maCAddress = maCAddress;
        _targetIp = targetIp;
        _setStatus = setStatus;
        _minerMode = minerMode;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}