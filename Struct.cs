using System;
using System.Collections.Generic;
using System.Text;

namespace Spartacus {

    public class NNT { //Number, Number. Text
        public double N0, N1;
        public string T;

        public NNT(double n0, double n1, string t) {
            N0 = n0; N1 = n1; T = t;
        }
        
    }

    public class NTT { //Number, Text, Text
        public double N;
        public string T0, T1;

        public NTT(double n, string t0, string t1) {
            N = n; T0 = t0; T1 = t1;
        }
    }
    
    public class N4 { //Number, Number, Number, Number
        public double N0;
        public double N1;
        public double N2;
        public double N3;

        public N4(double n0, double n1, double n2, double n3) {
            N0 = n0; N1 = n1; N2 = n2; N3 = n3;
        }
    }

    public class N3T { //Number, Number, Number, Text
        public double N0;
        public double N1;
        public double N2;
        public string T;

        public N3T(double n0, double n1, double n2, string t) {
            N0 = n0; N1 = n1; N2 = n2; T = t;
        }
    }

    public class NN { //Number, Number
        public double N0;
        public double N1;

        public NN(double n0, double n1) {
            N0 = n0; N1 = n1;
        }
    }

    public class NT { //Number, Text
        public double N;
        public string T;

        public NT(double n, string t) {
            N = n; T = t;
        }
    }

    public struct DI { //Double, int
        public double D;
        public int I;
    }


}
