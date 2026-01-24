using System.Windows;
using G00DS0ULRPG.ViewModel;

namespace WPFUI
{
    public partial class CharacterCreationScreen_ : Window
    {
        private CharacterCreationViewModel VM { get; set; }

        public CharacterCreationScreen_()
        {
            InitializeComponent();

            VM = new CharacterCreationViewModel();

            DataContext = VM;
        }

        private void RandomPlayer_OnCLick(object sender, RoutedEventArgs e)
        {
            VM.RollNewCharacter();
        }

        private void UseThisPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(VM.GetPlayer());
            mainWindow.Show();
            Close();
        }

        private void Race_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            VM.ApplyAttributeModifiers();
        }
    }
}
