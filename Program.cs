using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace toABMP
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<byte> bytes = new List<byte>();
            List<char> RLE = new List<char>();
            Bitmap image1 = new Bitmap(@args[0]);
            

            for (int i = 0; i < image1.Width; i++)
            {
                for (int j = 0; j < image1.Height; j++)
                {
                    Color pixel = image1.GetPixel(i, j);
                    bytes.Add(Compare(pixel));
                }
            }
            int bc = 0;
            byte lastbyte = bytes[0];
            int i2 = 0;
            foreach (byte b in bytes)
            {
                i2++;
                if (b == lastbyte)
                {
                   if (image1.Width -i2==0)
                    {
                        RLE.Add(',');
                        RLE.Add('{');
                        RLE.AddRange(bc.ToString().ToCharArray());
                        RLE.Add(',');
                        RLE.AddRange(lastbyte.ToString().ToCharArray());
                        RLE.Add('}');
                        lastbyte = b;
                        bc = 1;

                    }
                    else
                    {
                        bc++;
                    }
                }
                else
                {
                    RLE.Add(',');
                    RLE.Add('{');
                    RLE.AddRange(bc.ToString().ToCharArray());
                    RLE.Add(',');
                    RLE.AddRange(lastbyte.ToString().ToCharArray());
                    RLE.Add('}');
                    lastbyte = b;
                    bc = 1;

                }
                if (i2>=bytes.Count) {

                    RLE.Add(',');
                    RLE.Add('{');
                    RLE.AddRange(bc.ToString().ToCharArray());
                    RLE.Add(',');
                    RLE.AddRange(lastbyte.ToString().ToCharArray());
                    RLE.Add('}');
                    RLE.Add('}');
                }
            }
            RLE.InsertRange(0, image1.Height.ToString().ToCharArray());
            RLE.Insert(0, ',');
            RLE.InsertRange(0, image1.Width.ToString().ToCharArray());
            RLE.Insert(0,'{');
            
            Console.WriteLine(new string(RLE.ToArray()));
            Console.ReadKey();
        }
         static byte Compare(Color c) {

            int bluesim;
            int redsim;
            int magsim;
            int greensim;
            int cyansim;
            int yellowsim;
            int blacksim;
            Color blue = Color.FromArgb(99, 101, 255);
            Color red = Color.FromArgb(255, 48, 49);
            Color magenta = Color.FromArgb(255, 48, 222);
            Color green = Color.FromArgb(0, 207, 0);
            Color cyan = Color.FromArgb(0, 255, 255);
            Color yellow = Color.FromArgb(255, 255, 0);
            Color black = Color.FromArgb(0, 0, 0);
            bluesim = Math.Abs((blue.R - c.R) + (blue.G - c.G) + (blue.B - c.G));
            redsim = Math.Abs((red.R - c.R) + (red.G - c.G) + (red.B - c.G));
            magsim = Math.Abs((magenta.R - c.R) + (magenta.G - c.G) + (magenta.B - c.G));
            greensim = Math.Abs((green.R - c.R) + (green.G - c.G) + (green.B - c.G));
            cyansim = Math.Abs((cyan.R - c.R) + (cyan.G - c.G) + (cyan.B - c.G));
            yellowsim = Math.Abs((yellow.R - c.R) + (yellow.G - c.G) + (yellow.B - c.G));
            blacksim = Math.Abs((black.R - c.R) + (black.G - c.G) + (black.B - c.G));

            List<int> sims = new List<int> { bluesim ,redsim, magsim, greensim ,cyansim, yellowsim ,blacksim };
            int smalllol = 3000;
            foreach(int i in sims){
                if (smalllol>i) {
                    smalllol = i;
                }
            }
            if (smalllol == bluesim) {
                return 2;
            }
            if (smalllol == redsim)
            {
                return 3;
            }
            if (smalllol == magsim)
            {
                return 4;
            }
            if (smalllol == greensim)
            {
                return 5;
            }
            if (smalllol == cyansim)
            {
                return 6;
            }
            if (smalllol == yellowsim)
            {
                return 7;
            }
            if (smalllol == blacksim)
            {
                return 1;
            }
            else { return 8; }



        }
    }
}
