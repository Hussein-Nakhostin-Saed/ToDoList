using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private CardItem _draggedItem; // تغییر نوع به CardItem

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ListBox listBox = sender as ListBox;
        _draggedItem = listBox.SelectedItem as CardItem; // ذخیره آیتم دیتا
        if (_draggedItem != null)
        {
            DragDrop.DoDragDrop(listBox, _draggedItem, DragDropEffects.Move);
        }
    }

    private void ListBox_Drop(object sender, DragEventArgs e)
    {
        ListBox sourceListBox = e.Source as ListBox;
        ListBox targetListBox = sender as ListBox;

        if (sourceListBox != targetListBox && _draggedItem != null)
        {
            var sourceItems = (ObservableCollection<CardItem>)sourceListBox.ItemsSource;
            var targetItems = (ObservableCollection<CardItem>)targetListBox.ItemsSource;

            // حذف و اضافه کردن با استفاده از آیتم دیتا
            sourceItems.Remove(_draggedItem);
            targetItems.Add(_draggedItem);
        }
        _draggedItem = null;
    }
}

public class CardItem
{
    public string Title { get; set; }

    public CardItem(string title)
    {
        Title = title;
    }

    public override string ToString()
    {
        return Title;
    }
}

public class MainWindowViewModel
{
    public ObservableCollection<string> Column1Items { get; set; } = new ObservableCollection<string> { "Card 1-1", "Card 1-2" };
    public ObservableCollection<string> Column2Items { get; set; } = new ObservableCollection<string> { "Card 2-1", "Card 2-2" };
    public ObservableCollection<string> Column3Items { get; set; } = new ObservableCollection<string> { "Card 3-1", "Card 3-2" };
}