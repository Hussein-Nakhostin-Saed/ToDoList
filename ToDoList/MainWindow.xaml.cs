using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ToDoList.Domain.Entities;
using ToDoList.Services;

namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private TaskService _taskService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _taskService = serviceProvider.GetRequiredService<TaskService>();
        LoadTasks();
        _logger.Info($"Application started in {DateTime.Now}.");
    }

    private async void LoadTasks()
    {
        TaskListView.ItemsSource = await _taskService.GetAll();
    }

    private async void AddTask_Click(object sender, RoutedEventArgs e)
    {
        var task = new TaskItem
        {
            Title = TitleTextBox.Text,
            Description = DescriptionTextBox.Text,
            DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
            IsCompleted = IsCompleted.IsChecked ?? false
        };

        await _taskService.Insert(task);
        LoadTasks();

        SetElementsEmpty();

        _logger.Info($"{task.Title} Task Inserted");
    }

    private void TaskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TaskListView.SelectedItem != null)
        {
            TaskItem selectedTask = (TaskItem)TaskListView.SelectedItem;

            TitleTextBox.Text = selectedTask.Title;
            DescriptionTextBox.Text = selectedTask.Description;
            DueDatePicker.SelectedDate = selectedTask.DueDate;
            IsCompleted.IsChecked = selectedTask.IsCompleted;
        }
    }

    private async void EditTask_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListView.SelectedItem is TaskItem selectedTask)
        {
            selectedTask.Title = TitleTextBox.Text;
            selectedTask.Description = DescriptionTextBox.Text;
            selectedTask.DueDate = DueDatePicker.SelectedDate ?? DateTime.Now;
            selectedTask.IsCompleted = IsCompleted.IsChecked ?? false;

            await _taskService.Update(selectedTask);
            LoadTasks();

            SetElementsEmpty();

            TaskListView.SelectedItem = null;

            _logger.Info($"{selectedTask.Title} Task Edited");
        }
    }

    private async void DeleteTask_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListView.SelectedItem is TaskItem selectedTask)
        {
            await _taskService.Delete(selectedTask);
            LoadTasks();

            _logger.Info($"{selectedTask.Title} Task Deleted");
        }
    }

    private async void ExportToPdf_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await _taskService.ExportToPdf();
            MessageBox.Show("Pdf exported successfully!");
            _logger.Info($"Pdf Export in {DateTime.Now}");
        }
        catch (Exception)
        {
            _logger.Error($"Pdf Export Operation Failed in {DateTime.Now}");
            throw;
        }
    }

    public void SetElementsEmpty()
    {
        TitleTextBox.Text = "";
        DescriptionTextBox.Text = "";
        DueDatePicker.SelectedDate = null;
        IsCompleted.IsChecked = false;
    }
}

public class BoolToStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? "Done" : "To Do";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Brushes.LightGreen : Brushes.LightCoral;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}

public class TextToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return string.IsNullOrWhiteSpace(value?.ToString()) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
