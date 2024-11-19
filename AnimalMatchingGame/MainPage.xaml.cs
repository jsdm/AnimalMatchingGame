namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;
            List<string> animalEmoji = [
                "🐶", "🐶",
                "🐱", "🐱",
                "🐸", "🐸",
                "🐹", "🐹",
                "🐔", "🐔",
                "🐥", "🐥",
                "🐻", "🐻",
                "🐼", "🐼"];
            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }
        int tenthOfSecondsElapsed = 0;
        private bool TimerTick()
        {
            if(!this.IsLoaded) return false;
            tenthOfSecondsElapsed++;
            TimeElapsed.Text = "Time elapsed: " + (tenthOfSecondsElapsed / 10F).ToString("0.0s");

            if(PlayAgainButton.IsVisible)
            {
                tenthOfSecondsElapsed = 0;
                return false;
            }
            return true;
        }

        Button lastClicked;
        bool findingMatch = false;
        int matchesFound;
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button_Clicked)
            {
                if (!string.IsNullOrWhiteSpace(button_Clicked.Text) && (findingMatch == false))
                {
                    button_Clicked.BackgroundColor = Colors.Red;
                    lastClicked = button_Clicked;
                    findingMatch = true;
                }
                else
                {
                    if ((button_Clicked != lastClicked) && (button_Clicked.Text == lastClicked.Text) && (!String.IsNullOrWhiteSpace(button_Clicked.Text)))
                    {
                        lastClicked.BackgroundColor = Colors.Green;
                        button_Clicked.BackgroundColor = Colors.Green;
                        await Task.Delay(300);
                        matchesFound++;
                        lastClicked.Text = " ";
                        button_Clicked.Text = " ";
                    }
                    else {
                        button_Clicked.BackgroundColor = Colors.Red;
                        await Task.Delay(300);
                    }
                    lastClicked.BackgroundColor = Colors.LightBlue;
                    button_Clicked.BackgroundColor = Colors.LightBlue;
                    findingMatch = false;
                }
            }
            if (matchesFound == 8)
            {
                AnimalButtons.IsVisible = false;
                PlayAgainButton.IsVisible = true;
                matchesFound = 0;
            }
        }
    }

}
