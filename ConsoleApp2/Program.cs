using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp2 {
    class PPMEdytor {
        #region fields
        const int RED = 1;
        const int GREEN = 2;
        const int BLUE = 3;
        const int negate = 1;
        const int setTo0 = 2;
        const int contrast = 3;
        const int toGrey = 4;
        static int nCol { get; set; };
        static int nRow { get; set; }
        static int CodingAccuracy { get; set; }
        //   static int[,,] iA { get; set; }
        static String fileType { get; set; }
        #endregion
        static String inputFile = "i100.ppm";
        //   static String inputFile = "wejscie2.ppm";
        static String outputFile = "output.ppm";
        static StreamReader sr = new StreamReader(inputFile);
        static StreamWriter sw = new StreamWriter(outputFile);
        static StreamWriter sw2 = new StreamWriter("tempFile.txt");

        static void Main(string[] args)
        {
            int color = 1; // domyslnie czerwony ( np. do kontrastu, gdzie nie trzeba wybierac koloru)
            Initialization(sr, sw);


            ChangeImage(sr, sw, 1, 1);


            //Closing streams
            sr.Close();
            sw.Close();
            Console.WriteLine("Program ends.");
            Console.ReadLine();
        }


        static void ChangeImage(StreamReader sr, StreamWriter sw, int color, int operation)
        {
            String[] bArr;
            int counter = 1;
            // test
            if (inputFile.Equals("wejscie2.ppm"))
            {
                nRow = 9;
                nCol = 9;
            }
            switch (operation)
            {
                case 1:
                    {
                        for (int i = 0; i < nRow; i++)
                        {
                            String line = sr.ReadLine();
                            Console.WriteLine(line.Length);
                            line.Replace("\r\n", "");
                            bArr = line.Split(' ');
                            for (int j = 0; j < nCol * 3; j++)
                            {
                                if (counter == color)
                                {
                                    int value = CodingAccuracy - Int32.Parse(bArr[j]);
                                    bArr[j] = Convert.ToString(value);
                                }
                                if (counter == 3)
                                    counter = 0;
                                counter++;
                            }
                            sw.Write(string.Join(" ", bArr) + "\n");
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < nRow; i++)
                        {
                            String line = sr.ReadLine();
                            line.Replace("\r\n", "");
                            bArr = line.Split(' ');
                            for (int j = 0; j < nCol * 3; j++)
                            {
                                if (counter == color)
                                {
                                    int value = 0;
                                    bArr[j] = Convert.ToString(value);
                                }
                                if (counter == 3)
                                    counter = 0;
                                counter++;
                            }
                            counter = 1;
                            sw.Write(string.Join(" ", bArr) + "\n");
                        }
                        break;
                    }
                case 3:
                    {
                        int r = 0, g = 0, b = 0, srednia;
                        for (int i = 0; i < nRow; i++)
                        {
                            String line = sr.ReadLine();
                            line.Replace("\r\n", "");
                            bArr = line.Split(' ');

                            for (int j = 0; j < nCol * 3; j++)
                            {
                                switch (counter)
                                {
                                    case 1:
                                        {
                                            r = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                    case 2:
                                        {
                                            g = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                    case 3:
                                        {
                                            b = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                }
                                if (counter == 3) // jesli doliczy do 3ciego koloru
                                {
                                    counter = 0;
                                //    srednia = (r + g + b) / 3;
                                    srednia = 127; // WARTOSC SREDNIA KONTRASTU
                                    if (Convert.ToInt32(bArr[j - 2]) >= srednia)
                                        bArr[j - 2] = Convert.ToString(255);
                                    else
                                        bArr[j - 2] = Convert.ToString(0);

                                    if (Convert.ToInt32(bArr[j - 1]) >= srednia)
                                        bArr[j - 1] = Convert.ToString(255);
                                    else
                                        bArr[j] = Convert.ToString(0);

                                    if (Convert.ToInt32(bArr[j]) >= srednia)
                                        bArr[j] = Convert.ToString(255);
                                    else
                                        bArr[j] = Convert.ToString(0);

                                }
                                counter++;
                            }
                            counter = 1; // reset counter

                            // ustaw srednie kolorow  - JESLI ROZPATRYWANE JEST GLOBALNIE JAKO KOLORY
                            //sR = sR / (nCol * nRow);
                            //sG = sG / (nCol * nRow);
                            //sB = sB / (nCol * nRow);

                            sw.Write(string.Join(" ", bArr) + "\n");
                        }
                        break;
                    }
                case 4:
                    {
                        int r = 0, g = 0, b = 0, srednia;
                        for (int i = 0; i < nRow; i++)
                        {
                            String line = sr.ReadLine();
                            line.Replace("\r\n", "");
                            bArr = line.Split(' ');

                            for (int j = 0; j < nCol * 3; j++)
                            {
                                switch (counter)
                                {
                                    case 1:
                                        {
                                            r = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                    case 2:
                                        {
                                            g = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                    case 3:
                                        {
                                            b = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                }
                                if (counter == 3) // jesli doliczy do 3ciego koloru
                                {
                                    srednia = (r + g + b) / 3;
                                    counter = 0;
                                    bArr[j - 2] = Convert.ToString(srednia);
                                    bArr[j - 1] = Convert.ToString(srednia);
                                    bArr[j] = Convert.ToString(srednia);
                                }
                                counter++;
                            }
                            counter = 1; // reset counter
                            sw.Write(string.Join(" ", bArr) + "\n");
                        }
                        break;
                    }
                case 6:
                    {
                        int r = 0, g = 0, b = 0, srednia;
                        for (int i = 0; i < nRow; i++)
                        {
                            String line = sr.ReadLine();
                            line.Replace("\r\n", "");
                            bArr = line.Split(' ');

                            for (int j = 0; j < nCol * 3; j++)
                            {
                                switch (counter)
                                {
                                    case 1:
                                        {
                                            r = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                    case 2:
                                        {
                                            g = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                    case 3:
                                        {
                                            b = Convert.ToInt32(bArr[j]);
                                            break;
                                        }
                                }
                                if (counter == 3) // jesli doliczy do 3ciego koloru
                                {
                                    srednia = (r + g + b) / 3;
                                    counter = 0;
                                    bArr[j - 2] = Convert.ToString(srednia);
                                    bArr[j - 1] = Convert.ToString(srednia);
                                    bArr[j] = Convert.ToString(srednia);
                                }
                                counter++;
                            }
                            counter = 1; // reset counter
                            sw.Write(string.Join(" ", bArr) + "\n");
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Choosed wrong number.");
                        break;
                    }
            }
            //void negate(int color2) //TODO: podmienic w kazdym z kolorow
            //{
            //    for (int i = 0; i < nRow; i++)
            //    {
            //        for (int j = 0; j < nCol; j++)
            //        {
            //            int Rc = GetInt(sr);
            //            int Gc = GetInt(sr);
            //            int Bc = CodingAccuracy - GetInt(sr);
            //            sw.Write(Rc + " " + Gc + " " + Bc + " ");
            //        }
            //        sw.WriteLine();
            //    }
            //}
        }


        static void Initialization(StreamReader sr, StreamWriter sw)
        {
            int pixels;
            String buffer;
            char cBuffer;
            String[] bArr;
            List<long> positions = new List<long>();

            positions.Add(18); // add initial position of caret
            fileType = sr.ReadLine();
            buffer = sr.ReadLine();
            bArr = buffer.Split(' ');
            nRow = Int32.Parse(bArr[0]);
            nCol = Int32.Parse(bArr[1]);
            CodingAccuracy = GetInt(sr);
            pixels = nRow * nCol;

            sw.BaseStream.Seek(0, SeekOrigin.Begin); // SEEEKKKKKKKKKKK
            sw.WriteLine(fileType);
            sw.WriteLine(nCol + " " + nRow);
            sw.WriteLine(CodingAccuracy);
            Console.WriteLine("File type:" + fileType);
            Console.WriteLine("nRow           =" + nRow);
            Console.WriteLine("nCol           =" + nCol);
            Console.WriteLine("Coding Accuracy=" + CodingAccuracy);
            sr.ReadLine(); // read null line
            //foreach (long el in positions)
            //{
            //    Console.WriteLine("positions:" + el);
            //}

            void lineChanger(string newText, string fileName, int line_to_edit)
            {
                string[] arrLine = File.ReadAllLines(fileName);
                arrLine[line_to_edit - 1] = newText;
                File.WriteAllLines(fileName, arrLine);

            }
        }





        static int GetInt(StreamReader sr)
        {
            int integer = 0;

            int n = sr.Read();
            while (n == ' ' || n == '\n' || n == '\r' || n == '\t')
            {
                n = sr.Read();
                Console.WriteLine("asd:" + n);
                System.Threading.Thread.Sleep(350);
            }
            if (n == -1)
                return integer;
            //       Console.WriteLine("xxxxxxxxx:" + n);
            while (n >= '0' && n <= '9')
            {
                integer = integer * 10 + n - '0';
                n = sr.Read();
            }
            return integer;
        }



        static void Rotate(StreamReader sr, StreamWriter sw)
        {

            String[] bArr;
            List<long> positions = new List<long>();

            //    bArr = sr.ReadLine().Split(' ');
            //    positions.Add(sr.BaseStream.Position);
            //    sr.BaseStream.Position = desiredPosition;
            //    sr.DiscardBufferedData();

            // =========== obrot 0 90 stopni
            //      long position = sw.BaseStream.Position;
            int position = 18;
            bool isFirstCol = true;
            int row = 0;
            sw.BaseStream.Position = 0;
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            while (!sr.EndOfStream)
            {
                bArr = sr.ReadLine().Split(' ');
                int bArrL = bArr.Length - 1;

                for (int i = bArrL; i > -1; i--)
                {
                    //             position = position + 4;
                    if (i == (bArrL - 1))
                    {

                        //      Console.WriteLine("ifuje:" + sw.BaseStream.Position);
                    }

                    if (isFirstCol)
                    {
                        row++;
                        Console.WriteLine("sw.BaseStream.Position:" + sw.BaseStream.Position);
                        position = position + 4;
                        sw.WriteLine(bArr[i]);
                        // positions.Add(position + 4);
                        positions.Add(position);
                        Console.WriteLine("Wywolanie:isFirstCol");
                    }
                    else
                    {
                        sw.BaseStream.Seek(position, SeekOrigin.Begin);
                        sw.Write(" " + bArr[i]);
                        sw.Flush();

                    }

                    sw.BaseStream.Position = position;
                    //      sw.Write("\n");
                    Console.WriteLine("bArr:" + bArr[i] + " / index:" + i + " position:" + position);
                }

                isFirstCol = false;

            }
        }
    }
}

