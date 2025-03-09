using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Domain.Dtos;
using ToDoList.Services;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ObservableCollection<Card> ToDoCards;
    private ObservableCollection<Card> InProgressCards;
    private ObservableCollection<Card> DoneCards;
    //private readonly TaskService _taskService;

    public MainWindow(/*TaskService taskService*/)
    {
        InitializeComponent();
        //_taskService = taskService;
        InitializeData();
    }

    //private async void InitializeData()
    //{
    //    // مقداردهی اولیه کارت‌ها
    //    ToDoCards = await _taskService.GetAllToDoCards();
    //    InProgressCards = await _taskService.GetAllInProgressCards();
    //    DoneCards = await _taskService.GetAllDoneCards();

    //    ToDoList.ItemsSource = ToDoCards;
    //    InProgressList.ItemsSource = InProgressCards;
    //    DoneList.ItemsSource = DoneCards;
    //}

    private void InitializeData()
    {
        // مقداردهی اولیه با اطلاعات بیشتر
        ToDoCards = new ObservableCollection<Card>
        {
            new Card { Title = "Task 1", Description = "بررسی باگ در بخش ورود" },
            new Card { Title = "Task 2", Description = "طراحی رابط کاربری جدید" },
            new Card { Title = "Task 3", Description = "بهینه‌سازی دیتابیس" }
        };

        InProgressCards = new ObservableCollection<Card>
        {
            new Card { Title = "Task 4", Description = "تست واحد برای API" }
        };

        DoneCards = new ObservableCollection<Card>
        {
            new Card { Title = "Task 5", Description = "رفع مشکل امنیتی" }
        };

        ToDoList.ItemsSource = ToDoCards;
        InProgressList.ItemsSource = InProgressCards;
        DoneList.ItemsSource = DoneCards;
    }

    private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ListBox listBox = sender as ListBox;
        if (listBox != null)
        {
            var item = GetItemUnderMouse(listBox, e.GetPosition(listBox));
            if (item != null)
            {
                DragDrop.DoDragDrop(listBox, item, DragDropEffects.Move);
            }
        }
    }

    private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        Border border = sender as Border;
        if (border != null)
        {
            Card card = border.DataContext as Card;
            if (card != null)
            {
                // باز کردن پنجره جزییات
                CardDetailsWindow detailsWindow = new CardDetailsWindow(card);
                detailsWindow.ShowDialog(); // نمایش پنجره به صورت مودال
            }
        }
    }

    private void ListBox_Drop(object sender, DragEventArgs e)
    {
        ListBox targetListBox = sender as ListBox;
        if (e.Data.GetDataPresent(typeof(Card)))
        {
            Card card = e.Data.GetData(typeof(Card)) as Card;

            // حذف کارت از لیست مبدا
            if (ToDoCards.Contains(card)) ToDoCards.Remove(card);
            else if (InProgressCards.Contains(card)) InProgressCards.Remove(card);
            else if (DoneCards.Contains(card)) DoneCards.Remove(card);

            // اضافه کردن به لیست مقصد
            if (targetListBox == ToDoList) ToDoCards.Add(card);
            else if (targetListBox == InProgressList) InProgressCards.Add(card);
            else if (targetListBox == DoneList) DoneCards.Add(card);
        }
    }

    private Card GetItemUnderMouse(ListBox listBox, Point position)
    {
        var hitTestResult = System.Windows.Media.VisualTreeHelper.HitTest(listBox, position);
        if (hitTestResult != null)
        {
            var listBoxItem = FindVisualParent<ListBoxItem>(hitTestResult.VisualHit);
            if (listBoxItem != null)
            {
                return listBoxItem.DataContext as Card;
            }
        }
        return null;
    }

    private T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
    {
        DependencyObject parent = System.Windows.Media.VisualTreeHelper.GetParent(child);
        if (parent == null) return null;
        if (parent is T) return parent as T;
        return FindVisualParent<T>(parent);
    }
}