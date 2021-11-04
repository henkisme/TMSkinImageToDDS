	// _B --------------------- .dds BC1 ----------Basecolor / RGB
	// _R --------------------- .dds BC5 ----------Roughness Metallic (R=Roughness - G=Metalness) / RGB
	// _I --------------------- .dds BC3 ----------Self-illumination / RGB + Alpha channel
	// _N --------------------- .dds BC5 ----------Normal / RGB
	// _CoatR ----------------- .dds BC4 ----------Varnish layer / Grayscale
	// _DirtMask -------------- .dds BC4 ----------Dirt mask / Grayscale
	// _AO -------------------- .dds BC1 ----------Ambient Occlusion / Grayscale
using System;
using System.IO;
using System.IO.Compression;

namespace TrackmaniaSkinImageConverter
{
    class Converter
    {
        public Converter()
        {

        }

        public void Convert(String targetDirectory, String outputDirectory, String skinName)
        {
            //Get all files in directory
            string [] fileEntries = Directory.GetFiles(targetDirectory);
            foreach(string fileName in fileEntries)
            {
                ImageForConverting image = new ImageForConverting(fileName);
                image.Convert(outputDirectory, skinName);
            }
            ZipFile.CreateFromDirectory(outputDirectory + "\\" + skinName, outputDirectory + "\\" + skinName + ".zip");
            Directory.Delete(outputDirectory + "\\" + skinName, true);
        }
    }

}
