using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace Hyperbar.UI.Windows;

public class StreamToImageSourceConverter :
    ValueConverter<byte[], ImageSource>
{
    protected override ImageSource? ConvertTo(byte[] value,
        Type? targetType, 
        object? parameter, 
        string? language)
    {
        if (value == null)
        {
            return default;
        }

        MemoryStream memoryStream = new(value);
        IRandomAccessStream randomAccessStream = memoryStream.AsRandomAccessStream();

        BitmapImage bitmapImage = new();
        bitmapImage.SetSource(randomAccessStream);
        return bitmapImage;
    }
}