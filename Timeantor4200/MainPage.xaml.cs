using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Timeantor4200
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// 
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        //Begin elapsed time section declartions
        int elapsedDif = 100;

        int elMili = 0;
        int elSecound = 0;
        int elMin = 0;
        int elHour = 0;
        //end of elapsed decl
        int startORstop = 1; //start is 1, stop is 0
        DispatcherTimer TimeLoo;
        DispatcherTimer MiliLoo;

        double curRate = 0.00;

        double cumSecound=0;
        double cum30Sec=0;
        double cumMin=0;
        double cum15Min=0;
        double cumhalf=0;
        double cumHour=0;

        int tallySecound = 0;
        int tally30Sec = 0;
        int tallyMin = 0;
        int tally15Min = 0;
        int tallyhalfHour = 0;
        int tallycumHour = 0;
       
        public MainPage()
        {

           

            TimeLoo = new DispatcherTimer();
            TimeLoo.Tick += moveFXTimer;
            TimeLoo.Interval = new TimeSpan(0, 0, 0, 0, 1000); //count for every secound
            TimeLoo.Start();
            this.InitializeComponent();

            MiliLoo = new DispatcherTimer();
            MiliLoo.Tick += miliTimer;
            MiliLoo.Interval = new TimeSpan(0, 0, 0, 0, 75); //count for every milisecound, OG 75

            if (timerExists == false)
            {
                timerExists = true; //in use so use once
            }


        }
        void miliTimer(object sender, object e) //timer that handles counting elapsed time
        {
        
          
            elMili = elMili + elapsedDif;
            if (elMili>=1000)
            {
                if (elSecound == 30 || elSecound == 59)
                {
                    tally30Sec = tally30Sec + 1;
                    cum30Sec = cum30Sec + rate30Sec;
                    lbl_30Secound_tally.Text = tally30Sec.ToString();
                    lbl_30Secound_Copy.Text = cum30Sec.ToString();
                }
                lbl_Secound_Copy.Text = Math.Round(cumSecound, 4).ToString();
                elMili = 0;
                elSecound = elSecound + 1;
                cumSecound = cumSecound + rateSecound;
                tallySecound = tallySecound + 1;
                lbl_Secound_tally.Text = tallySecound.ToString();
              //  lbl_Hour.Text = cumHour.ToString();
          
            }
            if (elSecound>=60)
            {
                if (elSecound == 60)
                {
                    tallyMin = tallyMin + 1;
                    cumMin = cumMin + rateMin;
                    lbl_Minute_Copy.Text = cumMin.ToString();
                    lbl_Minute_tally.Text = tallyMin.ToString();
                }
                elSecound = 0;

         
              
              

                elMin = elMin + 1;

                if (elMin==15||elMin==30||elMin==45||elMin==59)
                {
                    cum15Min = cum15Min + rate15Min;
                    tally15Min = tally15Min + 1;
                    lbl_15Minutes_Copy.Text = cum15Min.ToString();
                    lbl_15Minutes_tally.Text = tally15Min.ToString();
                  
                }
                if (elMin==30 || elMin==59)
                {
                    tallyhalfHour = tallyhalfHour + 1;
                    cumhalf = cumhalf + rate30Sec;
                    lbl_HalfHour_Copy.Text = cumhalf.ToString();
                    lbl_HalfHour.Text = tallyhalfHour.ToString();
                  
                }
                if (elMin==59)
                {
                    tallycumHour = tallycumHour + 1;
                    cumHour = cumHour + rateHour;
                    lbl_Hour_Copy.Text = cumHour.ToString();
                    lbl_Hour_tally.Text = tallycumHour.ToString();
                }
            }
            if (elMin>=60)
            {
                elMin = 0;
                elHour = elHour + 1;
            }
            lbl_elapsedTime.Text = "Elapsed Time: "+elHour+"."+elMin+"."+elSecound+"."+elMili;
            Calcultron4200();
           
        }
        void moveFXTimer(object sender, object e) //timer that handles the main menu stuff
        {
            lbl_CurTime.Text = DateTime.Today.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToString("h:mm:ss tt");
           
        }
        private void ResetEverything()
        {
            curRate = 0.0;
            cumSecound = 0;
            cum30Sec = 0;
            cumMin = 0;
            cum15Min = 0;
            cumhalf = 0;
            cumHour = 0;

              tallySecound = 0;
              tally30Sec = 0;
              tallyMin = 0;
              tally15Min = 0;
              tallyhalfHour = 0;
              tallycumHour = 0;

        }
        private void txt_Rate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MiliLoo.IsEnabled==true)
            {
                txt_Rate.Text = "";
                btn_start.Content = "Start";
                startORstop = 1;
                MiliLoo.Stop();
                ResetEverything();
                elMili = 0;
                elSecound = 0;
                elMin = 0;
                elHour = 0;
            }
           
            if (txt_Rate.Text.Length>4)
            {
               txt_Rate.Text=txt_Rate.Text.Remove(3, 1);
            }
            try
            {
                curRate = Convert.ToDouble(txt_Rate.Text); //asuming this will block non char values
            }
            catch
            {
                txt_Rate.Text = "";
            }
            Calcultron4200();

          
        }
        double rateSecound, rate30Sec, rateMin, rate15Min, rateHalf, rateHour;
        private void Calcultron4200()
        {
            //this is the powerhouse of calculations done here with value curRate

             rateSecound = Math.Round(((curRate / 60) / 60), 4);
             rate30Sec = Math.Round(((curRate / 60) / 2), 4);
             rateMin = Math.Round(((curRate / 60)), 4);
             rate15Min = Math.Round(((curRate / 4)), 4);
             rateHalf = Math.Round(((curRate / 2)), 4);
             rateHour = Math.Round(((curRate / 1)), 4);

            lbl_Secound.Text = rateSecound.ToString();
            lbl_30Secound.Text = rate30Sec.ToString();
            lbl_Minute.Text = rateMin.ToString();
            lbl_15Minutes.Text = rate15Min.ToString();
            lbl_HalfHour.Text = rateHalf.ToString();
            lbl_Hour.Text = rateHour.ToString();

            if (MiliLoo.IsEnabled==true)
            {
              
                //cumMin = cumMin + rateMin;
                //cum15Min = cum15Min + rate15Min;
                //cumhalf = cumhalf + rate30Sec;
                //cumHour = cumHour + rateHour;

                lbl_Secound_Cumaltive.Text = Math.Round(cumSecound,4).ToString();

                
            
                
                /*
                lbl_30Secounds_Cumaltive1.Text = cum30Sec.ToString();
                lbl_Minute_Cumaltive2.Text = cumMin.ToString();
                lbl_15Min_Cumalitive.Text = cum15Min.ToString();
                lbl_HalfHourCumaltive3.Text = cumhalf.ToString();
                lbl_Hour.Text = cumHour.ToString();
                 */
            }

           

            
        }
        private void lbl_earnedPerHalfHour_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (startORstop==0)
            {
                startORstop = 1; //turn on START
                btn_start.Content = "Start";
                MiliLoo.Stop();
                ResetEverything();
                 elMili = 0;
                 elSecound = 0;
                 elMin = 0;
                 elHour = 0;
                 txt_Rate.Text = "";

            }
            else if (startORstop==1)
            {
                startORstop = 0; //END the shift
                btn_start.Content = "End";

                lbl_Secound_Copy.Text = "0.00";
                lbl_30Secound_Copy.Text = "0.00";
                lbl_Minute_Copy.Text = "0.00";
                lbl_15Minutes_Copy.Text = "0.00";
                lbl_HalfHour_Copy.Text = "0.00";
                lbl_Hour_Copy.Text = "0.00";
            
                MiliLoo.Start();
            }
        }
        bool timerExists = false;
        private void rad_Norm_Checked(object sender, RoutedEventArgs e)
        {
            if (timerExists==true)
            {
                MiliLoo.Interval = new TimeSpan(0, 0, 0, 0, 75); //count for every milisecound, OG 75
                elapsedDif = 100;
            }
           
        }

        private void rad_fast_Checked(object sender, RoutedEventArgs e)
        {
            if (timerExists==true)
            {
                MiliLoo.Interval = new TimeSpan(0, 0, 0, 0, 1); //count for every milisecound, OG 75
                elapsedDif = 999;
            }
          
        }

    }
}


//orginally wrote on 6-6-17
//version 1.5 with cumultive scores added on 6-17-17 along with speeder changer
//changed/added splash screens and logos

//version1.75
//added tally columns and changed calculation updates
