using System.Windows;
using ToDoList.Domain.Dtos;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for CardDetailsWindow.xaml
    /// </summary>
    public partial class CardDetailsWindow : Window
    {
        public CardDetailsWindow(Card card)
        {
            InitializeComponent();
            this.DataContext = card; 
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
