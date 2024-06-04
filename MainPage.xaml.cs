namespace ToolbarItemLeak;

public partial class MainPage : ContentPage
{
	WeakReference<Page> pageReference;
    public MainPage()
    {
        InitializeComponent();
    }

    private void ShowPageWithLeak(object sender, EventArgs e)
    {
		var leakPage = new LeakPage();
		pageReference = new WeakReference<Page>(leakPage);
		Navigation.PushAsync(leakPage);
    }
	void CheckLeak(object sender, EventArgs e)
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		GC.Collect();
		if (pageReference.TryGetTarget(out var page))
		{
			DisplayAlert("Leak", "LeakPage is still alive", "OK");
		}
		else
		{
			DisplayAlert("No Leak", "LeakPage is gone", "OK");
		}
	}
}

