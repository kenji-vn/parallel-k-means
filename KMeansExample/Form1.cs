using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime;
using KMeans;


namespace KMeansExample
{
    public partial class Form1 : Form
    {
        private Random r = new Random();

        BaseKMeans<Point> parKMeans;

        BaseKMeans<Point> seqKMeans;

        double[][] generatedMeans;
        double[][] generatedInputs;

        Process curProcess;


        public Form1()
        {
            InitializeComponent();

            curProcess = Process.GetCurrentProcess();

            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size( pnlDisplay.Width + 1, pnlDisplay.Height + 1 );

            m_BufferedGraphics = context.Allocate( pnlDisplay.CreateGraphics(), new Rectangle( 0, 0, pnlDisplay.Width, pnlDisplay.Height ) );
            m_Graphics = m_BufferedGraphics.Graphics;

            m_Graphics = pnlDisplay.CreateGraphics();
            m_Pen = new Pen( Color.Black );
            m_Pen2 = new Pen( Color.Red, 3 );


            seqKMeans = PointKMeansFactory.Create(false);
            parKMeans = PointKMeansFactory.Create(true);
        }

        private void button1_Click( object sender, EventArgs e )
        {
            btnRun.Text = "...";
            lblParTime.Text = lblSeqTime.Text = lblSpeedup.Text = "---";


            btnRun.Enabled = false;
            btnStep.Enabled = false;

            this.Update();


            int drawStepSize = (int)(Math.Log( m_Results.Input.Length ) + 1);

            //Make pretty stuff.
            while( !m_Results.Converged )
            {
                parKMeans.Step( m_Results );

                if( m_Results.Iterations % drawStepSize == 0 ) //Drawing is slow.  And seeing EVERY little step is, well, boring.
                {
                    UpdatePanel();
                    Thread.Sleep( 100 );
                }
            }


            UpdatePanel();




            KMeansResult<Point> parResult = new KMeansResult<Point>( m_Results );
            KMeansResult<Point> seqResult = new KMeansResult<Point>( m_Results );

            GC.Collect();   //Force GC's so it's a lot less likely it'll happen in the middle of this stuffs.

            DateTime start = DateTime.Now;

            while( !seqResult.Converged )
            {
                seqKMeans.Step( seqResult );
            }


            DateTime end = DateTime.Now;

            TimeSpan seqTimeElapsed = end - start;
            lblSeqTime.Text = FormatNum( seqTimeElapsed.TotalSeconds );

            lblSeqTime.Update();


            GC.Collect();



            start = DateTime.Now;

            while( !parResult.Converged )
            {
                parKMeans.Step( parResult );
            }

            end = DateTime.Now;

            TimeSpan parTimeElapsed = end - start;
            lblParTime.Text = FormatNum( parTimeElapsed.TotalSeconds );

            lblParTime.Update();



            lblSpeedup.Text = FormatNum( seqTimeElapsed.TotalSeconds / parTimeElapsed.TotalSeconds );



            //m_Results = parResult;
            //this.UpdatePanel();


            btnRun.Text = "Run";
            GC.Collect();
        }

        private static string FormatNum( double d )
        {
            return String.Format( "{0:0.00}", d );
        }

        private void buttonD_Click( object sender, EventArgs e )
        {

            btnRunD.Text = "Running...";
            lblParTimeDStatic.Text = lblSpeedupDStatic.Text = lblParTimeD.Text = lblSeqTimeD.Text = lblSpeedupD.Text = "---";

            this.Update();

            DoWorknDim();

            btnRunD.Text = "Run";
            GC.Collect();
        }

        private void DoWorknDim()
        {
            #region Init
            int inputSizeD, dimD, kD;

            try
            {
                inputSizeD = int.Parse( txtInputSizeD.Text );
                dimD = int.Parse( txtDimD.Text );

                kD = int.Parse( txtKD.Text );
            }
            catch
            {
                return;
            }

            if( kD <= 0 || kD >= inputSizeD || inputSizeD <= 0 || dimD <= 0 )
                return;

            #endregion


            #region Gen Random inputs
            int squareSize = (int)Math.Sqrt( inputSizeD * 250 );  //Completely arbritrary.


            double[][] nAry = new double[inputSizeD][];

            for( int i = 0; i < nAry.Length; i++ )
            {
                double[] nDimAry = new double[dimD];

                for( int j = 0; j < nDimAry.Length; j++ )
                {
                    nDimAry[j] = r.Next( squareSize );
                }

                nAry[i] = nDimAry;
            }

            #endregion


            if( kD > nAry.Length )  //sanity
                return;


            double[][] parResultsD;
            double[][] seqResultsD;



            double[][] initialMeansD = new double[kD][];

            initialMeansD = nAry.Distinct().OrderBy( x => r.Next() ).Take( kD ).ToArray();    //Grab k initial, *distinct* random means.
            //(Don't use the Base kMeans version so that way we are sure we have the same starting points for each set... for benchmarking

            generatedInputs = nAry;
            generatedMeans = initialMeansD;




            BaseKMeans<double[]> nDimkMeans = KMeansFactory.CreateSequential();

            TimeSpan seqTimeElapsed = TimeElapsed( () => { seqResultsD = nDimkMeans.Run( kD, nAry, initialMeansD ); }, 1, "Sequential" );

            lblSeqTimeD.Text = FormatNum( seqTimeElapsed.TotalSeconds );
            Console.WriteLine( "Sequential Time Elapsed: {0}", seqTimeElapsed );

            lblSeqTimeD.Update();




            nDimkMeans = KMeansFactory.CreateParallel();

            TimeSpan parallelTimeElapsed = TimeElapsed( () => { parResultsD = nDimkMeans.Run( kD, nAry, initialMeansD ); }, "Par" );

            lblParTimeD.Text = FormatNum( parallelTimeElapsed.TotalSeconds );
            Console.WriteLine( "Parallel Time Elapsed: {0}", parallelTimeElapsed );
            lblParTimeD.Update();




            lblSpeedupD.Text = FormatNum( seqTimeElapsed.TotalSeconds / parallelTimeElapsed.TotalSeconds );
            lblSpeedupD.Update();








            nDimkMeans = new ParallelImplStaticPartitioning<double[]>(KMeansFactory.EuclideanDistace, KMeansFactory.MeanAsCentroid);
            TimeSpan parallelStaticTimeElapsed = TimeElapsed( () => { parResultsD = nDimkMeans.Run( kD, nAry, initialMeansD ); }, "ParSTC" );

            lblParTimeDStatic.Text = FormatNum( parallelStaticTimeElapsed.TotalSeconds );
            Console.WriteLine( "Parallel Static Time Elapsed: {0}", parallelStaticTimeElapsed );

            lblSpeedupDStatic.Text = FormatNum( seqTimeElapsed.TotalSeconds / parallelStaticTimeElapsed.TotalSeconds );

        }

        public TimeSpan TimeElapsed( Action act, string name )
        {
            return TimeElapsed( act, Environment.ProcessorCount, name );
        }

        public TimeSpan TimeElapsed( Action act, int procs, string name )
        {
            GC.Collect();   //Lessen likelyhood of a GC happening during the benchmark

            TimeSpan start = curProcess.TotalProcessorTime;
            DateTime dtStart = DateTime.Now;


            act();


            TimeSpan end = curProcess.TotalProcessorTime;
            DateTime dtEnd = DateTime.Now;

            TimeSpan diff = end-start;
            TimeSpan dtDiff = dtEnd - dtStart;

            TimeSpan avgPerProc = new TimeSpan( diff.Ticks / procs ); //Get the avg work done per proc

            Console.WriteLine( "Did {0} seconds of processor work, for an avg of {1} seconds per Processor", diff, avgPerProc );

            return dtDiff;
        }

        private void btnGen_Click( object sender, EventArgs e )
        {
            int inputSize, k;

            try
            {
                inputSize = int.Parse( txtInputSize.Text );
                k = int.Parse( txtK.Text );
            }
            catch
            {
                return;
            }

            if( k <= 0 || k >= inputSize || inputSize <= 0 )
                return;


            
            Point[] initialPoints = new Point[inputSize];

            for( int i = 0; i < initialPoints.Length; i++ )
            {
                initialPoints[i] = new Point( r.Next( pnlDisplay.Width ), r.Next( pnlDisplay.Height ) );
            }




            Point[] initialsMeans = initialPoints.Distinct().OrderBy( x => r.Next() ).Take( k ).ToArray();    //Grab k initial, *distinct* random means
            m_Results = new KMeansResult<Point>( k, initialPoints, initialsMeans );




            btnRun.Enabled = true;
            btnStep.Enabled = true;

            UpdatePanel();
        }



        private Pen m_Pen;
        private Pen m_Pen2;
        Font drawFont = new Font( "Arial", 15, FontStyle.Bold );
        private Graphics m_Graphics;
        private BufferedGraphics m_BufferedGraphics;
        

        KMeansResult<Point> m_Results;
        
        private void UpdatePanel()
        {
            m_Graphics.Clear( Color.White );

            m_Graphics.DrawString( String.Format( "Iter: {0}", m_Results.Iterations ), drawFont, m_Pen2.Brush, 0, 0 );

            if( m_Results.ResultToInputMap == null )
            {
                //Haven't started grouping them yet, just draw them unfilled.

                for( int i = 0; i < m_Results.Input.Length; i++ )
                {
                    Point p = m_Results.Input[i];

                    m_Graphics.DrawEllipse( m_Pen, (int)p.X - 3, (int)p.Y - 3, 6, 6 );
                }

                for( int i = 0; i < m_Results.Result.Length; i++ )
                {
                    Point p = m_Results.Result[i];
                    m_Graphics.DrawRectangle( m_Pen2, (int)p.X - 3, (int)p.Y - 3, 6, 6 );
                }
            }
            else
            {
                double xScale = 255.0/pnlDisplay.Width;
                double yScale = 255.0/pnlDisplay.Height;
                double xyScale = 255.0 / ((pnlDisplay.Width + pnlDisplay.Height) / 2);


                for( int i = 0; i < m_Results.Result.Length; i++ )
                {

                    Point curResult = m_Results.Result[i];


                    m_Graphics.DrawRectangle( m_Pen2, (int)curResult.X - 3, (int)curResult.Y - 3, 6, 6 );   //Yes, draw it twice, for visualization purposes

                    int r = (int)((xScale)*(128-curResult.X));
                    int b = (int)((yScale)*(128-curResult.Y));
                    int g = (int)(128-((curResult.X + curResult.Y) / 2) * xyScale);
                    
                    Color c = Color.FromArgb( Math.Abs(r),  Math.Abs(g),  Math.Abs(b) );

                    Brush colorBrush = new SolidBrush( c );
                    Pen colorPen = new Pen( colorBrush );

                    IEnumerable<Point> coll = m_Results.ResultToInputMap[i];

                    foreach( Point p in coll )
                    {
                        m_Graphics.DrawEllipse( colorPen, (int)p.X - 3, (int)p.Y - 3, 6, 6 );
                        m_Graphics.FillEllipse( colorBrush, (int)p.X - 3, (int)p.Y - 3, 6, 6 );
                    }

                    colorBrush.Dispose();


                    m_Graphics.DrawRectangle( m_Pen2, (int)curResult.X - 3, (int)curResult.Y - 3, 6, 6 );
                }
            }

            m_Graphics.DrawString( String.Format( "Iter: {0}", m_Results.Iterations ), drawFont, m_Pen2.Brush, 0, 0 );

        }

        private void btnStep_Click( object sender, EventArgs e )
        {
            parKMeans.Step( m_Results );

            this.UpdatePanel();

            if( m_Results.Converged )
            {
                btnRun.Enabled = false;
                btnStep.Enabled = false;
            }
        }

        private void txtK_TextChanged( object sender, EventArgs e )
        {
            btnRun.Enabled = false;
            btnStep.Enabled = false;
        }

        private void txtInputSize_TextChanged( object sender, EventArgs e )
        {
            btnRun.Enabled = false;
            btnStep.Enabled = false;
        }

        private void button1_Click_1( object sender, EventArgs e )
        {
            if( generatedMeans == null || generatedInputs == null )
                return;



            #region Init
            int inputSizeD, dimD, kD;

            try
            {
                inputSizeD = int.Parse( txtInputSizeD.Text );
                dimD = int.Parse( txtDimD.Text );

                kD = int.Parse( txtKD.Text );
            }
            catch
            {
                return;
            }

            if( kD <= 0 || kD >= inputSizeD || inputSizeD <= 0 || dimD <= 0 )
                return;

            #endregion



            using( System.IO.StreamWriter file = new System.IO.StreamWriter( String.Format( "n{0}k{1}d{2}.txt", inputSizeD, kD, dimD ) ) )
            {
                file.WriteLine( "n={0} k={1} d={2}", inputSizeD, kD, dimD );

                for( int i = 0; i < generatedInputs.Length; i++ )
                {
                    double[] dAry = generatedInputs[i];

                    for( int j = 0; j < dAry.Length; j++ )
                        file.Write( "{0} ", dAry[j] );

                    file.WriteLine();
                }


                for( int i = 0; i < generatedMeans.Length; i++ )
                {
                    double[] dMeansAry = generatedMeans[i];

                    for( int j = 0; j < dMeansAry.Length; j++ )
                        file.Write( "{0} ", dMeansAry[j] );

                    file.WriteLine();
                }
            }
        }
    }
}
