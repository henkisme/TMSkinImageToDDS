using System;
using System.IO;
using BCnEncoder.Encoder;
using BCnEncoder.ImageSharp;
using BCnEncoder.Shared;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


namespace TrackmaniaSkinImageConverter
{
    class ImageForConverting
    {
        private string fileName;
        private string filePath;
        private CompressionFormat compressionFormat;
        public ImageForConverting(string path)
        {
            filePath = path;

            //Get filename from filepath
            var fileNameSplitted = filePath.Split('\\');
            var name = fileNameSplitted[fileNameSplitted.Length - 1];
            var nameParts = name.Split('.');
            //-1 because length to 0 index array and -1 because last part in array is file extention
            fileName = nameParts[nameParts.Length - 2];
            //Choose compression format based on filename
            compressionFormat = ChooseCompressionFormat();
        }
        public void Convert(String outputDirectory, String skinName)
        {
            if (compressionFormat != CompressionFormat.Unknown)
            {
                using Image<Rgba32> image = Image.Load<Rgba32>(filePath);

                BcEncoder encoder = new BcEncoder();

                encoder.OutputOptions.GenerateMipMaps = true;
                encoder.OutputOptions.Quality = CompressionQuality.BestQuality;
                encoder.OutputOptions.Format = compressionFormat;
                encoder.OutputOptions.FileFormat = OutputFileFormat.Dds;

                Directory.CreateDirectory(outputDirectory + "\\" + skinName);
                using FileStream fs = File.OpenWrite(outputDirectory + "\\" + skinName + "\\" + fileName + ".dds");
                encoder.EncodeToStream(image, fs);
            }

        }

        public CompressionFormat ChooseCompressionFormat()
        {
            if (fileName.EndsWith("_B") || fileName.EndsWith("_AO"))
            {
                return CompressionFormat.Bc1;
            }
            else if (fileName.EndsWith("_I"))
            {
                return CompressionFormat.Bc3;
            }
            else if (fileName.EndsWith("_CoatR") || fileName.EndsWith("_DirtMask"))
            {
                return CompressionFormat.Bc4;
            }
            else if (fileName.EndsWith("_R") || fileName.EndsWith("_N"))
            {
                return CompressionFormat.Bc5;
            }
            return CompressionFormat.Unknown;
        }
    }
}
