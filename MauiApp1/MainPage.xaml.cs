using CommunityToolkit.Mvvm.Messaging;

namespace MauiApp1;

public record Order_1(string message);
public partial class MainPage : ContentPage
{
	int count = 0;
	private readonly IMessenger _messenger;

    public MainPage(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		_messenger.Send(new Order_1("1"));

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private void OnCounterClicked2(object sender, EventArgs e)
	{
		_messenger.Send(new Order_1("2"));

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

