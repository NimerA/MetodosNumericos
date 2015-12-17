using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using Main;
using info.lundin.math;

namespace Metodos_Numericos.Algorithms
{
    public class Pointf
    {
        public decimal X,Y;

        public Pointf(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

    }
    public class Pointd
    {
        public double x, y;
        public Pointd(double X, double Y)
        {
            x = X;
            y = Y;
        }

    }

    class TrazadoresCubicos
    {
        private List<Pointf> _listaF= new List<Pointf>();
        private string FPO="FPOd", FPN="FPNd";
        private decimal FPOd, FPNd;

        public void SetExample()
        {
            AddPoint( 2, 5 );
            AddPoint( 4, 6 );
            AddPoint( 5, 9 );
            AddPoint( 8, 5 );
            AddPoint( 10, 4 );
            AddFPO( 1 );
            AddFPN( 0 );
        }

        public void Clear()
        {
            _listaF.Clear();
        }

        public void AddPoint(decimal x, decimal y)
        {
            _listaF.Add(new Pointf(x,y));
        }

        public void AddFPN(string fpn)
        {
            FPN = fpn;
        }

        public void AddFPN( decimal fpn )
        {
            FPNd = fpn;
        }

        public void AddFPO(string fpo)
        {
            FPO = fpo;
        }

        public void AddFPO( decimal fpo )
        {
            FPOd = fpo;
        }

        // TRAZADORES CUBICOS NATURALES.
        public string Solve()
        {
            int MaximoFilas = _listaF.Count;
            //Asignacion de dimensiones (a arreglos necesarios) para el proceso.

            //Matriz 
            decimal[,] Matrix = new decimal[MaximoFilas, 6];

            //Arreglo con diferencias entre valores de las (X's)
            decimal[] h = new decimal[MaximoFilas];

            decimal[] alfa = new decimal[MaximoFilas];
            decimal[] l = new decimal[MaximoFilas];
            decimal[] miu = new decimal[MaximoFilas];
            decimal[] z = new decimal[MaximoFilas];

            //Arreglos de los coeficientes
            decimal[] c = new decimal[MaximoFilas];
            decimal[] b = new decimal[MaximoFilas];
            decimal[] d = new decimal[MaximoFilas];

            //Entrada de valores (X,Y)
            for (int i = 0; i <= MaximoFilas - 1; i++)
            {
                //X
                Matrix[i, 0] = _listaF[i].X;
                //F(X) o Y
                Matrix[i, 1] = _listaF[i].Y;
            }

            //Determinacion de las distancias (PASO 1)
            for (int i1 = 0; i1 <= MaximoFilas - 2; i1++)
            {
                h[i1] = Matrix[i1 + 1, 0] - Matrix[i1, 0];
            }

            //PASO 2
            for (int i2 = 1; i2 <= MaximoFilas - 2; i2++)
            {
                alfa[i2] = ((3 / h[i2]) * (Matrix[i2 + 1, 1] - Matrix[i2, 1])) - ((3 / h[i2 - 1]) * (Matrix[i2, 1] - Matrix[i2 - 1, 1]));
            }

            //PASOS (3,4,5) ENCARGADOS DE LA SOLUCION DE UN SISTEMA LINEAL TRIDIAGONAL)

            //PASO 3
            l[0] = 1;
            miu[0] = 0;
            z[0] = 0;

            //PASO 4
            for (int i3 = 1; i3 <= MaximoFilas - 2; i3++)
            {
                l[i3] = 2 * (Matrix[i3 + 1, 0] - Matrix[i3 - 1, 0]) - h[i3 - 1] * miu[i3 - 1];
                miu[i3] = h[i3] / l[i3];
                z[i3] = (alfa[i3] - h[i3 - 1] * z[i3 - 1]) / l[i3];

            }

            //PASO 5
            l[MaximoFilas - 1] = 1;
            z[MaximoFilas - 1] = 0;
            c[MaximoFilas - 1] = 0;

            //PASO 6
            for (int j = MaximoFilas - 2; j >= 0; j--)
            {
                c[j] = z[j] - miu[j] * c[j + 1];
                b[j] = (Matrix[j + 1, 1] - Matrix[j, 1]) / h[j] - (h[j] * (c[j + 1] + 2 * c[j]) / 3);
                d[j] = ((c[j + 1] - c[j]) / (3 * h[j]));
            }

            string STC = "";
            //PASO 7 (Copiado de los valores de los coeficientes (A,B,C,D) a la matriz madre)
            for (int j2 = 0; j2 <= MaximoFilas - 2; j2++)
            {
                //Coeficientes
                //A
                Matrix[j2, 2] = Matrix[j2, 1];
                //B
                Matrix[j2, 3] = b[j2];
                //C
                Matrix[j2, 4] = c[j2];
                //D
                Matrix[j2, 5] = d[j2];
                Console.WriteLine("J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[j2, 2], Matrix[j2, 3], Matrix[j2, 4], Matrix[j2, 5]);
                STC += string.Format("J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[j2, 2], Matrix[j2, 3], Matrix[j2, 4],Matrix[j2, 5]) + "\n";
            }

            //Cadena que almacenará el sistema de trazas cúbicas.
            

            //Generacion de (S.E) - Trazadores cubicos naturales (Conjuntos de polinomios)
            //for (int j3 = 1; j3 <= MaximoFilas - 3; j3++)
            //{
            //    STC = STC + "n" + Matrix[j3, 2].ToString() + " + " + Matrix[j3, 3].ToString() + " (x " + "-" + Matrix[j3, 0].ToString() + ") " + " + " + Matrix[j3, 4].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^2  " + " + " + Matrix[j3, 5].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^3  " + " - [" + Matrix[j3, 0].ToString() + "," + Matrix[j3 + 1, 0].ToString() + "]" + "r";
            //}

            //Regresame la cadena con los trazadores cúbicos.
            return (STC);
        }

        public string SolveS()
        {
            int MaximoFilas = _listaF.Count;
            //Asignacion de dimensiones (a arreglos necesarios) para el proceso.

            //Matriz 
            decimal[,] Matrix = new decimal[ MaximoFilas, 6 ];

            //Arreglo con diferencias entre valores de las (X's)
            decimal[] h = new decimal[ MaximoFilas ];

            decimal[] alfa = new decimal[ MaximoFilas ];
            decimal[] l = new decimal[ MaximoFilas ];
            decimal[] miu = new decimal[ MaximoFilas ];
            decimal[] z = new decimal[ MaximoFilas ];

            //Arreglos de los coeficientes
            decimal[] c = new decimal[ MaximoFilas ];
            decimal[] b = new decimal[ MaximoFilas ];
            decimal[] d = new decimal[ MaximoFilas ];

            //Entrada de valores (X,Y)
            for ( int i = 0; i <= MaximoFilas - 1; i++ )
            {
                //X
                Matrix[ i, 0 ] = _listaF[ i ].X;
                //F(X) o Y
                Matrix[ i, 1 ] = _listaF[ i ].Y;
            }

            //Determinacion de las distancias (PASO 1)
            for ( int i1 = 0; i1 <= MaximoFilas - 2; i1++ )
            {
                h[ i1 ] = Matrix[ i1 + 1, 0 ] - Matrix[ i1, 0 ];
            }

            //PASO 2
            alfa[ 0 ] = (  3 * ( Matrix[ 1, 1 ] - Matrix[ 0, 1 ] )  / h[ 0 ] - 3 * eval( FPO, Matrix[ 0, 0 ] ) );
            alfa[ MaximoFilas - 1 ] = ( 3 * eval( FPN, Matrix[ MaximoFilas - 1, 0 ] ) - 3 * ( Matrix[ MaximoFilas - 1, 1 ] - Matrix[ MaximoFilas - 2, 1 ] ) / h[ MaximoFilas - 2 ] );
            
            //PASO 3
            for ( int i2 = 1; i2 <= MaximoFilas - 2; i2++ )
            {
                alfa[ i2 ] = ( ( 3 / h[ i2 ] ) * ( Matrix[ i2 + 1, 1 ] - Matrix[ i2, 1 ] ) ) - ( ( 3 / h[ i2 - 1 ] ) * ( Matrix[ i2, 1 ] - Matrix[ i2 - 1, 1 ] ) );
            }

            //PASOS (4,5,6) ENCARGADOS DE LA SOLUCION DE UN SISTEMA LINEAL TRIDIAGONAL)

            //PASO 4
            l[ 0 ] = 2*h[0];
            miu[ 0 ] = 0.5m;
            z[ 0 ] = alfa[0]/l[0];

            //PASO 5
            for ( int i3 = 1; i3 <= MaximoFilas - 2; i3++ )
            {
                l[ i3 ] = 2 * ( Matrix[ i3 + 1, 0 ] - Matrix[ i3 - 1, 0 ] ) - h[ i3 - 1 ] * miu[ i3 - 1 ];
                miu[ i3 ] = h[ i3 ] / l[ i3 ];
                z[ i3 ] = ( alfa[ i3 ] - h[ i3 - 1 ] * z[ i3 - 1 ] ) / l[ i3 ];

            }

            //PASO 6
            l[ MaximoFilas - 1 ] = h[MaximoFilas-2]*(2-miu[MaximoFilas-2]);
            z[ MaximoFilas - 1 ] = (alfa[MaximoFilas-1]-h[MaximoFilas-2]*z[MaximoFilas-2])/l[MaximoFilas-1];
            c[ MaximoFilas - 1 ] = z[MaximoFilas-1];

            //PASO 7
            for ( int j = MaximoFilas - 2; j >= 0; j-- )
            {
                c[ j ] = z[ j ] - miu[ j ] * c[ j + 1 ];
                b[ j ] = ( Matrix[ j + 1, 1 ] - Matrix[ j, 1 ] ) / h[ j ] - ( h[ j ] * ( c[ j + 1 ] + 2 * c[ j ] ) / 3 );
                d[ j ] = ( ( c[ j + 1 ] - c[ j ] ) / ( 3 * h[ j ] ) );
            }

            string STC = "";
            //PASO 7 (Copiado de los valores de los coeficientes (A,B,C,D) a la matriz madre)
            for ( int j2 = 0; j2 <= MaximoFilas - 2; j2++ )
            {
                //Coeficientes
                //A
                Matrix[ j2, 2 ] = Matrix[ j2, 1 ];
                //B
                Matrix[ j2, 3 ] = b[ j2 ];
                //C
                Matrix[ j2, 4 ] = c[ j2 ];
                //D
                Matrix[ j2, 5 ] = d[ j2 ];
                Console.WriteLine( "J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[ j2, 2 ], Matrix[ j2, 3 ], Matrix[ j2, 4 ], Matrix[ j2, 5 ] );
                STC += string.Format( "J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[ j2, 2 ], Matrix[ j2, 3 ], Matrix[ j2, 4 ], Matrix[ j2, 5 ] ) + "\n";
            }

            //Cadena que almacenará el sistema de trazas cúbicas.


            //Generacion de (S.E) - Trazadores cubicos naturales (Conjuntos de polinomios)
            //for (int j3 = 1; j3 <= MaximoFilas - 3; j3++)
            //{
            //    STC = STC + "n" + Matrix[j3, 2].ToString() + " + " + Matrix[j3, 3].ToString() + " (x " + "-" + Matrix[j3, 0].ToString() + ") " + " + " + Matrix[j3, 4].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^2  " + " + " + Matrix[j3, 5].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^3  " + " - [" + Matrix[j3, 0].ToString() + "," + Matrix[j3 + 1, 0].ToString() + "]" + "r";
            //}

            //Regresame la cadena con los trazadores cúbicos.
            return ( STC );
        }

        private decimal eval(string function, decimal x )
        {
            if (function.Equals("FPOd"))
                return FPOd;
            if ( function.Equals( "FPNd" ) )
                return FPNd;
            MathParser parser = new MathParser();
            parser.LocalVariables.Add( "x", ( decimal ) x );
            var result = parser.Parse( function );
            return ( decimal ) result;
        }
    }
    //========================================================= Metodo De Biseccion ========================================================
    public class MetodoDeBiseccion
    {

        double fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }



        public string MetodoBiseccion(string function, double a, double b, double Tol, int iterador)
        {

            if ((fn(function, a) < 0 && fn(function, b) < 0) || (fn(function, b) > 0 && fn(function, a) > 0) || a > b)
                return "El metodo fracaso";
            var FA = fn(function, a);
            var i = 1;
            while (i <= iterador)
            {
                double p = a + ((b - a) / 2);
                var FP = fn(function, p);
                //  Console.WriteLine("#"+(i)+"  A= "+a+"  B= "+b+"  P= "+p+"  F(p)= "+FP);
                if (FP == 0 || (b - a) / 2 < Tol)
                    return "P" + (i) + "= " + p;
                i++;
                if (FA * FP > 0)
                {
                    a = p;
                    FA = FP;
                }
                else
                    b = p;
            }
            return "El metodo fracaso despues de " + iterador + " iteraciones";
        }
    }

    //===================================================================== MULLER ===========================================================
    class Muller
    {

        public decimal X0, X1, X2, Tol, N;

        public string Function;

        public Muller()
        {

        }

        private decimal F(decimal x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", x);
            var result = parser.Parse(Function);
            return result;
        }

        public void AddExample()
        {
            Function = "16x^4 - 40x^3 + 5x^2 +20x + 6";
            Tol = 0.00001m;
            X0 = 0.5m;
            X1 = 1;
            X2 = 1.5m;
            N = 7;
        }

        public string Solve()
        {
            decimal x0 = X0, x1 = X1, x2 = X2, tol = Tol, n = N;
            decimal h1, h2, d1, d2, d, b, D, E, h, p = decimal.Zero, i;
            string res="";
            //(Paso 1)
            h1 = x1 - x0;
            h2 = x2 - x1;
            d1 = (F(x1) - F(x0)) / h1;

            d2 = (F(x2) - F(x1)) / h2;

            d = (d2 - d1) / (h2 + h1);

            i = 3;


            //(Paso 2)

            while (i <= n)
            {
                //(Paso 3)
                b = d2 + h2 * d;

                var ts = 1;
                if ((- 4 * F(x2) * d) < 0)
                    ts = -1;
                D = Sqrt(((b * b)+(-4 * F(x2) * d) * ts));
                //(Paso 4)

                if (Math.Abs(b - D) < Math.Abs(b + D))
                    E = b + D;
                else
                    E = b - D;
                //(Paso 5)
                h = -2 * F(x2) / E;
                p = x2 + h;
                res = p + "" + (ts < 0 ? "+-i" : "");
                Console.WriteLine("{0} - {1}", i, p);
                //(Paso 6)
                if (tol > Math.Abs(h))
                {
                    break;
                }
                //(Paso 7)
                x0 = x1;
                x1 = x2;
                x2 = p;
                h1 = x1 - x0;
                h2 = x2 - x1;
                d1 = (F(x1) - F(x0)) / h1;
                d2 = (F(x2) - F(x1)) / h2;
                d = (d2 - d1) / (h2 + h1);
                i++;
            }

            if (i > n)
            {
                return string.Format("El Procedimiento fallo despues de {0} operaciones",n);
            }
            else
            {
                return string.Format("La Respuesta Correcta es: Iteracion {0} - {1} ",i,res);
            }


        }

        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

    }

    //=====================================================================================  METODO DE LA SECANTE ==============================================================

    class MetodoDeLaSecante
    {
        public decimal XO, Xl, EPS, EPSI, MAXIT;
        public string function;

        public MetodoDeLaSecante()
        {
            function = "x^3 + 2x^2 + 10x - 20";
            XO = 0;
            Xl = 1;
            EPS = 0;
            EPSI = (decimal)0.001;
            MAXIT = 4;
        }


        public string solve()
        {
            try
            {
                for (int i = 0; i < MAXIT; i++)
                {
                    var X = XO - (Xl - XO) * F(XO) / (F(Xl) - F(XO));
                    if (Math.Abs(X - Xl) < EPS)
                    {
                        Console.WriteLine(X);
                        return "La Respuesta Correcta es " + X.ToString();
                    }
                    if (Math.Abs(F(X)) < EPSI)
                    {
                        Console.WriteLine(X);
                        return "La Respuesta Correcta es " + X.ToString();
                    }
                    XO = Xl;
                    Xl = X;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"Error: " + e.Message);
            }
            Console.WriteLine(@"EL MÉTODO NO CONVERGE A UNA RAÍZ");
            return ("EL MÉTODO NO CONVERGE A UNA RAÍZ");
            

        }

        private decimal F(decimal x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (decimal)result;
        }
    }


    //============================================================INTERPOLACION DE LAGRANGE=============================

    public class InterpolacionLaGrange
    {

        public string Calculate(double x, double[] xd, double[] yd)
        {


            double sum = 0;
            for (int i = 0, n = xd.Length; i < n; i++)
            {
                if (Math.Abs(x - xd[i]) < 0)
                {
                    return yd[i].ToString(CultureInfo.InvariantCulture);
                }
                double FXINT = yd[i];
                for (int j = 0; j < n; j++)
                {
                    if ((i == j) || (Math.Abs(xd[i] - xd[j]) < 0))
                    {
                        continue;
                    }
                    FXINT *= (x - xd[j]) / (xd[i] - xd[j]);
                }
                sum += FXINT;
            }

            return "La Respuesta Correcta es: " + sum.ToString(CultureInfo.InvariantCulture);

        }
    }


    //================================================================INTERPOLANTE DE NEWTON=============================

    public class InterpolanteDeNewton
    {
        public string Calculate(double value, double[] x, double[] y)
        {
            var b = new double[x.Length, y.Length];

            for (var i = 0; i < x.Length; i++)
                b[i, 0] = y[i];

            for (var j = 1; j < x.Length; j++)
                for (var i = 0; i < x.Length - j; i++)
                    b[i, j] = (b[i + 1, j - 1] - b[i, j - 1]) / (x[i + j] - x[i]);

            double xt = 1;
            double yi = b[0, 0];
            for (var i = 0; i < x.Length - 1; i++)
            {
                xt = xt * (value - x[i]);
                yi = yi + b[0, i + 1] * xt;

            }

            return "La respuesta Correcta es:" + yi;
        }
    }


    //=========================================================NEWTON-RAPHSON==========================================

    public class NewtonRaphson
    {
        double fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }


        public string newtonRaphson(string function, string function2, double aprox, double TOL, double iterador)
        {

            int i = 1;
            while (i <= iterador)
            {
                double p = aprox - (fn(function, aprox) / fn(function2, aprox));
                //Console.WriteLine("#" + (i) + "  Po= " + aprox + "  P= " + p);
                if (Math.Abs(p - aprox) < TOL)
                    return "La Respuesta Correcta es P" + (i) + "= " + p;
                i++;
                aprox = p;
            }
            return "El metodo fracaso despues de " + iterador + " iteraciones";
        }
    }

    //==================================================FALSA POSICION==========================================================

    public class PosicionFalsa
    {
        double Fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }
        public string Calculate(string function, double p0, double p1, double tol, int iterador)
        {
            var q0 = Fn(function, p0);
            var q1 = Fn(function, p1);
            var i = 2;
            for (; i <= iterador; i++)
            {
                var processPartialResult = p1 - (q1 * (p1 - p0) / (q1 - q0));

                if (Math.Abs(processPartialResult - p1) < tol)
                {
                    return "La Respuesta Correcta es " + processPartialResult;
                }

                var qProcessPartialResult = Fn(function, processPartialResult);

                if ((qProcessPartialResult * q1) < 0)
                {
                    p0 = p1;
                    q0 = q1;
                }

                p1 = processPartialResult;
                q1 = qProcessPartialResult;


            }
            return "Processo falló en la iteración: " + i;
        }
    }


    //==================================================================PUNTO FIJO========================================

    public class PuntoFijo
    {
        double fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }
        public string puntofijo(string function, string function2, double aprox, double TOL, double iterador)
        {
            int i = 1;
            while (i <= iterador)
            {
                var p = fn(function2, aprox);
                if (Math.Abs(p - aprox) < TOL)
                    return "La Respuesta Correcta es P" + (i) + "= " + p;
                i++;
                aprox = p;
            }
            return "El metodo fracaso despues de " + iterador + " iteraciones";
        }
    }
    //======================================================== DIFRENCIACION MATEMATICA ====================
        public class DiferenciacionNumerica
        {
            public string Calcular(double[] x, double[] fx, double XD, int N)
            {
                double DP = 0;
                var i = 0;
                try
                {
                    while (i <= N)
                    {
                        double p = 1;
                        var j = 0;
                        while (j <= N)
                        {
                            if (i != j)
                                p = p * (x[i] - x[j]);
                            j++;
                        }
                        var k = 0;
                        double s = 0;
                        while (k <= N)
                        {
                            if (i != k)
                            {
                                double Pone = 1;
                                j = 0;
                                while (j <= N)
                                {
                                    if (j != i && j != k)
                                        Pone = Pone * (XD - x[j]);
                                    j++;
                                }
                                s = s + Pone;
                            }
                            k++;
                        }
                        DP = DP + ((fx[i] / p) * s);
                        i++;
                    }

                    return "La Respuesta Correcta es " + DP;
                }
                catch (Exception)
                {
                    return "El Metodo Fracaso";
                }
            }
        }
        //===================================================== REGLA DEl TRAPEZIO ================

        public class ReglaDelTrapezio 
        {
            public double fn(string function, double x)
            {
                MathParser parser = new MathParser();
                parser.LocalVariables.Add("x", (decimal)x);
                var result = parser.Parse(function);
                return (double)result;
            }

            public string Calcular(string function, double limiteA, double limiteB, int iteracion)
            {
                var suma = 0.0;
                var area = 0.0;
                var dx = (limiteB - limiteA) / iteracion;
                var xi = limiteA;
                var i = 1;
                try
                {
                    while (i < iteracion)
                    {
                        xi = xi + dx;
                        suma = suma + (2 * fn(function, xi));
                        i++;
                    }
                    area = dx / 2 * ((fn(function, limiteA) + suma + fn(function, limiteB)));
                    return "La Respuesta Correcta es " + area;

                }
                catch (Exception)
                {
                   return "El Metodo Fracaso";
                }
            }

        }
        //================================================================ SIMPSON =============================
        public class Simpson 
        {
       
            public double fn(string function, double x)
            {
                MathParser parser = new MathParser();
                parser.LocalVariables.Add("x", (decimal)x);
                var result = parser.Parse(function);
                return (double)result;
            }

            public string Calcular(string function, double a, double b, int N)
            {
                try
                {
                    var h = (b - a) / N;
                    var x0 = fn(function, a) + fn(function, b);
                    var x1 = 0.0;
                    var x2 = 0.0;
                    var x = 0.0;
                    for (int i = 1; i <= N - 1; i++)
                    {
                        x = a + (i * h);
                        if ((i % 2) == 0)
                            x2 = x2 + fn(function, x);
                        else
                            x1 = x1 + fn(function, x);

                    }

                    var result = h * (x0 + (2 * x2) + (4 * x1)) / 3;
                    return "La Respuesta Correcta es " + result;
                }
                catch (Exception)
                {
                    return "El Metodo Fracaso";
                }

            }
        }
        //================================================================= CUADRATURAS GAUSS =============
        public class CuadraturaGauss 
        {

            public double fn(string function, double x)
            {
                MathParser parser = new MathParser();
                parser.LocalVariables.Add("x", (decimal)x);
                var result = parser.Parse(function);
                return (double)result;
            }

            public string Calcular(string function, double A, double B, int N)
            {
                var NP = new int[] { 0, 2, 3, 4, 5, 6 };
                var IAUX = new int[] { 0, 1, 2, 4, 6, 9, 12 };
                var Z = new double[] { 0.0, 0.577350269, 0.0, 0.774596669, 0.339981044, 0.861136312, 0.0, 0.538469310, 0.906179846, 0.238619186, 0.661209387, 0.932469514 };
                var W = new double[] { 0.0, 1.0, 0.888888888, 0.555555555, 0.652145155, 0.347854845, 0.568888888, 0.478628671, 0.236926885, 0.467913935, 0.360761573, 0.171324493 };
                var I = 1;
                while (I <= 5)
                {
                    if (N == NP[I])
                    {
                        goto PASO10;
                    }
                    I = I + 1;
                }
                return "N no es 2, 3, 4, 5, 6";
            PASO10:
                var S = 0.0;
                var J = IAUX[I];
                try
                {
                    while (J <= IAUX[I + 1] - 1)
                    {
                        var ZAUX = (Z[J] * (B - A) + B + A) / 2;
                        S = S + fn(function, ZAUX) * W[J];
                        ZAUX = (-Z[J] * (B - A) + A + B) / 2;
                        S = S + fn(function, ZAUX) * W[J];
                        J = J + 1;
                    }
                }
                catch (Exception)
                {
                    return "El Metodo Fracaso";
                }

                var AREA = ((B - A) / 2) * S;
                return "La Respuesta Correcta es " + AREA;
            }
        }
        //================================================ METODO EULER ============
        public class MetodoEuler 
        {
            public List<Pointd> Calcular(string function, double X0, double XF, double Y0, int N)
            {

                try
                {
                    var h = (XF - X0) / N;
                    List<Pointd> list = new List<Pointd>();
                    list.Add(new Pointd(X0, Y0));

                    for (int i = 1; i <= N; i++)
                    {
                        var exp = new ExpressionParser();
                        exp.Values.Add("x", X0);
                        exp.Values.Add("y", Y0);
                        exp.Values.Add("t", X0);
                        var result = exp.Parse(function);
                        Y0 = Y0 + (h * result);
                        X0 = X0 + (h);

                        list.Add(new Pointd(Math.Round(X0,2), Y0));
                    }

                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }
        //========================= SOLUCION RUNGE-KUTTA ================================

        public class SolucionRungeKutta
        {
            public List<Pointd> Calcular(string function, double X0, double XF, double Y0, int N)
            {
                var h = (XF - X0) / N;
                var k1 = 0.0;
                var k2 = 0.0;
                var k3 = 0.0;
                var k4 = 0.0;

                List<Pointd> list = new List<Pointd>();
                list.Add(new Pointd(X0, Y0));
                try
                {
                    for (int i = 1; i <= N; i++)
                    {
                        var exp = new ExpressionParser();
                        exp.Values.Add("x", X0);
                        exp.Values.Add("t", X0);
                        exp.Values.Add("y", Y0);
                        k1 = exp.Parse(function);
                        var exp2 = new ExpressionParser();
                        exp2.Values.Add("x", X0 + (h / 2));
                        exp2.Values.Add("t", X0 + (h / 2));
                        exp2.Values.Add("y", Y0 + (h * (k1 / 2)));
                        k2 = exp2.Parse(function);
                        var exp3 = new ExpressionParser();
                        exp3.Values.Add("x", X0 + (h / 2));
                        exp3.Values.Add("t", X0 + (h / 2));
                        exp3.Values.Add("y", Y0 + (h * (k2 / 2)));
                        k3 = exp3.Parse(function);
                        var exp4 = new ExpressionParser();
                        exp4.Values.Add("x", X0 + (h));
                        exp4.Values.Add("t", X0 + (h));
                        exp4.Values.Add("y", Y0 + (h * (k3)));
                        k4 = exp4.Parse(function);
                        Y0 = Y0 + ((h / 6) * (k1 + (2 * k2) + (2 * k3) + k4));
                        X0 = X0 + h;
                        list.Add(new Pointd(Math.Round(X0, 2), Y0));
                    }

                    return list;

                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
        //========================= SISTEMAS RUNGE-KUTTA ================================
        public class SistemasRungeKutta 
        {
            public double func(double x, double y)
            {
                double elevado = Math.Pow(y, 2);
                return 0.5 * (1 + x) * elevado;
            }

            //Chicos n es el numero de subintervalos a emplear tiene que solicitarse antes y no puede ser 0
            public List<Pointd> Calcular(double x0, double y0, double xf, int n)
            {
                var list = new List<Pointd>();
                list.Add(new Pointd(x0, y0));
                double h, k1, k2, k3, k4;

                h = (xf - x0) / n;

                for (int i = 1; i <= n; i++)
                {
                    k1 = func(x0, y0);
                    k2 = func(x0 + h / 2, y0 + h * k1 / 2);
                    k3 = func(x0 + h / 2, y0 + h * k2 / 2);
                    k4 = func(x0 + h, y0 + h * k3);
                    y0 = y0 + (k1 + 2 * k2 + 2 * k3 + k4) * h / 6;
                    x0 = x0 + h;
                    list.Add(new Pointd(x0,y0));
                }
                return list;
            }
        }
        //========================= ELIMINACION GAUSS ================================
        public class EliminacionGauss 
        {
            public List<double> Calcular(double[,] a)
            {

                int filas = a.GetLength(0);
                int columnas = a.GetLength(1);
                //INICIALIZANDO RESPUESTAS
                double[] x = new double[filas];
                List<double> list = new List<double>();
                double det = 1;
                try
                {

                    //METER LOS DATOS A UN ARREGLO AL VECTOR B
                    double[] b = new double[filas];
                    for (int i = 0; i < filas; i++)
                    {
                        b[i] = a[i, columnas - 1];
                    }

                    int n = filas;// numero de  ecuaciones
                    //INICIA LA FUNCION DE ELIMINACION DE GAUSS


                    det = 1; //paso uno
                    for (int i = 0; i < n - 1; i++)
                    {
                        det = det * a[i, i];
                        if (det == 0)
                        {
                            return null;
                        }
                        for (int k = i + 1; k < n; k++)
                        {
                            for (int j = i + 1; j < n; j++)
                            {
                                a[k, j] = a[k, j] - (a[k, i] * a[i, j]) / a[i, i];
                            }
                            b[k] = b[k] - (a[k, i] * b[i]) / a[i, i];
                        }
                    } //fin del for

                    det = det * a[n - 1, n - 1];

                    if (det == 0)
                    {
                        return null;
                    }

                    x[n - 1] = b[n - 1] / a[n - 1, n - 1];

                    for (int i = n - 1; i >= 0; i--)
                    {
                        x[i] = b[i];
                        for (int j = i + 1; j < n; j = j + 1)
                        {
                            x[i] = x[i] - a[i, j] * x[j];
                        }
                        x[i] = x[i] / a[i, i];
                    }
                    //AQUI TERMIMNA LA FUNCION DE GAUUSS

                    // empezar a asignar las incognitas a el data grid
                    list.Add(det);
                    for (int i = 0; i < x.Length; i++)
                    {
                        list.Add(x[i]);
                    }

                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //========================= ELIMINACION GAUSS JORDAN ================================
        public class GaussJordan
        {
            public List<double> Calcular(double[,] a)
            {
                try
                {
                    int x = a.GetLength(0);
                    int y = a.GetLength(1);

                    double[] r = new double[x];
                    List<double> list = new List<double>();


                    double t, s;
                    int i, l, j, k, m, n;

                    n = r.Length - 1;
                    m = n + 1;
                    for (l = 0; l <= n - 1; l++)
                    {
                        j = l;
                        for (k = l + 1; k <= n; k++)
                        {
                            if (!(Math.Abs(a[j, l]) >= Math.Abs(a[k, l])))
                            {
                                j = k;
                            }
                        }

                        if (!(j == l))
                        {
                            for (i = 0; i <= m; i++)
                            {
                                t = a[l, i];
                                a[l, i] = a[j, i];
                                a[j, i] = t;
                            }
                        }

                        for (j = l + 1; j <= n; j++)
                        {
                            t = (a[j, l] / a[l, l]);
                            for (i = 0; i <= m; i++)
                            {
                                a[j, i] -= t * a[l, i];
                            }
                        }
                    }
                    r[n] = a[n, m] / a[n, n];
                    for (i = 0; i <= n - 1; i++)
                    {
                        j = n - i - 1;
                        s = 0;
                        for (l = 0; l <= i; l++)
                        {
                            k = j + l + 1;
                            s += a[j, k] * r[k];
                        }
                        r[j] = ((a[j, m] - s) / a[j, j]);
                    }

                    for (int z = 0; z < r.Length; z++)
                    {

                        list.Add(r[z]);
                    }

                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //========================= MEDIANTE INVERSA ================================

        public class MedianteInversa 
        {
            public double[,] Calcular(double[,] a)
            {
                //PASO 1
                int i, j, k, s;
                int y = a.GetLength(0);
                int x = a.GetLength(1);
                double[,] m1 = new double[x * 2, y * 2];
                double[,] m2 = new double[y, x];
                double coef, elemento;
                double[] aux = new double[y * 2];
                double[] vecSol = new double[y];
                double[] solFinal = new double[y];
                k = y;

                for (i = 0; i < y; i++)
                {
                    for (j = 0; j < (x - 1); j++)
                    {
                        m1[i, j] = a[i, j];
                    }
                }

                for (i = 0; i < y; i++)
                {
                    vecSol[i] = a[i, x - 1];
                }

                //PASO 2 
                for (i = 0; i < k; i++)
                {
                    for (j = k; j < 2 * k; j++)
                    {
                        if (i == (j - k))
                            m1[i, j] = 1;
                        else
                            m1[i, j] = 0;
                    }
                }
                //Iteraciones
                for (s = 0; s < k; s++)
                {
                    elemento = m1[s, s];
                    for (j = 0; j < 2 * k; j++)
                        m1[s, j] = m1[s, j] / elemento;

                    for (i = 0; i < k; i++)
                    {
                        if (i != s)
                        {
                            coef = m1[i, s];
                            for (j = 0; j < 2 * k; j++)
                                aux[j] = m1[s, j] * (coef * -1);
                            for (j = 0; j < 2 * k; j++)
                                m1[i, j] = m1[i, j] + aux[j];
                        }
                    }
                }
                //PASO 3
                for (i = 0; i < k; i++)
                {
                    for (j = k; j < 2 * k; j++)
                        if (double.IsNaN(m1[i, j]))
                        {
                            throw new Exception("Error al revisar matriz");
                        }
                }
                //PASO 4
                for (i = 0; i < k; i++)
                {
                    for (j = k; j < 2 * k; j++)
                    {
                        m2[i, (j - k)] = (Math.Round(m1[i, j], 3));
                    }
                }

                //PASO 5
                double tempRes = 0;
                for (i = 0; i < k; i++)
                {
                    for (j = k; j < 2 * k; j++)
                    {
                        tempRes += (m1[i, j] * vecSol[j - k]);
                    }
                    solFinal[i] = tempRes;
                    tempRes = 0;
                }
                for (i = 0; i < k; i++)
                    m2[i, (x - 1)] = (Math.Round(solFinal[i], 3));


                return m2;

            }

        }
        //========================= REGRESION LINEAL ================================

        public class RegresionLineal 
        {
            public double Calcular(double[] valoresX, double[] valoresY, double xo)
            {
                var m = valoresX.Length;
                var constante1 = 0.0;
                var constante2 = 0.0;
                var constante3 = 0.0;
                var constante4 = 0.0;
                var i = 0;

                while (i < m)
                {
                    constante1 = constante1 + valoresY[i];
                    constante2 = constante2 + (valoresX[i] * valoresX[i]);
                    constante3 = constante3 + valoresX[i];
                    constante4 = constante4 + (valoresX[i] * valoresY[i]);

                    i = i + 1;
                }
                var coeficiente1 = ((constante1 * constante2) - (constante3 * constante4)) /
                                   ((m * constante2) - (constante3 * constante3));

                var coeficiente2 = ((m * constante4) - (constante1 * constante3)) / ((m * constante2) - (constante3 * constante3));

                var y = coeficiente1 + (xo * coeficiente2);

                return y;
            }
        }
        //========================= DIFERENCIAS FINITAS ================================
        public class MetodoDiferenciasFinitas 
        {
            public double EvaluateFn(string function, double x)
            {
                var parser = new MathParser();
                parser.LocalVariables.Add("x", (decimal)x);
                var result = parser.Parse(function);
                return (double)result;
            }

            public List<Pointd> Calcular(string px, string qx, string rx, double a, double b, double alpha, double beta, int N)
            {
                List<Pointd> list = new List<Pointd>();

                if (N < 2)
                {
                    return null;
                }

                var A = new double[N + 1];
                var B = new double[N + 1];
                var C = new double[N + 1];
                var D = new double[N + 1];
                var L = new double[N + 1];
                var U = new double[N + 1];
                var Z = new double[N + 1];
                var W = new double[N + 2];
                var Y = new double[N + 2];
                var X = new double[N + 2];

                var h = (b - a) / (N + 1);
                var x = a + h;
                A[0] = (2 + (h * h * EvaluateFn(qx, x)));
                B[0] = (-1 + ((h / 2) * EvaluateFn(px, x)));
                D[0] = (-h * h * EvaluateFn(rx, x) + (1 + (h / 2) * EvaluateFn(px, x)) * alpha);

                for (var i = 1; i < N - 1; i++)
                {
                    x = a + (i * h);
                    A[i] = (2 + (h * h * EvaluateFn(qx, x)));
                    B[i] = (-1 + (h / 2) * EvaluateFn(px, x));
                    C[i] = (-1 - (h / 2) * EvaluateFn(px, x));
                    D[i] = (-h * h * EvaluateFn(rx, x));
                }

                x = b - h;
                A[N - 1] = (2 + h * h * EvaluateFn(qx, x));
                C[N - 1] = (-1 - (h / 2) * EvaluateFn(px, x));
                D[N - 1] = (-h * h * EvaluateFn(rx, x) + (1 - (h / 2) * EvaluateFn(px, x)) * beta);

                L[0] = A[0];
                U[0] = B[0] / A[0];
                Z[0] = D[0] / L[0];

                for (var i = 1; i < N - 1; i++)
                {
                    L[i] = A[i] - C[i] * U[i - 1];
                    U[i] = B[i] / L[i];
                    Z[i] = ((D[i] - C[i] * Z[i - 1]) / L[i]);
                }

                L[N - 1] = (A[N - 1] - C[N - 1] * U[N - 2]);
                Z[N - 1] = ((D[N - 1] - C[N - 1] * Z[N - 2]) / L[N - 1]);

                Y[0] = alpha;
                Y[N + 1] = beta;
                Y[N] = Z[N - 1];

                W[0] = alpha;
                W[N + 1] = beta;

                for (var i = N - 1; i > -1; i--)
                {
                    Y[i] = Z[i] - U[i] * Y[i + 1];
                    W[i + 1] = Y[i];
                }

                for (var i = 0; i <= N + 1; i++)
                {
                    X[i] = a + (i * h);
                }

                for (var i = 0; i < W.Length; i++)
                {
                    list.Add(new Pointd(X[i], W[i]));
                }

                return list;
            }
        }









}
