using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NetSparkleUpdater;
using NetSparkleUpdater.AppCastHandlers;
using NetSparkleUpdater.Downloaders;


namespace NetSparkleUpdaterApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
        CheckForUpdates();
    }

    private async void CheckForUpdates() {
        string appcastUrl = "https://jlarapr.github.io/NetSparkleUpdaterApp/appcast.xml";

        var sparkle = new SparkleUpdater(appcastUrl, null);


        var updateInfo = await sparkle.CheckForUpdatesQuietly();
        if (updateInfo.Status == NetSparkleUpdater.Enums.UpdateStatus.UpdateAvailable)
        {
            sparkle.ShowUpdateNeededUI(updateInfo.Updates, true);

        }
    }
}