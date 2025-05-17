using SetMinerStaticIpCK.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;

namespace SetMinerStaticIpCK.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Person> People { get; }
    public ObservableCollection<MinerIpConfig> MinerIpList { get; }

    public MainWindowViewModel()
    {
        var people = new List<Person>
        {
            new Person("Neil", "Armstrong"),
            new Person("Buzz", "Li year"),
            new Person("James", "Kirk")
        };
        People = new ObservableCollection<Person>(people);
        var minerIpList = new List<MinerIpConfig>
        {
            new MinerIpConfig(1, @"10.11.10.1", "mac", @"10.11.1.1", "wait", "S19")
        };
        MinerIpList = new ObservableCollection<MinerIpConfig>(minerIpList);
    }

    private string _log = "等待监听...\n";

    public string Log
    {
        get => _log;
        set => this.RaiseAndSetIfChanged(ref _log, value);
    }

    private void AppendLog(string text)
    {
        Log += $"{DateTime.Now:T} - {text}\n";
    }

    private void StartListening()
    {
        Task.Run(async () =>
        {
            using var udpClient = new UdpClient(14235);
            AppendLog("UDP 监听已启动，端口: 14235");

            while (true)
            {
                try
                {
                    var result = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer);
                    AppendLog($"收到来自 {result.RemoteEndPoint}: {message}");
                }
                catch (Exception ex)
                {
                    AppendLog($"错误: {ex.Message}");
                }
            }
        });
    }
}