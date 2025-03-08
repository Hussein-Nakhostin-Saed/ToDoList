using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace ToDoList;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Border _draggedCard;
    private AdornerLayer _adornerLayer;
    private CardAdorner _cardAdorner;
    private Point _startPoint;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _draggedCard = sender as Border;
        _startPoint = e.GetPosition(this);
        _adornerLayer = AdornerLayer.GetAdornerLayer(this);
        _cardAdorner = new CardAdorner(_draggedCard, _startPoint);
        _adornerLayer.Add(_cardAdorner);
        CaptureMouse();
        e.Handled = true;
    }

    private void MainWindow_MouseMove(object sender, MouseEventArgs e)
    {
        if (_draggedCard != null && IsMouseCaptured)
        {
            Point currentPoint = e.GetPosition(this);
            Vector offset = currentPoint - _startPoint;
            _cardAdorner.UpdatePosition(offset);
        }
    }

    private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (_draggedCard != null && IsMouseCaptured)
        {
            ReleaseMouseCapture();
            _adornerLayer.Remove(_cardAdorner);
            _cardAdorner = null;
            _draggedCard = null;
        }
    }

    private void DropTarget_Drop(object sender, DragEventArgs e)
    {
        if (_draggedCard != null)
        {
            //DropTarget.Children.Add(_draggedCard);
            _draggedCard = null;
        }
    }
}

public class CardAdorner : Adorner
{
    private Border _card;
    private Point _adornerStartPoint;
    private TranslateTransform _translateTransform;

    public CardAdorner(Border adornedElement, Point startPoint) : base(adornedElement)
    {
        _card = adornedElement;
        _adornerStartPoint = startPoint;
        _translateTransform = new TranslateTransform();
        this.RenderTransform = _translateTransform;
        this.IsHitTestVisible = false;
    }

    public void UpdatePosition(Vector offset)
    {
        _translateTransform.X = offset.X;
        _translateTransform.Y = offset.Y;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        drawingContext.DrawRectangle(new VisualBrush(_card), null, new Rect(0, 0, _card.ActualWidth, _card.ActualHeight));
    }
}