using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Timers;

namespace Gabriel.Cat.S.Utilitats
{
    public delegate void BitmapAnimatedFrameChangedEventHanlder(BitmapAnimated bmpAnimated, Bitmap frameActual);
    public class BitmapAnimated : ObjectAutoId
    {
        Llista<KeyValuePair<Bitmap, int>> frames;
        int frameAlAcabar;
        int index;
        System.Timers.Timer timer;

        #region Variables
        int numeroDeRepeticiones = 0;


        #endregion

        public event BitmapAnimatedFrameChangedEventHanlder FrameChanged;
        public BitmapAnimated(IList<Bitmap> bmps, params int[] delays)
        {
         
            NumeroDeRepeticionesFijas = 1;
            frameAlAcabar = 0;
            AnimarCiclicamente = true;
            frames = new Llista<KeyValuePair<Bitmap, int>>();
            if (bmps != null)
                for(int i=0,j=0;i<bmps.Count;i++)
                {
                    frames.Add(new KeyValuePair<Bitmap, int>(bmps[i], delays[j]));
                    if (delays.Length > i)
                        j++;
                }
            timer = new System.Timers.Timer();
            timer.Elapsed += ChangeFrame;




        }



        public bool AnimarCiclicamente { get; set; }
        public int FrameAlAcabar
        {
            get { return frameAlAcabar; }
            set { frameAlAcabar = Math.Abs(value) % frames.Count; }
        }
        public int ActualFrameIndex
        {
            get { return this.index; }
            set { index = value % frames.Count; }
        }
        public Bitmap ActualBitmap
        {
            get { return frames[ActualFrameIndex].Key; }
        }

        public bool MostrarPrimerFrameAlAcabar
        {
            get
            {
                return frameAlAcabar == 0;
            }

            set
            {
                if (value)
                    frameAlAcabar = 0;
                else frameAlAcabar = frames.Count - 1;
            }
        }
        public int FrameASaltarAnimacionCiclica
        {
            get; set;
        }
        public int NumeroDeRepeticionesFijas { get; set; }

        public KeyValuePair<Bitmap, int> this[int index]
        {
            get { return frames[index]; }
        }
        public void AddFrame(Bitmap bmp, int delay = 500, int posicion = -1)
        {
            if (bmp == null || delay < 0)
                throw new ArgumentException();

            KeyValuePair<Bitmap, int> frame = new KeyValuePair<Bitmap, int>(bmp, delay);

            if (posicion < 0 || posicion > frames.Count)
                frames.Add(frame);
            else
                frames.Insert(posicion, frame);
        }

        public void RemoveFrame(int index)
        {
            if (frames.Count <= index + 1||index<0)
                throw new ArgumentOutOfRangeException();
            frames.RemoveAt(index);
        }
        private void ChangeFrame(object sender, ElapsedEventArgs e)
        {
            FrameChanged(this, frames[ActualFrameIndex].Key);
            ActualFrameIndex++;
            if (numeroDeRepeticiones > 0)
                if (ActualFrameIndex == FrameASaltarAnimacionCiclica)
                    ActualFrameIndex++;

            if (ActualFrameIndex==0)
                numeroDeRepeticiones++;
        
            if(numeroDeRepeticiones == NumeroDeRepeticionesFijas && !AnimarCiclicamente)
                FrameChanged(this, frames[FrameAlAcabar].Key);

            timer.Interval = frames[ActualFrameIndex].Value;
        }
        public void Start()
        {
            if (FrameChanged == default)
                throw new Exception("FrameChanged doesn't asigned ");
            timer.Interval = frames[ActualFrameIndex].Value;
            timer.Start();

        }
        public void Stop()
        {
            timer.Stop();
        }
        /// <summary>
        /// Stop and init animation
        /// </summary>
        public void Reset()
        {
            Stop();
            numeroDeRepeticiones = 0;
            ActualFrameIndex = 0;
            
        }



    }
}