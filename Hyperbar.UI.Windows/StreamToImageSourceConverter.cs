using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace Hyperbar.UI.Windows;

public class StreamToImageSourceConverter :
    ValueConverter<Stream, ImageSource>
{
    protected override ImageSource? ConvertTo(Stream value,
        Type? targetType, 
        object? parameter, 
        string? language)
    {
        if (value == null)
        {
            return default;
        }

        IRandomAccessStream randomAccessStream = 
            WindowsRuntimeStreamExtensions.AsRandomAccessStream(value);

        BitmapImage bitmapImage = new();
        bitmapImage.SetSource(randomAccessStream);
        return bitmapImage;
    }
}