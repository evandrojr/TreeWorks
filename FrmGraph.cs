using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Spartacus {
    public partial class FrmGraph : Form {

        public NodeTargetCategorical N = null;
        public List<float> ALst = new List<float>();
        public List<float> AbsLst = new List<float>();
        public float ABest=0;
        public float doX = 0f+100f, doY = 700f-100f, gSize=400f, sf=100f, a0=0f, a1=0f, c=0f;
        public float excess = 1.2f;
        public List<float> x0Lst = new List<float>();
        public List<float> x1Lst = new List<float>();
        public List<int> n = new List<int>();
        System.Drawing.Pen p;
        System.Drawing.Graphics g; 


        
        public FrmGraph() {
            InitializeComponent();
            g = this.CreateGraphics();
        }

        private void FrmGraph_Load(object sender, EventArgs e) {
            draw();
        }

        private void button1_Click(object sender, EventArgs e) {

        }

        private void FrmGraph_Paint(object sender, PaintEventArgs e) {
            draw();
        }


        private float tx(float dx) {
            return (dx * sf) + doX;
        }

        private float ty(float dy) {
            return (-dy * sf) + doY;
        }

        public void PlotScaled(){
            p = new System.Drawing.Pen(System.Drawing.Color.Blue);

            for (int i = 0; i < x0Lst.Count; ++i) {
                if (n.Contains(i))
                    p.Color = Color.Red;
                else
                    p.Color = Color.Blue;
                g.DrawLine(p, tx(x0Lst[i])-2, ty(x1Lst[i])-2, tx(x0Lst[i])+2, ty(x1Lst[i])+2);
                g.DrawLine(p, tx(x0Lst[i]) - 2, ty(x1Lst[i]) + 2, tx(x0Lst[i]) + 2, ty(x1Lst[i]) - 2);
            }
        }

        public void DrawUnivariateDivisor() {

            p = new System.Drawing.Pen(System.Drawing.Color.Black);
            if (N.BestSplit.PredictorLstIdx == 0)
                g.DrawLine(p, tx(c)+2, ty(-excess), tx(c)+2, ty(excess));            
            if (N.BestSplit.PredictorLstIdx == 1)
                g.DrawLine(p, tx(-excess), ty(c)-2, tx(excess), ty(c)-2);            
            

        }

        public void DrawAllDivisors() {

            if (a1 == 0 && a0 == 0)
                return;
            float x0b, x0e; //b for Begin, e for End
            float x1b, x1e;
            p = new System.Drawing.Pen(System.Drawing.Color.SeaGreen);
            if (a1 != 0) {
                for(int i=0; i < ALst.Count; ++i){
                    x1b = (-c - ALst[i] * -excess) / a1;
                    x1e = (-c - ALst[i] * excess) / a1;
                    try {
                        g.DrawLine(p, tx(-excess), ty(x1b), tx(excess), ty(x1e));
                    } catch { }
                }
            } else {
                for (int i = 0; i < ALst.Count; ++i) {
                    x0b = (-c - ALst[i] * -excess) / a0;
                    x0e = (-c - ALst[i] * excess) / a0;
                    try {
                        g.DrawLine(p, tx(x0b), ty(-excess), tx(x0e), ty(excess));
                    } catch { }
                }
            }
        }

        public void DrawAllBisectorDivisors() {

            if (a1 == 0 && a0 == 0)
                return;
            float x0b, x0e; //b for Begin, e for End
            float x1b, x1e;
            p = new System.Drawing.Pen(System.Drawing.Color.SeaGreen);
            if (a1 != 0) {
                for (int i = 0; i < AbsLst.Count; ++i) {
                    x1b = (-c - AbsLst[i] * -excess) / a1;
                    x1e = (-c - AbsLst[i] * excess) / a1;
                    try {                    
                        g.DrawLine(p, tx(-excess), ty(x1b), tx(excess), ty(x1e));
                    } catch { }
                }
            } else {
                for (int i = 0; i < AbsLst.Count; ++i) {
                    x0b = (-c - AbsLst[i] * -excess) / a0;
                    x0e = (-c - AbsLst[i] * excess) / a0;
                    try {
                        g.DrawLine(p, tx(x0b), ty(-excess), tx(x0e), ty(excess));
                    } catch { }
                }
            }
        }


        public void DrawBestDivisor() {

            if (a1 == 0 && a0 == 0)
                return;
            float x0b, x0e; //b for Begin, e for End
            float x1b, x1e;
            p = new System.Drawing.Pen(System.Drawing.Color.Salmon);
            if (a1 != 0) {
                    x1b = (-c - ABest * -excess) / a1;
                    x1e = (-c - ABest * excess) / a1;
                    try {
                        g.DrawLine(p, tx(-excess), ty(x1b), tx(excess), ty(x1e));
                    } catch { }
            } else {
                    x0b = (-c - ABest * -excess) / a0;
                    x0e = (-c - ABest * excess) / a0;
                    try {
                        g.DrawLine(p, tx(x0b), ty(-excess), tx(x0e), ty(excess));
                    } catch { }
            }
        }


        private void draw() {
            p = new System.Drawing.Pen(System.Drawing.Color.Black);
            g.DrawLine(p, doX, doY, doX + gSize, doY);
            g.DrawLine(p, doX, doY + 1, doX + gSize, doY + 1);
            g.DrawLine(p, doX, doY, doX, doY - gSize);
            g.DrawLine(p, doX + 1, doY, doX + 1, doY - gSize);
            
            if(rbOriginal.Checked)
                DrawAllDivisors();
            if (rbBisector.Checked)
                DrawAllBisectorDivisors();
            DrawUnivariateDivisor();
            DrawBestDivisor();
            PlotScaled();            
        }

        private void FrmGraph_FormClosing(object sender, FormClosingEventArgs e) {
            p.Dispose();
            g.Dispose();
        }

        private void tbScale_Scroll(object sender, EventArgs e) {
            sf = tbScale.Value;
            Invalidate();
        }

        private void rbBisector_CheckedChanged(object sender, EventArgs e) {
            Invalidate();
        }

        private void rbOriginal_CheckedChanged(object sender, EventArgs e) {
            Invalidate();
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e) {
            Invalidate();
        }
        
    }
}