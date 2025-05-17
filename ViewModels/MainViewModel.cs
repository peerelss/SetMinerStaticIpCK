namespace SetMinerStaticIpCK.ViewModels;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
public class MainViewModel: INotifyPropertyChanged
{ private string _statusMessage = "等待中...";
    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged(nameof(StatusMessage));
        }
    }

    private bool _isListening = false;
    private CancellationTokenSource? _cts;

    public async void StartListening()
    {
        if (_isListening)
        {
            StatusMessage = "已经在监听中...";
            return;
        }

        _isListening = true;
        StatusMessage = "开始监听 UDP 14235...";
        _cts = new CancellationTokenSource();

        await Task.Run(() => ListenUdp(_cts.Token));
    }

    private void ListenUdp(CancellationToken token)
    {
        using var udp = new UdpClient(14235);
        var endpoint = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            while (!token.IsCancellationRequested)
            {
                var result = udp.Receive(ref endpoint);
                string msg = Encoding.UTF8.GetString(result);
                StatusMessage = $"来自 {endpoint}: {msg}";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"监听出错: {ex.Message}";
        }
        finally
        {
            _isListening = false;
        }
    }

    public void StopListening()
    {
        _cts?.Cancel();
        StatusMessage = "已停止监听";
        _isListening = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}