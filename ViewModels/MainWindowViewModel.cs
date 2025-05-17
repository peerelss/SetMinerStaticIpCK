using SetMinerStaticIpCK.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
}