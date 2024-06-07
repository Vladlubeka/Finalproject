using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp63
{
            public class Image : ICloneable
            {
                public string FilePath { get; set; }

                public Image(string filePath)
                {
                    FilePath = filePath;
                }

                public object Clone()
                {
                    return new Image(this.FilePath);
                }

                public void Display()
                {
                    Console.WriteLine($"Отображение изображения: {FilePath}");
                }
            }

            public abstract class ImageDecorator : Image
            {
                protected Image _image;

                public ImageDecorator(Image image) : base(image.FilePath)
                {
                    _image = image;
                }
            }

            public class GrayscaleDecorator : ImageDecorator
            {
                public GrayscaleDecorator(Image image) : base(image) { }

                public void ApplyGrayscale()
                {
                    Console.WriteLine($"Применение эффекта градаций серого к: {FilePath}");
                }
            }

            public class BorderDecorator : ImageDecorator
            {
                public BorderDecorator(Image image) : base(image) { }

                public void ApplyBorder()
                {
                    Console.WriteLine($"Применение рамки к: {FilePath}");
                }
            }

            public class ImageManager
            {
                private static ImageManager _instance;
                private static readonly object _lock = new object();

                public string Settings { get; private set; }

                private ImageManager()
                {
                    Settings = "Настройки по умолчанию";
                }

                public static ImageManager Instance
                {
                    get
                    {
                        if (_instance == null)
                        {
                            lock (_lock)
                            {
                                if (_instance == null)
                                {
                                    _instance = new ImageManager();
                                }
                            }
                        }
                        return _instance;
                    }
                }

                public void DisplaySettings()
                {
                    Console.WriteLine($"Текущие настройки: {Settings}");
                }

                public void UpdateSettings(string newSettings)
                {
                    Settings = newSettings;
                    Console.WriteLine($"Настройки обновлены: {Settings}");
                }
            }

            public class ImageFacade
            {
                public void ProcessImage(string filePath)
                {
                    ImageManager imageManager = ImageManager.Instance;
                    imageManager.DisplaySettings();

                    Image image = new Image(filePath);
                    Image clonedImage = (Image)image.Clone();
                    clonedImage.Display();

                    GrayscaleDecorator grayscaleImage = new GrayscaleDecorator(clonedImage);
                    grayscaleImage.ApplyGrayscale();

                    BorderDecorator borderedImage = new BorderDecorator(grayscaleImage);
                    borderedImage.ApplyBorder();
                }
            }

            class Program
            {
                static void Main()
                {
                    ImageFacade imageFacade = new ImageFacade();
                    imageFacade.ProcessImage(@"C:\Users\Влад Любека\Downloads\photo.jpg");
                }
            }
        }
